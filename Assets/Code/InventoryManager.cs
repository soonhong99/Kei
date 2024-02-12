using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // 다른 스크립트가 인벤토리 관리자의 메서드를 사용할 것이기 때문에 엑세스 할 수 있는 항목의
    // 인스턴스를 만들어야 한다.
    public static InventoryManager instance;

    // 처음 시작할 때 주어지는 아이템
    public DropItem[] startItems;

    public int maxStackedItems = 5;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    // 처음에는 선택된 슬롯이 아무것도 없다는 뜻
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
            // string 값을 정수로 변환, number는 결과값
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
        // 같은 아이템이 맥시멈 개수보다 적은 것이 있는지 체크하기
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            // 하위에 있는 아이템이 항상 같이 움직이므로 슬롯안에 있는 아이템을 임시 변수안에 넣어놓는다.
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems &&
                itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        // 빈 슬롯을 찾는다
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            // 하위에 있는 아이템이 항상 같이 움직이므로 슬롯안에 있는 아이템을 임시 변수안에 넣어놓는다.
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
        // 무엇가로부터 생성된것에 집중: 인스턴스화
        // 실체가 있다는 것에 집중: object
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
