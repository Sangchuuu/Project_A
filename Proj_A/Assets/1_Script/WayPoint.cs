using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField]
    List<GameObject> m_listWayPoints = new List<GameObject> ();

    [SerializeField]
    GameObject m_objPlayer;

    
    [SerializeField] 
    private int m_nRandNum = 0;

    [SerializeField]
    private bool m_bIsPlayerArrived;

    [SerializeField]
    private float m_fDetectLength; // 웨이포인트 감지거리. 인스펙터에서 수정하면 웨이포인트끼리 감지거리가 증가함.

    public void FindWayPoint()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, m_fDetectLength);
        for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.CompareTag("WayPoint"))
            {
                if (colliders[i].gameObject != this.gameObject)
                {
                     m_listWayPoints.Add(colliders[i].gameObject);
                     m_nRandNum = m_listWayPoints.Count;
                }
            }

            if (colliders[i].gameObject.CompareTag("Player"))
            {
                m_objPlayer = colliders[i].gameObject;
            }
                 
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Monster"))
        {
            other.gameObject.GetComponent<MonsterAi>().ObjWayPoint = m_listWayPoints[Random.Range(0, m_nRandNum)];
            other.gameObject.GetComponent<MonsterAi>().IsArrived = true;
        }

        if(other.gameObject.CompareTag("Player"))
        {

        }
    }

   private void OnDrawGizmos()
   {
       Gizmos.color = Color.red;
       Gizmos.DrawWireSphere(this.transform.position, m_fDetectLength);
   }


    private void Awake()
    {
        m_fDetectLength = 100f;
    }
    private void Start()
    {
        FindWayPoint(); // 이거 주석처리하면 끄면 감지범위 꺼짐.
    }

}
