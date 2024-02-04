using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Gold : Mover
{
    public Vector2 inputVec;
    public float speed;

    private bool isAlive = true;
    private bool isMoving;
    private float lastSoundTime = -1; // 마지막 사운드 재생 시간을 추적하기 위한 변수

    SpriteRenderer swordRenderer;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator animator;
    public AudioManager audioManager;

    protected override void Start()
    {
        base.Start();
    }


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        // sword_0의 SpriteRenderer 찾기
        Transform swordTransform = transform.Find("Weapon_0");
        if (swordTransform != null)
        {
            swordRenderer = swordTransform.GetComponent<SpriteRenderer>();
        }
    }

    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);


        isMoving = (inputVec.x == 0 && inputVec.y == 0);
        animator.SetBool("IsMoving", isMoving);

        if (isMoving == false && (Time.time - lastSoundTime) >= 0.3f)
        {
            audioManager.RandomSoundness();
            lastSoundTime = Time.time; // 마지막 사운드 재생 시간 업데이트
        }

    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    private void LateUpdate()
    {
        if(inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if (!isAlive)
        {
            return;
        }

        base.ReceiveDamage(dmg);
        animator.SetBool("IsDamaged", true); // 데미지 받음 상태로 설정
        StartCoroutine(ResetDamageState());
        GameManager.instance.OnHitpointChange();
    }

    // 데미지 상태를 초기화하는 코루틴
    private IEnumerator ResetDamageState()
    {
        yield return new WaitForSeconds(0.5f); // 상태를 유지할 시간 설정 (예: 0.5초)
        animator.SetBool("IsDamaged", false); // 데미지 받음 상태 해제
        animator.SetBool("IsMoving", isMoving);
    }

    // 여기에 없는 것은 override로 끌고 올 수 있다. Mover를 참조하고 있으므로 가능
    protected override void Death()
    {
        isAlive = false;
        animator.SetBool("IsDeath", true); // 애니메이터에 사망 상태 설정

        if (swordRenderer != null)
        {
            swordRenderer.enabled = false; // sword_0의 SpriteRenderer 비활성화
        }
        GameManager.instance.deathMenuAnim.SetTrigger("Show");
    }

    public void SwapSprite(int skinId)
    {
        GetComponent<SpriteRenderer>().sprite = GameManager.instance.playerSprites[skinId];
    }

    public void OnLevelUp()
    {
        maxHitpoint++;
        hitpoint = maxHitpoint;
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }

    public void Heal(int healingAmount)
    {
        if (hitpoint == maxHitpoint)
        {
            return;
        }

        hitpoint += healingAmount;

        if (hitpoint > maxHitpoint)
        {
            hitpoint = maxHitpoint;
        }

        GameManager.instance.ShowText("+" + healingAmount.ToString() + "hp", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
        GameManager.instance.OnHitpointChange();
    }

    public void Respawn()
    {
        Heal(maxHitpoint);
        isAlive = true;
        immuneTime = Time.time;
        pushDirection = Vector3.zero;
    }
}
