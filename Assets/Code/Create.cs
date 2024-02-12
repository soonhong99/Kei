using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// create 가 아니라 crate(나무상자)
public class Create : Fighter
{
    AudioManager audioManager;
    private float lastSoundTime = -1; // 마지막 사운드 재생 시간을 추적하기 위한 변수


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        
    }

    protected override void Death()
    {
        GetComponent<DropBag>().InstantiateDrop(transform.position);

        // 죽었을 때 player에게 아이템이 들어오고, 퀵슬롯에 해당 아이템이 생길 수 있게 해야됨.
        // 아이템이 들어오게 하는 것이 loot 코드에 있다.
        Destroy(gameObject);
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if ((Time.time - lastSoundTime) >= 0.5f)
        {
            audioManager.PlaySFX(audioManager.BoxAttack);
            // deltatime을 쓰면 물리적 업데이트가 될 때마다 나오는거고, 그냥 time은 실제 시간을 의미한다.
            lastSoundTime = Time.time; // 마지막 사운드 재생 시간 업데이트
        }

        base.ReceiveDamage(dmg);
    }
}
