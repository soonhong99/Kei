using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // �ٸ� ��ũ��Ʈ�� �κ��丮 �������� �޼��带 ����� ���̱� ������ ������ �� �� �ִ� �׸���
    // �ν��Ͻ��� ������ �Ѵ�.
    public static InventoryManager instance;

    // ó�� ������ �� �־����� ������
    public DropItem[] startItems;

    public int maxStackedItems = 5;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    // ó������ ���õ� ������ �ƹ��͵� ���ٴ� ��
    int selectedSlot = -1;

    [SerializeField] private Flask flask;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ChangeSelectedSlot(0);
        foreach (var item in startItems)
        {
            AddItem(item);
        }
    }

    private void Update()
    {
        if (Input.inputString != null)
        {
            // string ���� ������ ��ȯ, number�� �����
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 6)
            {
                ChangeSelectedSlot(number - 1);
                GetSelectedItem(true);
            }
        }
    }

    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }

        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(DropItem item)
    {
        // ���� �������� �ƽø� �������� ���� ���� �ִ��� üũ�ϱ�
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            // ������ �ִ� �������� �׻� ���� �����̹Ƿ� ���Ծȿ� �ִ� �������� �ӽ� �����ȿ� �־���´�.
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems &&
                itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        // �� ������ ã�´�
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            // ������ �ִ� �������� �׻� ���� �����̹Ƿ� ���Ծȿ� �ִ� �������� �ӽ� �����ȿ� �־���´�.
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }

    void SpawnNewItem(DropItem item, InventorySlot slot)
    {
        // �������κ��� �����ȰͿ� ����: �ν��Ͻ�ȭ
        // ��ü�� �ִٴ� �Ϳ� ����: object
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public DropItem GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            DropItem item = itemInSlot.item;
            if (use == true)
            {
                itemInSlot.count--;
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }

                if (item.dropName == "flaskBlue")
                {
                    flask.flaskBlue();
                }

                else if (item.dropName == "flaskGreen")
                {
                    flask.flaskGreen();
                }

                else if (item.dropName == "flaskRed")
                {
                    flask.flaskRed();
                }

                else if (item.dropName == "flaskYellow")
                {
                    flask.flaskYellow();
                }
            }
            return item;
        }
        return null;
    }
}
