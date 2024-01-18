using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec, pointerInput;
    public float speed;
    public Vector2 PointerInput => pointerInput;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    [SerializeField]
    private InputActionReference move, fire, pointerPosition;

    private WeaponParent weaponParent;

    void Awake()
    {
        // ������Ʈ���� ������Ʈ�� �������� �Լ�
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        weaponParent = GetComponentInChildren<WeaponParent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // inputValue Ÿ���� �Ű����� �ۼ�
    void OnMove(InputValue value)
    {
        // ������ inputvalue -> vector2�� ��ȯ�ϱ�
        inputVec = value.Get<Vector2>();
    }

    // ���� ���� �����Ӹ��� ȣ��Ǵ� �����ֱ� �Լ�
    void FixedUpdate()
    {
        // �밢���� ���� �ӵ�, �����ӿ� ���� �ӵ��� �޶����� �ʰ� ��
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;

        // 3. ��ġ �̵�
        rigid.MovePosition (rigid.position + nextVec);

        pointerInput = GetPointerInput();
        weaponParent.Pointerposition = pointerInput;
        
    }

    // �������� ���� �Ǳ� �� ����Ǵ� �����ֱ� �Լ�
    void LateUpdate()
    {
        // ������ ���� ���� �ֱ�
        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint (mousePos);
    }
}
