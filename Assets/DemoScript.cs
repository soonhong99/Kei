using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public DropItem[] itemsToPickUp;

    public void PickUpItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickUp[id]);
        if (result == true)
        {
            Debug.Log("Item added");
        }
        else
        {
            Debug.Log("Item not added");
        }
    }

    public void GetSelectedItem()
    {
        DropItem receiveditem = inventoryManager.GetSelectedItem(false);
        if (receiveditem != null)
        {
            Debug.Log("Received item: " + receiveditem);
        }
        else
        {
            Debug.Log("No item received");
        }
    }

    public void UseSelectedItem()
    {
        DropItem receiveditem = inventoryManager.GetSelectedItem(true);
        if (receiveditem != null)
        {
            Debug.Log("Used item: " + receiveditem);
        }
        else
        {
            Debug.Log("No item received");
        }
    }
}
