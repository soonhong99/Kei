using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Collidable ��ũ��Ʈ ���
public class Chest : Collectable
{
    public Sprite emptyChest;
    public int pesosAmount;
    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            Debug.Log("Grant " + pesosAmount + "pesos!");
        }
    }
}
