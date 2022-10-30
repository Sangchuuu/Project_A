using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")] // �޴�â�� ���� ������ �� �ִ� ����
public class Item : ScriptableObject // ���� ������Ʈ�� ���� �ʿ� ����, �� ��ü�� ������Ʈ�� ��
{
    public enum ItemType { NORMAL, USED, STORY , ETC  }

    public string itemName;
    public ItemType itemtype;
    public Sprite itemImage;
    public GameObject itemPrefab;
    public string iteminfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
