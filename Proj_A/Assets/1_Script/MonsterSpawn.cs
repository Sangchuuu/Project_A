using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
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
    // Start is called before the first frame update
    void Update()
    {
        if(curSpawnMonster<maxSpawnMonster)
            StartCoroutine(SpawnEnemy());
    }
    // Update is called once per frame
    

    IEnumerator SpawnEnemy()
    {
        //소환될 몬스터
        int ranEnemy = Random.Range(0, 1);
        //소환될 위치
        int ranPoint = Random.Range(0, 14);
        Instantiate(monsters[ranEnemy],
                    enemyZones[ranPoint].position,
                    enemyZones[ranPoint].rotation);
        curSpawnMonster++;

        yield return null;
    }
}
