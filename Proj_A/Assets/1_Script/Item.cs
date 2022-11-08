using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")] // 메뉴창을 통해 생성할 수 있는 로직
public class Item : ScriptableObject // 게임 오브젝트에 붙일 필요 없고, 그 자체가 컴포넌트가 됨
{
    public enum ItemType { NORMAL, USEAGE, STORY , ETC  }

    public string itemName;
    public ItemType itemtype;
    public Sprite itemImage;
    public GameObject itemPrefab;
    public string iteminfo;
    //public int itemuse;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
