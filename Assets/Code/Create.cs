using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// create �� �ƴ϶� crate(��������)
public class Create : Fighter
{
    AudioManager audioManager;
    private float lastSoundTime = -1; // ������ ���� ��� �ð��� �����ϱ� ���� ����


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        
    }

    protected override void Death()
    {
        GetComponent<DropBag>().InstantiateDrop(transform.position);

        // �׾��� �� player���� �������� ������, �����Կ� �ش� �������� ���� �� �ְ� �ؾߵ�.
        // �������� ������ �ϴ� ���� loot �ڵ忡 �ִ�.
        Destroy(gameObject);
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if ((Time.time - lastSoundTime) >= 0.5f)
        {
            audioManager.PlaySFX(audioManager.BoxAttack);
            // deltatime�� ���� ������ ������Ʈ�� �� ������ �����°Ű�, �׳� time�� ���� �ð��� �ǹ��Ѵ�.
            lastSoundTime = Time.time; // ������ ���� ��� �ð� ������Ʈ
        }

        base.ReceiveDamage(dmg);
    }
}
