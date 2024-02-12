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

    // ���� �� �ִ� �������ΰ�? stackable
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
