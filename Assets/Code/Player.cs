using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    private Vector3 moveDelta;
    private RaycastHit2D hit;

    private BoxCollider2D boxCollider;
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

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }


    private void FixedUpdate()
    {
        //float x = Input.GetAxisRaw("Horizontal");
        //float y = Input.GetAxisRaw("Vertical");

        //moveDelta = new Vector3(x,y,0);

        //if (moveDelta.x > 0)
        //{
        //    transform.localScale = Vector3.one;
        //}
        //else if (moveDelta.x < 0)
        //{
        //    transform.localScale = new Vector3(-1, 1, 1);
        //}

        //transform.Translate(moveDelta * Time.deltaTime);

        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        // 3. 위치 이동 - 위치이동이라 현재 위치도 더해주어야 한다.
        rigid.MovePosition(rigid.position + nextVec);

        // make sure we can move in this direction, by casting a box there first, if the box returns null, we're free to move
        //hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        //if (hit.collider == null)
        //{
        //    // Make this thing move!
        //    transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        //}

        //hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        //if (hit.collider == null)
        //{
        //    // Make this thing move!
        //    transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        //}

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, inputVec, Mathf.Abs(inputVec.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            // Make this thing move!
            transform.Translate(0, inputVec.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, inputVec, Mathf.Abs(inputVec.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            // Make this thing move!
            transform.Translate(inputVec.x * Time.deltaTime, 0, 0);
        }
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    // 프레임이 종료 되기 전 실행되는 생명주기 함수
    private void LateUpdate()
    {
        // 아직 애니메이션을 넣지 못함
        // anim.SetFloat("Speed", inputVec.magnitude);
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
            // 디버깅하는법
            // Debug.Log(spriter.flipX);
        }
    }
}


