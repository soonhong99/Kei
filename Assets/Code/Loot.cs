using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 전리품이라는 뜻
public class Loot : MonoBehaviour
{
    // serializefield: 자주 변경해야 하는 private 변수 직렬화(인스펙터창에서 수정한 것이 남아있는 이유)
    [SerializeField] private float moveSpeed;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private BoxCollider2D boxcollider;
    [SerializeField] private bool triggered = false;
    private DropItem item;
    private int count = 0;
    public void Initialize(DropItem item)
    {
        this.item = item;
        sr.sprite = item.image;
    }

    // 플레이어가 부딪히면 해당 아이템을 먹는 알고리즘
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.name == "Player")
        {         
            bool canAdd = InventoryManager.instance.AddItem(item);
            count++;
            Debug.Log("trigger count: " + count);
            if (canAdd)
            {
                StartCoroutine(MoveAndCollect(other.transform));
            }
            triggered = true;
        }
    }

    // 코루틴을 수행하기 위해서는 IEnumerator를 써야 하고, yield return 형을 써야 한다.
    // 함수의 끝에 도달하지 않아도 실행을 잠깐 멈춘 채로 제어권을 넘길 수 있다.
    // 자신쪽으로 아이템이 흡수되는 현상
    private IEnumerator MoveAndCollect(Transform target)
    {
        Destroy(boxcollider);

        while (transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            yield return 0;
        }

        Destroy(gameObject);
    }
}
