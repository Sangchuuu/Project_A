using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAi : MonoBehaviour
{
    [SerializeField]
    private float m_fSpeed;
    [SerializeField]
    private float m_fDetectDist;  //추격범위
    [SerializeField]
    private float m_fSight;


    [SerializeField]
    private GameObject m_objTarget;
    [SerializeField]
    private GameObject m_objWayPoint;
    [SerializeField]
    private GameObject m_objRoom;

    [SerializeField]
    private bool m_bIsArrived = false;
    [SerializeField]
    private bool m_bIsTargetInRoom = false;
    
    [SerializeField]
    private enum e_Mon_Ai_State { Walk, Find, Chase, Skill } // 배회, 탐색, 추격, 특수

    [SerializeField]
    e_Mon_Ai_State m_Mon_Ai_State;
    
    public GameObject ObjWayPoint { get => m_objWayPoint; set => m_objWayPoint = value; }
    public bool IsArrived { get => m_bIsArrived; set => m_bIsArrived = true; }

    public void SetMonsterAi()
    {
        if (m_objTarget != null)
        {
            if (CheckDist(m_objTarget) > 9.0f)
                m_Mon_Ai_State = e_Mon_Ai_State.Chase;
        }
        else
        {
            if (m_bIsTargetInRoom == true)
                m_Mon_Ai_State = e_Mon_Ai_State.Find;

            if (CheckDist(m_objWayPoint) > 0.5f)
                m_Mon_Ai_State = e_Mon_Ai_State.Walk;
        }
       
        MonsterAiState(m_Mon_Ai_State);
    }
    
    private void MonsterAiState(e_Mon_Ai_State state)
    {
        switch (state)
        {
            case e_Mon_Ai_State.Walk:
                this.transform.LookAt(m_objWayPoint.transform.position); // 추후 수정
                this.transform.position += transform.forward * m_fSpeed * Time.deltaTime;
                break;

            case e_Mon_Ai_State.Find:
                
                break;

            case e_Mon_Ai_State.Chase:

                break;

            case e_Mon_Ai_State.Skill:
                break;

        }

    }

    private void Detect()
    {

    }

    private float CheckDist(GameObject gameObject)
    {
        float fDist = (this.transform.position - gameObject.transform.position).magnitude;
        return fDist;
    }
    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> objWayPoints = new List<GameObject>();
        float fdist = 0;
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 10f);
        for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("WayPoint"))
                objWayPoints.Add(colliders[i].gameObject);
        }
        for(int i = 0; i < objWayPoints.Count; i++)
        {
            float dist = (this.transform.position - objWayPoints[i].transform.position).magnitude;
            if(fdist < dist)
            {
                fdist = dist;
                m_objWayPoint = objWayPoints[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetMonsterAi();
    }
}
