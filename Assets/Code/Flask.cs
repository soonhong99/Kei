using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Flask : MonoBehaviour
{
    // 이용되지 않은 선택된 아이템
    // 해당 아이템으로 무언가 작업이 필요할 때 쓰는 것.
    // 아이템 타입(ItemType)이나 Input.GetMouseButtonDown(0)과 같이 입력이 있을 때 
    // item.type == ItemType.BuildingBlock

    [SerializeField] private float playerSpeed;
    [SerializeField] private float originalSpeed = 1f;
    [SerializeField] private Player_Gold player;
    [SerializeField] private Hand hand;
    [SerializeField] private int originalDamageAmount;
    [SerializeField] private float originalPushForce;
    private void Start()
    {
        player = FindObjectOfType<Player_Gold>();
        // handInstance = Hand.instance;
        hand = FindObjectOfType<Hand>();
        (originalDamageAmount, originalPushForce) = OriginalDamage();

    }

    public (int, float) OriginalDamage()
    {
        int damageAmount = hand.damagePoint[hand.weaponLevel];
        float pushForce = hand.pushforce[hand.weaponLevel];

        return (damageAmount, pushForce);
    }

    // speed up
    // 발소리가 커져 몬스터들이 더 몰려온다.
    public void flaskBlue()
    {
        // SpeedUp();
        // 코루틴 함수는 활성화된 게임 옵젝에서만 돌아갈 수 있다.
        StartCoroutine(SpeedBoostCoroutine());
    }

    // speed down
    // 발소리를 줄여주게 한다.
    public void flaskGreen()
    {
        StartCoroutine(SpeedDownCoroutine());
    }

    // power up
    // 그러나 몬스터들한테도 세게 맞는다.
    public void flaskRed()
    {
        // PowerUp();
        StartCoroutine(PowerUpCoroutine());
    }

    // Recovery
    // 모든 물약의 능력들 원상 복귀.
    public void flaskYellow()
    {
        Recovery();
    }

    private IEnumerator SpeedBoostCoroutine()
    {
        // 속도를 1.5배로 증가시킴
        if (player != null)
        {
            player.speed *= 1.25f;

            yield return new WaitForSeconds(10.0f); // 10초 동안 대기

            // 시간이 지난 후 속도를 원래대로 돌려놓음
            player.speed = originalSpeed;
        }
        else
        {
            Debug.LogError("Player_Gold를 찾을 수 없습니다!");
        }
    }

    private IEnumerator SpeedDownCoroutine()
    {
        // 속도를 0.5배로 감소시킴
        if (player != null)
        {
            player.speed *= 0.5f;

            yield return new WaitForSeconds(10.0f); // 10초 동안 대기

            // 시간이 지난 후 속도를 원래대로 돌려놓음
            player.speed = originalSpeed;
        }
        else
        {
            Debug.LogError("Player_Gold를 찾을 수 없습니다!");
        }
    }

    private IEnumerator PowerUpCoroutine()
    {
        // 플레이어 오브젝트 찾기
        if (player != null)
        {
            Debug.Log("now is red");
            // damageAmount와 pushforce를 2배로 증가시킴
            Hand.instance.damagePoint[Hand.instance.weaponLevel] *= 2;
            Hand.instance.pushforce[Hand.instance.weaponLevel] *= 2;

            // 일정 시간 대기
            yield return new WaitForSeconds(10.0f);
            // 시간이 지난 후 원래 값으로 되돌리기
            Hand.instance.damagePoint[Hand.instance.weaponLevel] = originalDamageAmount;
            Hand.instance.pushforce[Hand.instance.weaponLevel] = originalPushForce;
        }
        else
        {
            Debug.LogError("Player_Gold를 찾을 수 없습니다!");
        }
    }

    private void Recovery()
    {
        if (player != null)
        {
            // 원래 능력치로 복구
            Hand.instance.damagePoint[Hand.instance.weaponLevel] = originalDamageAmount;
            Hand.instance.pushforce[Hand.instance.weaponLevel] = originalPushForce;

            player.speed = originalSpeed;
            Debug.Log("All potion effects are reverted.");
        }
        else
        {
            Debug.LogError("Player_Gold를 찾을 수 없습니다!");
        }
    }
}
