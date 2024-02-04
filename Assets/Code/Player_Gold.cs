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
    private float lastSoundTime = -1; // ������ ���� ��� �ð��� �����ϱ� ���� ����

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

        // sword_0�� SpriteRenderer ã��
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
            lastSoundTime = Time.time; // ������ ���� ��� �ð� ������Ʈ
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
        animator.SetBool("IsDamaged", true); // ������ ���� ���·� ����
        StartCoroutine(ResetDamageState());
        GameManager.instance.OnHitpointChange();
    }

    // ������ ���¸� �ʱ�ȭ�ϴ� �ڷ�ƾ
    private IEnumerator ResetDamageState()
    {
        yield return new WaitForSeconds(0.5f); // ���¸� ������ �ð� ���� (��: 0.5��)
        animator.SetBool("IsDamaged", false); // ������ ���� ���� ����
        animator.SetBool("IsMoving", isMoving);
    }

    // ���⿡ ���� ���� override�� ���� �� �� �ִ�. Mover�� �����ϰ� �����Ƿ� ����
    protected override void Death()
    {
        isAlive = false;
        animator.SetBool("IsDeath", true); // �ִϸ����Ϳ� ��� ���� ����

        if (swordRenderer != null)
        {
            swordRenderer.enabled = false; // sword_0�� SpriteRenderer ��Ȱ��ȭ
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
