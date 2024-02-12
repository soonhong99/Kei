using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DropBag : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<DropItem> dropList = new List<DropItem>();

    DropItem GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101);

        List<DropItem> possibleItems = new List<DropItem>();

        foreach(DropItem item in dropList)
        {
            if (randomNumber <= item.dropChance)
            {
                possibleItems.Add(item);
            }
        }

        if (possibleItems.Count > 0)
        {
            DropItem droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        Debug.Log(possibleItems);
        return null;
    }

    // 드랍 아이템을 복제
    public void InstantiateDrop(Vector3 spawnPosition)
    {
        DropItem droppedItem = GetDroppedItem();
        if (droppedItem != null)
        {
            Debug.Log(droppedItem);
            GameObject DropGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
            //DropGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.image;

            // 전리품에 대한 이미지 갖고 오기
            DropGameObject.GetComponent<Loot>().Initialize(droppedItem);

            // 이걸 쓰면 아이템이 어디론가 날라감
            //float dropForce = 300f;
            //Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            //DropGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
        }
    }
}
