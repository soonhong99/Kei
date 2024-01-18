//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class Player : MonoBehaviour
//{
//    public Vector2 inputVec;
//    public float speed;
//    private Vector3 moveDelta;
//    private RaycastHit2D hit;

//    private BoxCollider2D boxCollider;
//    Rigidbody2D rigid;
//    Animator anim;
//    SpriteRenderer spriter;

//    // ��ó���� �ѹ� ȣ��Ǵ� �Լ�
//    private void Awake()
//    {
//        spriter = GetComponent<SpriteRenderer>();
//        rigid = GetComponent<Rigidbody2D>();
//        boxCollider = GetComponent<BoxCollider2D>();
//        anim = GetComponent<Animator>();
//    }

//    private void Start()
//    {
//        boxCollider = GetComponent<BoxCollider2D>();
//    }


//    private void FixedUpdate()
//    {
//        //float x = Input.GetAxisRaw("Horizontal");
//        //float y = Input.GetAxisRaw("Vertical");

//        //moveDelta = new Vector3(x,y,0);

//        //if (moveDelta.x > 0)
//        //{
//        //    transform.localScale = Vector3.one;
//        //}
//        //else if (moveDelta.x < 0)
//        //{
//        //    transform.localScale = new Vector3(-1, 1, 1);
//        //}

//        //transform.Translate(moveDelta * Time.deltaTime);

//        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
//        // 3. ��ġ �̵� - ��ġ�̵��̶� ���� ��ġ�� �����־�� �Ѵ�.
//        rigid.MovePosition(rigid.position + nextVec);

//        // make sure we can move in this direction, by casting a box there first, if the box returns null, we're free to move
//        //hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

//        //if (hit.collider == null)
//        //{
//        //    // Make this thing move!
//        //    transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
//        //}

//        //hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
//        //if (hit.collider == null)
//        //{
//        //    // Make this thing move!
//        //    transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
//        //}

//        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, nextVec, Mathf.Abs(nextVec.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
//        if (hit.collider == null)
//        {
//            // Make this thing move!
//            transform.Translate(0, nextVec.y * Time.deltaTime, 0);
//        }

//        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, nextVec, Mathf.Abs(nextVec.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
//        if (hit.collider == null)
//        {
//            // Make this thing move!
//            transform.Translate(nextVec.x * Time.deltaTime, 0, 0);
//        }
//    }

//    void OnMove(InputValue value)
//    {
//        inputVec = value.Get<Vector2>();
//    }

//    // �������� ���� �Ǳ� �� ����Ǵ� �����ֱ� �Լ�
//    private void LateUpdate()
//    {
//        // ���� �ִϸ��̼��� ���� ����
//        // anim.SetFloat("Speed", inputVec.magnitude);
//        if (inputVec.x != 0)
//        {
//            spriter.flipX = inputVec.x < 0;
//            // ������ϴ¹�
//            // Debug.Log(spriter.flipX);
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// MonoBehaviour: ���� ���� ������ �ʿ��� �͵��� ���� Ŭ����
public class Player : MonoBehaviour
{
    // public: �ٸ� ��ũ��Ʈ(inspector)���� �����Ѵ�
    public Vector2 inputVec;
    public float speed;
    // �˻� Ŭ���� Ÿ�� ���� ���� �� �ʱ�ȭ
    // ���� ���� ��ũ��Ʈ�� ������Ʈ�� �����ϰ� ����Ѵ�.
    // public Scanner scanner;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    // ������ �� �ѹ��� ����Ǵ� �����ֱ� Awake
    private void Awake()
    {
        // GetComponent: ������Ʈ���� ������Ʈ�� �������� �Լ� �� �ʱ�ȭ ��Ű��
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        // scanner = GetComponent<Scanner>();
    }

    // ���� ���� �����Ӹ��� ȣ��Ǵ� �����ֱ� �Լ�
    private void FixedUpdate()
    {
        // 1. ���� �ش�
        // rigid.AddForce(inputVec);

        // 2. �ӵ� ����
        // rigid.velocity = inputVec;

        // �ӽ÷� ���� ������ �� Vector2 ���� �߰�
        // fixedDeltaTime: ���� ������ �ϳ��� �Һ��� �ð�
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;

        // 3. ��ġ �̵� - ��ġ�̵��̶� ���� ��ġ�� �����־�� �Ѵ�.
        rigid.MovePosition(rigid.position + nextVec);
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    // �������� ���� �Ǳ� �� ����Ǵ� �����ֱ� �Լ�
    private void LateUpdate()
    {
        // anim.SetFloat("Speed", inputVec.magnitude);
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }
}
