using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        // �Ʒ��� �Լ��� ����Ͽ� ���� ��ȯ�Ǵ��� ����Ǿ��� �ν��Ͻ��� �ı����� �ʴ´�.
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
        InventoryRayout.gameObject.SetActive(open);    
    }

}
