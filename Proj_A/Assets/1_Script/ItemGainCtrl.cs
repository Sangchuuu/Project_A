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
        //AimCenter();
        CheckItem();
        TryAction();
    }

    /*private void AimCenter()
    {
        Ray ray = Camera.main.ScreenPointToRay(ScreenCenter);
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);
    }*/

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
                //Gizmos.DrawRay(transform.position, transform.forward * hitInfo.distance);
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
        actionText.text = hitInfo.transform.GetComponent<ItemPickup>().item.itemName + "ȹ��" + "<color=yellow>" + "(RClick)" + "</color>";

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
