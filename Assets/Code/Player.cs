using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Mover
{
    Animator animator;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer swordRenderer;
    private bool isAlive = true;
    private bool isMoving;
    private float originalXSpeed;
    private float originalYSpeed;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // sword_0의 SpriteRenderer 찾기
        Transform swordTransform = transform.Find("sword_0");
        if (swordTransform != null)
        {
            swordRenderer = swordTransform.GetComponent<SpriteRenderer>();
        }

        // 초기 속도 설정
        // originalXSpeed = xSpeed;
        // originalYSpeed = ySpeed;
        Debug.Log("originalxspeed: " + originalXSpeed);
    }

    void Update()
    {
        // Shift 키가 눌려 있는지 확인
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            Sprint(true); // 스프린트 활성화
        }
        else
        {
            Sprint(false); // 스프린트 비활성화
        }
    }



    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        // 움직임이 있는지 확인
        isMoving = (x == 0 && y == 0);
        animator.SetBool("IsMoving", isMoving);

        // if (isAlive)
            // UpdateMotor(new Vector3(x, y, 0));

    }

    public void SetSpeed(float newSpeedX, float newSpeedY)
    {
        // xSpeed = newSpeedX;
        // ySpeed = newSpeedY;
    }

    // 예를 들어, 'sprint' 기능을 추가한다면
    public void Sprint(bool isSprinting)
    {
        if (isSprinting)
        {
            SetSpeed(originalXSpeed * 2, originalYSpeed * 2); // Sprinting 시 속도 2배
        }
        else
        {
            SetSpeed(originalXSpeed, originalYSpeed); // Sprinting이 아닐 때는 원래 속도로
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

        if(hitpoint > maxHitpoint)
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


