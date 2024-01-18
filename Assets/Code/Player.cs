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
        // 오브젝트에서 컴포넌트를 가져오는 함수
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        weaponParent = GetComponentInChildren<WeaponParent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // inputValue 타입의 매개변수 작성
    void OnMove(InputValue value)
    {
        // 형식을 inputvalue -> vector2로 변환하기
        inputVec = value.Get<Vector2>();
    }

    // 물리 연산 프레임마다 호출되는 생명주기 함수
    void FixedUpdate()
    {
        // 대각선도 같은 속도, 프레임에 따라 속도가 달라지지 않게 함
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;

        // 3. 위치 이동
        rigid.MovePosition (rigid.position + nextVec);

        pointerInput = GetPointerInput();
        weaponParent.Pointerposition = pointerInput;
        
    }

    // 프레임이 종료 되기 전 실행되는 생명주기 함수
    void LateUpdate()
    {
        // 벡터의 길이 값만 넣기
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
