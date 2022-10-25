using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField]
    List<GameObject> m_listWayPoints = new List<GameObject> ();

    
    [SerializeField] 
    private int m_nRandNum = 0;
    [SerializeField]
    private float m_fDetectLength; // ��������Ʈ �����Ÿ�. �ν����Ϳ��� �����ϸ� ��������Ʈ���� �����Ÿ��� ������.
    
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
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Monster"))
        {
            other.gameObject.GetComponent<MonsterAi>().ObjWayPoint = m_listWayPoints[Random.Range(0, m_nRandNum)];
            other.gameObject.GetComponent<MonsterAi>().IsArrived = true;
        }
    }

   private void OnDrawGizmos()
   {
       Gizmos.color = Color.red;
       Gizmos.DrawWireSphere(this.transform.position, m_fDetectLength);
   }


    private void Awake()
    {
        m_fDetectLength = 10f;
    }
    private void Start()
    {
        FindWayPoint(); // �̰� �ּ�ó���ϸ� ���� �������� ����.
    }

}
