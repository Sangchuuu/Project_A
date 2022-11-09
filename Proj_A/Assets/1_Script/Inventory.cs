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
        //�����̶�� ��ü�� ����ִ� �ڽ� ������Ʈ�� �����´�
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        if (Item.ItemType.USEAGE == _item.itemtype)
        {
            for (int i = 0; i < slots.Length; i++) // ���� ���̸�ŭ �ݺ�
            {
                if (slots[i].item != null)  // null �̶�� slots[i].item.itemName �� �� ��Ÿ�� ���� ����
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count); // ī��Ʈ ��ŭ ������ ���Կ� ���� �߰�
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++) // ������ ��� ���� ������ �˻�
        {
            if (slots[i].item == null) // ������ ����ִٸ�
            {
                slots[i].AddItem(_item, _count); // ī��Ʈ��ŭ �������� �迭�� �߰�
                return;
            }
        }
    }
}
