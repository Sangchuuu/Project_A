using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using UnityEngine.EventSystems;
using System.Runtime.CompilerServices;

public class Slot : MonoBehaviour,IPointerClickHandler
{
    public Item item;

    //[Range(0,255)]
    public int itemcount;
    
    public Image itemimage;

    public Image Zoomimage;

    [SerializeField]
    private bool i_zoom = false;

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
        ClearSlot();
    }

    //// Update is called once per frame
    void Update()
    {
        //    AddItem(item);
        //    SetSlotCount(itemcount);
        ImageClose();
        CheckAction();
    }

    private void CheckAction()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if (item != null)
            {
                if (item.itemtype == Item.ItemType.USEAGE)
                {
                    if (item.itemName == "배터리")
                    {
                        if (itemcount > 0)
                        {
                            SetSlotCount(-1);
                        }
                    }
                }
            }

        }
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
        text_ItemName.text = item.itemName;
        text_Iteminfo.text = item.iteminfo;

        if(item.itemtype == Item.ItemType.USEAGE)
        {
            CountImage.SetActive(true);
            text_Count.gameObject.SetActive(true);
            text_Count.text = itemcount.ToString();
        }
        else
        {
            text_Count.text = "0";
            text_Count.gameObject.SetActive(false);
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
            Color color = itemimage.GetComponent<Image>().color;
            color.a = 0.1f;
            itemimage.GetComponent<Image>().color = color;

            /*text_ItemName.text = "????";
            text_Iteminfo.text = "????? ?????? ???????\n ?????????? ????????????";*/
        }
    }

    private void ClearSlot() // 슬롯 삭제(지금 안씀)
    {
        item = null;
        itemcount = 0;
        itemimage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        CountImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventdata) // 마우스 혹은 터치 입력, 드래그 등의 데이터 정보를 담고 있음
    {
        if(eventdata.button == PointerEventData.InputButton.Left) // 마우스 우클릭 데이터 들어올때
        {
            if(item != null)
            {
                if(item.itemtype == Item.ItemType.USEAGE)
                {
                    if (itemcount > 0)
                    {
                        SetSlotCount(-1);
                    }
                }
                else if (item.itemtype == Item.ItemType.STORY)
                {
                    /*RectTransform rectTran = gameObject.GetComponent<RectTransform>();

                    rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1200);
                    rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 700);

                    Vector3 Position = itemimage.transform.localPosition;

                    Position.x = 0;
                    Position.y = 0;
                    itemimage.transform.localPosition = Position;*/

                    i_zoom = true;
                    Zoomimage.sprite = item.itemImage;
                    Zoomimage.gameObject.SetActive(true);
                }


            }

        }
    }

    void ImageClose()
    {
        if(i_zoom == true && Input.GetKeyDown(KeyCode.E))
        {
            Zoomimage.gameObject.SetActive(false);
        }
    }

}
