using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Weapon : Collidable
{
    // 무기를 쥔 방향
    public bool isLeft;
    public SpriteRenderer spriter;

    SpriteRenderer player;

    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0);
    Vector3 rightPosReverse = new Vector3(-0.15f, -0.15f, 0);
    Quaternion leftRot = Quaternion.Euler(0, 0, -20);
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -160);

    // Damage struct
    public int[] damagePoint = {1, 2, 3, 4, 5, 6, 7};
    public float[] pushforce = {2.0f, 2.2f, 2.5f, 3f, 3.2f, 3.6f, 4f};

    // Upgrade
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;

    // Swing
    private Animator anim;
    private float cooldown = 0.5f;
    private float lastSwing;

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();

    }

    private void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    private void LateUpdate()
    {
        bool isReverse = spriter.flipX;

        transform.localRotation = isReverse ? leftRotReverse : leftRot;
        spriter.flipY = isReverse;
        spriter.sortingLayerName = isReverse ? "Interactable" : "Weapon";
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

    private void Swing()
    {
        anim.SetTrigger("Swing");
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
