using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DropItem : ScriptableObject
{
    public Sprite image;
    public string dropName;
    public int dropChance;
    public ActionType actionType;
    public ItemType itemType;

    // 쌓을 수 있는 구조물인가? stackable
    public bool stackable = true;
    //public DropItem(string dropName, int dropChance)
    //{
    //    this.dropName = dropName;
    //    this.dropChance = dropChance;
    //}
}

public enum ItemType
{
    Coin,
    Portion,
}

public enum ActionType
{
    SpeedUp,
    SpeedDown,
    PowerUp,
    AttackSpeedUp,
    AttackSpeedDown,
    CoinUp,
}
