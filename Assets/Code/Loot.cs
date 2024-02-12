using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ǰ�̶�� ��
public class Loot : MonoBehaviour
{
    // serializefield: ���� �����ؾ� �ϴ� private ���� ����ȭ(�ν�����â���� ������ ���� �����ִ� ����)
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

    // �÷��̾ �ε����� �ش� �������� �Դ� �˰���
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

    // �ڷ�ƾ�� �����ϱ� ���ؼ��� IEnumerator�� ��� �ϰ�, yield return ���� ��� �Ѵ�.
    // �Լ��� ���� �������� �ʾƵ� ������ ��� ���� ä�� ������� �ѱ� �� �ִ�.
    // �ڽ������� �������� ����Ǵ� ����
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
