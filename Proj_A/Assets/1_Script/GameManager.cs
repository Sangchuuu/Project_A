using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //몬스터 소환
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
    
    //방 소환
    [SerializeField]
    private bool mainStage;
    [SerializeField]
    private Transform[] bigRoomZones;
    [SerializeField]
    private GameObject[] bigRooms;
    [SerializeField]
    private Transform[] middleRoomZones;
    [SerializeField]
    private GameObject[] middleRooms;
    [SerializeField]
    private Transform[] smallRoomZones;
    [SerializeField]
    private GameObject[] smallRooms;





    public static GameManager _instance;
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
    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else if(_instance != this)
        {
            Destroy(gameObject);
        }
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
        if (mainStage)
        {
            BigRoom();
        }
            
    } 

    IEnumerator SpawnEnemy()
    {
        //소환될 몬스터
        int ranEnemy = Random.Range(0, 2);
        //소환될 위치
        int ranPoint = Random.Range(0, 2);
        Instantiate(monsters[ranEnemy],
                    enemyZones[ranPoint].position,
                    enemyZones[ranPoint].rotation);
        curSpawnMonster++;

        yield return null;
    }
    
    void BigRoom()
    {        
        for(int x = 0; x < bigRooms.Length; ++x)
        {
            int ranRoom = Random.Range(0, bigRooms.Length);
            int ranPoint = Random.Range(0, bigRoomZones.Length);
            Instantiate(bigRooms[ranRoom],
                        bigRoomZones[ranPoint].position,
                        bigRoomZones[ranPoint].rotation);
        }

    }

    
    

}
