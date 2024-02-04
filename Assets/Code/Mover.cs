using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    private Vector3 originalSize;

    public Transform weapon;

    protected Vector3 moveDelta;
    protected RaycastHit2D hit;

    protected CapsuleCollider2D capsuleCollider;
    Rigidbody2D rigid_any;
    Animator anim;
    SpriteRenderer spriter_any;

    // 맨처음에 한번 호출되는 함수
    private void Awake()
    {
        spriter_any = GetComponent<SpriteRenderer>();
        rigid_any = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        originalSize = transform.localScale;
    }

    //protected virtual void UpdateMotor(Vector3 input)
    //{
    //    //// Reset Move Delta
    //    //moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0).normalized;

    //    // swap sprite direction, wether you're going right or left
    //    if (input.x > 0)
    //    {
    //        transform.localScale = originalSize;
    //    }
    //    else if (input.x < 0)
    //    {
    //        transform.localScale = new Vector3(originalSize.x * -1, originalSize.y, originalSize.z);
    //    }

    //    // Add push vector, if any
    //    // moveDelta += pushDirection;
    //    input += pushDirection;

    //    // Reduce push force every frame, based off recovery speed
    //    pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverSpeed);
        
    //    // make sure we can move in this direction, by casting a box there first, if the box returns null, we're free to move
    //    hit = Physics2D.CapsuleCast(transform.position, capsuleCollider.size, CapsuleDirection2D.Horizontal, 0,  new Vector2(0, input.y), LayerMask.GetMask("Actor", "Blocking"));
    //    // hit = Physics2D.BoxCast(transform.position, capsuleCollider.size, 0, new Vector2(0, input.y), Mathf.Abs(input.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
    //    Debug.Log("hit: " + hit);
    //    if (hit.collider == null)
    //    {
    //        // Make this thing move!
    //        transform.Translate(0, input.y * Time.deltaTime, 0);
    //    }

    //    hit = Physics2D.CapsuleCast(transform.position, capsuleCollider.size, CapsuleDirection2D.Vertical, 0, new Vector2(input.x, 0), LayerMask.GetMask("Actor", "Blocking"));
    //    // hit = Physics2D.BoxCast(transform.position, capsuleCollider.size, 0, new Vector2(input.x, 0), Mathf.Abs(input.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
    //    if (hit.collider == null)
    //    {
    //        // Make this thing move!
    //        transform.Translate(input.x * Time.deltaTime, 0, 0);
    //    }
    // }
}
