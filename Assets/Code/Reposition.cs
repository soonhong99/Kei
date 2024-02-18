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

    // 타일하고 충돌을 하면, 해당 함수가 호출되는데, 지금 나는 ?
    // 몬스터를 때릴 때 콜라이더를 지우는 그런 게 있어가지고 그 때만 호출되는 성격인 것 같다.
    // 충돌을 하면 호출되는 함수
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
