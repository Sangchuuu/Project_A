using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGainCtrl : MonoBehaviour
{
    [SerializeField]
    private float range; // 아이템 습득이 가능한 최대 거리

    private bool pickupActivated = false; // 아이템 습득 가능 여부

    private RaycastHit hitInfo; // 충돌체 정보 저장

    [SerializeField]
    private LayerMask layerMask; // 특정 레이어를 감지 후 동작

    [SerializeField]
    private Text actionText;

    [SerializeField]
    private Inventory theInventory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckItem();
            CanPickUp();
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(this.transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            Debug.Log("Ray: ", hitInfo.collider.gameObject);
            if (hitInfo.collider.gameObject.tag == "Item")
            {
               
                ItemInfoAppear();
            }


        }
        else
            ItemInfoDisappear();

       // Debug.Log("Ray: ", hitInfo.collider.gameObject);

    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<ItemPickup>().item.itemName + "획득" + "<color=yellow>" + "(E)" + "</color>";

    }

    private void ItemInfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);

    }

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if(hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickup>().item.itemName + "획득했습니다");
                theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickup>().item);
                Destroy(hitInfo.transform.gameObject);
                ItemInfoDisappear();
            }

        }


    }


}
