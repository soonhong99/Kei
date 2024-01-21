using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Collidable 스크립트 상속
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
            GameManager.instance.ShowText("+" + pesosAmount + "pesos!", 25, Color.yellow, transform.position, Vector3.up * 25, 1.5f);
            Debug.Log("Grant " + pesosAmount + "pesos!");
        }
    }
}
