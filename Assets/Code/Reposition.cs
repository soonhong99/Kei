using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    // Ÿ���ϰ� �浹�� �ϸ�, �ش� �Լ��� ȣ��Ǵµ�, ���� ���� ?
    // ���͸� ���� �� �ݶ��̴��� ����� �׷� �� �־���� �� ���� ȣ��Ǵ� ������ �� ����.
    // �浹�� �ϸ� ȣ��Ǵ� �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 playerDir = GameManager.instance.player.inputVec;

        Debug.Log("player direction: " + playerDir);

        switch (transform.tag)
        {
            case "Enemy":
                if (coll.enabled)
                {
                    transform.Translate(playerDir * 1.5f + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.3f, 0.3f), 0f));
                }

                break;

        }
    }
}
