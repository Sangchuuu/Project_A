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
    private Vector3 m_vRayPoint;
    [SerializeField]
    private Vector3 m_vDir;

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
    private enum e_Mon_Ai_State { Idle, Walk, Find, Chase, Skill } // 배회, 탐색, 추격, 특수

    [SerializeField]
    e_Mon_Ai_State m_Mon_Ai_State = e_Mon_Ai_State.Idle;
    
    public GameObject ObjWayPoint { get => m_objWayPoint; set => m_objWayPoint = value; }
    public bool IsArrived { get => m_bIsArrived; set => m_bIsArrived = true; }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SetActive(true);
        }
    }

    public void SetMonsterAi()
    {
        //RaycastHit hit;
        
        //if(Physics.Raycast(m_vRayPoint, m_objTarget.transform.position, out hit, 1000f))
        //{ 
        //    if (hit.collider.CompareTag("Player"))
        //    {
        //        Debug.Log("레이");
        //        Debug.Log(hit.collider.name);
        //        if (CheckDist(m_objTarget) < 9.0f)
        //            m_Mon_Ai_State = e_Mon_Ai_State.Chase;
        //        else
        //            m_Mon_Ai_State = e_Mon_Ai_State.Idle;
        //    }
        //}
        if (m_objWayPoint != null)
        {
            if (m_bIsTargetInRoom == true)
                m_Mon_Ai_State = e_Mon_Ai_State.Find;

            if (CheckDist(m_objWayPoint) > 0.5f)
                m_Mon_Ai_State = e_Mon_Ai_State.Walk;
        }
        else
            m_Mon_Ai_State = e_Mon_Ai_State.Idle;
   


        MonsterAiState(m_Mon_Ai_State);
    }
    
    private void MonsterAiState(e_Mon_Ai_State state)
    {
        switch (state)
        {
            case e_Mon_Ai_State.Idle:
                
                break;

            case e_Mon_Ai_State.Walk:
                this.transform.LookAt(m_objWayPoint.transform.position); // 추후 수정
                this.transform.position += transform.forward * m_fSpeed * Time.deltaTime;
                break;

            case e_Mon_Ai_State.Find:
                
                break;

            case e_Mon_Ai_State.Chase:
                Rotation(m_objTarget);
                this.transform.position += transform.forward * m_fSpeed * Time.deltaTime;
                
                break;

            case e_Mon_Ai_State.Skill:
                break;
        }
    }

    public void Rotation(GameObject objTarget)
    {

        Vector3 vDir = (objTarget.transform.position - this.transform.position).normalized;
        m_vDir = vDir;
        this.transform.rotation = Quaternion.LookRotation(vDir);
    }

    private void Detect()
    {
    }

    private float CheckDist(GameObject gameObject)
    {
        float fDist = (this.transform.position - gameObject.transform.position).magnitude;
        //Debug.Log(fDist);
        return fDist;
    }

    void Start()
    {
        m_objTarget = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        SetMonsterAi();
    }
}
