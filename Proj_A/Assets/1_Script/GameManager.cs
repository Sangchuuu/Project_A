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

    void Start()
    {
        bigList.AddRange(bigZones);
        bigRoomList.AddRange(bigRooms);
        BigRoomSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTutorial && curSpawnMonster < maxSpawnMonster)
        {
            StartCoroutine(SpawnEnemy());
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

    void BigRoomSpawn()
    {
        if(bigList.Count != 0 && bigRoomList.Count != 0)
        {
            for (int i = 0; i < 3; i++)
            {
                int ranPoint = Random.Range(0, bigList.Count);
                print(bigList[ranPoint]);                
                int ranRoom = Random.Range(0, bigRoomList.Count);
                print(bigRoomList[ranRoom]);
                int ranAngle = Random.Range(0, 2);
                switch (ranAngle)
                {
                    case 0:
                        Instantiate(bigRoomList[ranRoom],
                            bigList[ranPoint].position,
                            bigList[ranPoint].rotation);
                        break;
                    case 1:
                        Instantiate(bigRoomList[ranRoom],
                            bigList[ranPoint].position,
                            bigList[ranPoint].rotation);
                        break;
                }                
                bigList.RemoveAt(ranPoint);
                bigRoomList.RemoveAt(ranRoom);
            }
        }        
    }
    

}
