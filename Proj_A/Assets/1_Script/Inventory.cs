using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;

    [SerializeField]
    private GameObject InventoryBase;
    [SerializeField]
    private GameObject SlotParent;
    [SerializeField]
    private Slot[] slots; // 

    // Start is called before the first frame update
    void Start()
    {
        slots = SlotParent.GetComponentsInChildren<Slot>();
        //슬롯이라는 객체가 들어있는 자식 오브젝트를 가져온다
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        if (Item.ItemType.USEAGE == _item.itemtype)
        {
            for (int i = 0; i < slots.Length; i++) // 슬롯 길이만큼 반복
            {
                if (slots[i].item != null)  // null 이라면 slots[i].item.itemName 할 때 런타임 에러 나서
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count); // 카운트 만큼 아이템 슬롯에 개수 추가
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++) // 슬롯이 비어 있을 때까지 검사
        {
            if (slots[i].item == null) // 슬롯이 비어있다면
            {
                slots[i].AddItem(_item, _count); // 카운트만큼 아이템을 배열에 추가
                return;
            }
        }
    }
}
