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
    private GameObject[] monsters; // 몬스터 데이터 저장 배열
    [SerializeField]
    private bool isTutorial; // 튜토리얼 진행중 여부
    [SerializeField]
    private int maxSpawnMonster; // 최대 몬스터 스폰가능횟수
    [SerializeField]
    private int curSpawnMonster; // 현재 스폰된 몬스터    
    [SerializeField]
    public GameObject InventoryRayout;

    [SerializeField]
    private bool inventoryopen;

    public static GameManager instance;

    


    //public List <GameObject> ItemObjectList;

    [SerializeField]
    //private bool itemgain; // 아이템 처음 획득여부

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
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else
        {
            Destroy(this.gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        
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
        //소환될 몬스터
        int ranEnemy = Random.Range(0, 3);
        //소환될 위치
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
