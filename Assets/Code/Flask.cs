using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Flask : MonoBehaviour
{
    // �̿���� ���� ���õ� ������
    // �ش� ���������� ���� �۾��� �ʿ��� �� ���� ��.
    // ������ Ÿ��(ItemType)�̳� Input.GetMouseButtonDown(0)�� ���� �Է��� ���� �� 
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
    // �߼Ҹ��� Ŀ�� ���͵��� �� �����´�.
    public void flaskBlue()
    {
        // SpeedUp();
        // �ڷ�ƾ �Լ��� Ȱ��ȭ�� ���� ���������� ���ư� �� �ִ�.
        StartCoroutine(SpeedBoostCoroutine());
    }

    // speed down
    // �߼Ҹ��� �ٿ��ְ� �Ѵ�.
    public void flaskGreen()
    {
        StartCoroutine(SpeedDownCoroutine());
    }

    // power up
    // �׷��� ���͵����׵� ���� �´´�.
    public void flaskRed()
    {
        // PowerUp();
        StartCoroutine(PowerUpCoroutine());
    }

    // Recovery
    // ��� ������ �ɷµ� ���� ����.
    public void flaskYellow()
    {
        Recovery();
    }

    private IEnumerator SpeedBoostCoroutine()
    {
        // �ӵ��� 1.5��� ������Ŵ
        if (player != null)
        {
            player.speed *= 1.25f;

            yield return new WaitForSeconds(10.0f); // 10�� ���� ���

            // �ð��� ���� �� �ӵ��� ������� ��������
            player.speed = originalSpeed;
        }
        else
        {
            Debug.LogError("Player_Gold�� ã�� �� �����ϴ�!");
        }
    }

    private IEnumerator SpeedDownCoroutine()
    {
        // �ӵ��� 0.5��� ���ҽ�Ŵ
        if (player != null)
        {
            player.speed *= 0.5f;

            yield return new WaitForSeconds(10.0f); // 10�� ���� ���

            // �ð��� ���� �� �ӵ��� ������� ��������
            player.speed = originalSpeed;
        }
        else
        {
            Debug.LogError("Player_Gold�� ã�� �� �����ϴ�!");
        }
    }

    private IEnumerator PowerUpCoroutine()
    {
        // �÷��̾� ������Ʈ ã��
        if (player != null)
        {
            Debug.Log("now is red");
            // damageAmount�� pushforce�� 2��� ������Ŵ
            Hand.instance.damagePoint[Hand.instance.weaponLevel] *= 2;
            Hand.instance.pushforce[Hand.instance.weaponLevel] *= 2;

            // ���� �ð� ���
            yield return new WaitForSeconds(10.0f);
            // �ð��� ���� �� ���� ������ �ǵ�����
            Hand.instance.damagePoint[Hand.instance.weaponLevel] = originalDamageAmount;
            Hand.instance.pushforce[Hand.instance.weaponLevel] = originalPushForce;
        }
        else
        {
            Debug.LogError("Player_Gold�� ã�� �� �����ϴ�!");
        }
    }

    private void Recovery()
    {
        if (player != null)
        {
            // ���� �ɷ�ġ�� ����
            Hand.instance.damagePoint[Hand.instance.weaponLevel] = originalDamageAmount;
            Hand.instance.pushforce[Hand.instance.weaponLevel] = originalPushForce;

            player.speed = originalSpeed;
            Debug.Log("All potion effects are reverted.");
        }
        else
        {
            Debug.LogError("Player_Gold�� ã�� �� �����ϴ�!");
        }
    }
}
