using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Enemy_Gold : MonoBehaviour
{
    public float speed;
    public int xpValue = 1;

    bool isLive = true;

    Rigidbody2D rigid;
    SpriteRenderer spriter;

    private Transform playerTransform;

    private void Awake()
    {

        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        playerTransform = GameObject.Find("Player").transform;
    }

    private void FixedUpdate()
    {
        if (!isLive)
        {
            return;
        }

        Vector2 playerPosition = new Vector2(playerTransform.position.x, playerTransform.position.y);
        Vector2 enemyPosition = new Vector2(rigid.position.x, rigid.position.y);
        Vector2 distanceVec = playerPosition - enemyPosition;

        Vector2 dirVec = distanceVec.normalized;
        Vector2 nextVec = dirVec * speed * Time.fixedDeltaTime;

        // nextVec ������ ���͸� �Űܶ�.
        rigid.MovePosition(rigid.position + nextVec);

        // �����ӵ��� �̵��� ������ ���� �ʵ��� �ӵ� ����
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!isLive)
        {
            return;
        }

        Vector2 playerPosition = new Vector2(playerTransform.position.x, playerTransform.position.y);

        spriter.flipX = playerPosition.x < rigid.position.x;
    }
}
