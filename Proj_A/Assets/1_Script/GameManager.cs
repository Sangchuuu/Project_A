using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject InventoryRayout;
    [SerializeField]
    private bool inventoryopen;




    
    private static GameManager _instance;
    
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
    
    

    void Start()
    {
        

        inventoryopen = false;
        InventoryRayout.gameObject.SetActive(inventoryopen);
        
    }

    // Update is called once per frame
    void Update()
    {
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

    public void TestStage()
    {
        SceneManager.LoadScene("Map 1F_002_Test_CCH");
    }



}
