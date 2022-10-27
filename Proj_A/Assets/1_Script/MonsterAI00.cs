using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI00 : MonoBehaviour
{
    [SerializeField]
    List<GameObject> TargetObjects = new List<GameObject>();

    [SerializeField]
    private GameObject TargetObject;
    
    [SerializeField]
    private GameObject ReturnPoint;

    [SerializeField]
    NavMeshAgent agent;

    [SerializeField]
    private float speed;

    [SerializeField]
    private bool ismove;

    [SerializeField]
    private float site;
    

    public enum m_state {IDLE, TRACKING, RETURN};

    public m_state Istate;

    private void Start()
    {
        UpdateTargetSphere();
        SetState(Istate);
    }


    private void Update()
    {
        MovingLogic();
        UpdateState();
    }


    private void FixedUpdate()
    {
        UpdateTargetSphere();
    }

    void MovingLogic()
    {
        speed = agent.speed;

        Vector3 TargetPos = TargetObject.transform.position;
        TargetPos.y = 0;
        Vector3 vPos = this.gameObject.transform.position;
        vPos.y = 0;
        float fDist = (TargetPos - vPos).magnitude;
        

        if(fDist < speed)
        {
            ismove = true;
            agent.SetDestination(TargetObject.transform.position);
        }
        else
        {
            TargetObjects.Clear();
            ismove = false;
        }

    }

    void TargetSelect()
    {

    }



    void SetState(m_state status)
    {
        switch (status) 
        { 
            case m_state.IDLE:
                break;

            case m_state.TRACKING:
                break;

            case m_state.RETURN:
                TargetObject = ReturnPoint;
                break;
               
        }

        Istate = status;

    }


    void UpdateState()
    {
        switch (Istate)
        {
            case m_state.IDLE:
                break;
            case m_state.TRACKING:
                if(!TargetObject)
                {
                    SetState(m_state.RETURN);
                }
                break;
            case m_state.RETURN:
                break;
        }


    }




    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, site);
    }


    void UpdateTargetSphere() // 구형 모양의 오버랩을 이용하여 감지
    {
        float fdist = 0;

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, site);

        for(int i = 0; i < colliders.Length; i++) // 콜라이더 감지된 것을 배열에 추가
        {
            if (colliders[i].CompareTag ("Player"))
            {
                TargetObjects.Add(colliders[i].gameObject);
                Debug.Log("TargetObject: " + colliders[i].gameObject);
            }
           
        }
        for(int i = 0; i<TargetObjects.Count; i++) // 콜라이더를 저장한 배열만큼 반복
        {
            float dist = (this.gameObject.transform.position - colliders[i].transform.position).magnitude; // 자신하고 타겟의 게임오브젝트를 뺀 벡터 값의 크기를 거리의 값으로 설정

            if(fdist < dist)
            {
                fdist = dist;
                TargetObject = colliders[i].gameObject;
            }

        }


    }

}
