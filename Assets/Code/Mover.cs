using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    private Vector3 originalSize;

    protected Vector2 inputVec;
    public float speed;
    public Transform weapon;

    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    
    public float ySpeed = 0.75f;
    public float xSpeed = 1.0f;

    protected BoxCollider2D boxCollider;
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriter;

    // 맨처음에 한번 호출되는 함수
    private void Awake()
    {
        spriter = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        originalSize = transform.localScale;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        // Reset Move Delta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0).normalized;

        // swap sprite direction, wether you're going right or left
        if (moveDelta.x > 0 )
        {
            transform.localScale = originalSize;
        }
        else if (moveDelta.x < 0 )
        {
            transform.localScale = new Vector3(originalSize.x * -1, originalSize.y , originalSize.z);
        }

        // Add push vector, if any
        moveDelta += pushDirection;

        // Reduce push force every frame, based off recovery speed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverSpeed);

        // make sure we can move in this direction, by casting a box there first, if the box returns null, we're free to move
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            // Make this thing move!
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            // Make this thing move!
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
