using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGainCtrl : MonoBehaviour
{
    [SerializeField]
    private float range; // ������ ������ ������ �ִ� �Ÿ�

    private bool pickupActivated = false; // ������ ���� ���� ����

    private RaycastHit hitInfo; // �浹ü ���� ����

    [SerializeField]
    private LayerMask layerMask; // Ư�� ���̾ ���� �� ����

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
        actionText.text = hitInfo.transform.GetComponent<ItemPickup>().item.itemName + "ȹ��" + "<color=yellow>" + "(E)" + "</color>";

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
                Debug.Log(hitInfo.transform.GetComponent<ItemPickup>().item.itemName + "ȹ���߽��ϴ�");
                theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickup>().item);
                Destroy(hitInfo.transform.gameObject);
                ItemInfoDisappear();
            }

        }


    }


}
