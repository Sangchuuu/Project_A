using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] enemyZones; // 
    [SerializeField]
    private GameObject[] monsters; // ���� ������ ���� �迭
    [SerializeField]
    private bool isTutorial; // Ʃ�丮�� ������ ����
    [SerializeField]
    private int maxSpawnMonster; // �ִ� ���� ��������Ƚ��
    [SerializeField]
    private int curSpawnMonster; // ���� ������ ����    
    [SerializeField]
    public GameObject InventoryRayout;

    [SerializeField]
    private bool inventoryopen;

    public static GameManager instance;

    


    //public List <GameObject> ItemObjectList;

    [SerializeField]
    //private bool itemgain; // ������ ó�� ȹ�濩��

    //public static GameManager GetInstance()
    //{
    //    if(instance == null)
    //    {            
    //        Debug.Log("no singleton obj");
    //    }
    //    return instance;       
    //}
    private void Awake()
    {
        if (instance == null)
        {           
            instance = this;
            DontDestroyOnLoad(instance);
        }
        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
        else
        {
            Destroy(this.gameObject);
        }
        // �Ʒ��� �Լ��� ����Ͽ� ���� ��ȯ�Ǵ��� ����Ǿ��� �ν��Ͻ��� �ı����� �ʴ´�.
        
    }


    void Start()
    {       
        inventoryopen = false;      
        InventoryRayout.gameObject.SetActive(inventoryopen);
    }

    // Update is called once per frame
    void Update()
    {

       
        if (isTutorial && curSpawnMonster < maxSpawnMonster)
        {
            StartCoroutine(SpawnEnemy());
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(inventoryopen == true)
            {
                inventoryopen = false;
            }
            else if (inventoryopen == false)
            {
                inventoryopen = true;
            }
            OpenInventoryUI(inventoryopen);
        }
    } 

    IEnumerator SpawnEnemy()
    {
        //��ȯ�� ����
        int ranEnemy = Random.Range(0, 3);
        //��ȯ�� ��ġ
        int ranPoint = Random.Range(0, 4);
        Instantiate(monsters[ranEnemy],
                    enemyZones[ranPoint].position,
                    enemyZones[ranPoint].rotation);
        curSpawnMonster++;

        yield return null;
    }
    
    public void OpenInventoryUI(bool open)
    {
        if(open == true)
        {                      
            InventoryRayout.gameObject.SetActive(open);
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;              
            InventoryRayout.gameObject.SetActive(open);
        }       
    }

}
