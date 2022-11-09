using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Slot : MonoBehaviour
{
    public Item item;
    public int itemcount;
    public Image itemimage;

    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject CountImage;

    [SerializeField]
    private Text text_ItemName;
    [SerializeField]
    private Text text_Iteminfo;


    // Start is called before the first frame update
    void Start()
    {
        itemimage.sprite = item.itemImage;
        text_ItemName.text = item.itemName;
        text_Iteminfo.text = item.iteminfo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetColor(float _alpha)
    {
        Color color = itemimage.color; // 아이템 이미지의 색상값
        color.a = _alpha; // 알파값 설정
        itemimage.color = color;
    }

    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemcount = _count;
        itemimage.sprite = item.itemImage;

        if(item.itemtype == Item.ItemType.USEAGE)
        {
            CountImage.SetActive(true);
            text_Count.text = itemcount.ToString();
        }
        else
        {
            text_Count.text = "0";
            CountImage.SetActive(false);
        }

        SetColor(1);
    }

    public void SetSlotCount(int _count)
    {
        itemcount += _count;
        text_Count.text = itemcount.ToString();

        if (itemcount <= 0)
        {
            itemimage.color = Color.gray;
        }
    }

    /*private void ClearSlot() // 슬롯 삭제(지금 안씀)
    {
        item = null;
        itemcount = 0;
        itemimage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        CountImage.SetActive(false);
    }*/

}
