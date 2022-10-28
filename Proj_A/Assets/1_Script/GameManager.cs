using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    GameObject InventoryRayout;

    bool inventoryopen;

    private static GameManager instance;

    public static GameManager GetInstance()
    {
        if(instance == null)
        {
            instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            Debug.Log("no singleton obj");
        }
        return instance;
        
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

       
        if (isTutorial && curSpawnMonster < maxSpawnMonster)
        {
            StartCoroutine(SpawnEnemy());
        }

        //DontDestroyOnLoad(instance);
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
        InventoryRayout.gameObject.SetActive(open);    
    }

}
