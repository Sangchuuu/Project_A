using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] enemyZones;
    [SerializeField]
    private GameObject[] monsters;
    [SerializeField]
    private bool isTutorial;
    [SerializeField]
    private int maxSpawnMonster;
    [SerializeField]
    private int curSpawnMonster;
    [SerializeField]
    private Transform[] bigZones;
    [SerializeField]
    private GameObject[] bigRooms;
    List<Transform> bigList = new List<Transform>();
    List<GameObject> bigRoomList = new List<GameObject>();

    [SerializeField]
    public GameObject InventoryRayout;
    [SerializeField]
    private bool inventoryopen;




    //private static GameManager _instance;
    /*
    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else if(_instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    */

    void Start()
    {
        RandomList();
        BigRoomSpawn();

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
            if (inventoryopen == true)
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

    void RandomList()
    {
        bigList.AddRange(bigZones);
        bigRoomList.AddRange(bigRooms);
    }

    void BigRoomSpawn()
    {
        if(bigList.Count != 0 && bigRoomList.Count != 0)
        {
            for (int i = 0; i < 3; i++)
            {
                int ranPoint = Random.Range(0, bigList.Count); 
                int ranRoom = Random.Range(0, bigRoomList.Count);
                int ranAngle = Random.Range(0, 2);
                switch (ranAngle)
                {
                    case 0:
                        bigList[ranPoint].Rotate(Vector3.up * 180);
                        break;
                    case 1:                        
                        break;
                }
                Instantiate(bigRoomList[ranRoom],
                            bigList[ranPoint].position,
                            bigList[ranPoint].rotation);
                bigList.RemoveAt(ranPoint);
                bigRoomList.RemoveAt(ranRoom);
            }
        }        
    }

    public void OpenInventoryUI(bool open)
    {
        if (open == true)
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
