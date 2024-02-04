using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : Collidable
{
    public bool isLeft;

    SpriteRenderer player;
    Animator animator;
    public AudioManager audioManager;

    // Damage struct
    public int[] damagePoint = { 1, 2, 3, 4, 5, 6, 7 };
    public float[] pushforce = { 2.0f, 2.2f, 2.5f, 3f, 3.2f, 3.6f, 4f };

    // Upgrade
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;

    // Swing
    private float cooldown = 0.5f;
    private float lastSwing;

    private float lastSoundTime = -1; // 마지막 사운드 재생 시간을 추적하기 위한 변수

    protected override void Start()
    {
        base.Start();
    }

    private void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
        animator = GetComponentInParent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    protected override void Update()
    {
        base.Update();

        bool isReverse = player.flipX;
        if (isReverse)
        {
            animator.SetBool("Reverse", true);
        }
        else
        {
            animator.SetBool("Reverse", false);
        }

        // Check if the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastSwing > cooldown && isReverse)
        {
            lastSwing = Time.time;
            // Debug.Log("space is now downing");
            animator.SetBool("Swing", true); // Set the Swing bool to true
            animator.SetBool("Reverse", true);

            if ((Time.time - lastSoundTime) >= 0.3f)
            {
                audioManager.RandomSwordness();
                lastSoundTime = Time.deltaTime; // 마지막 사운드 재생 시간 업데이트
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastSwing > cooldown && !isReverse)
        {
            lastSwing = Time.time;
            // Debug.Log("space is now downing");
            animator.SetBool("Swing", true); // Set the Swing bool to true
            animator.SetBool("Reverse", false);

            if ((Time.time - lastSoundTime) >= 0.3f)
            {
                audioManager.RandomSwordness();
                lastSoundTime = Time.deltaTime; // 마지막 사운드 재생 시간 업데이트
            }
        }
        else if (isReverse)
        {
            animator.SetBool("Swing", false); 
            animator.SetBool("Reverse", true);
        }
        else if (!isReverse)
        {
            animator.SetBool("Swing", false);
            animator.SetBool("Reverse", false);
        }
    }

    protected override void OnCollide(CapsuleCollider2D coll)
    {
        if (coll.tag == "Fighter")
        {
            if (coll.name == "Player")
                return;
            // create a new damage object, then we'll send it to the fighter we've hit
            Damage dmg = new Damage()
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushforce = pushforce[weaponLevel]
            };
            coll.SendMessage("ReceiveDamage", dmg);
        }

    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
        GameManager.instance.SaveState();
    }

    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }


}
