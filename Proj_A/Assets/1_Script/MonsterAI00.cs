using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

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

    /*[SerializeField]
    float ViewAngle = 0f;

    [SerializeField]
    LayerMask TargetMask;

    [SerializeField]
    LayerMask ObstacleMask;*/



    public enum m_state { IDLE, TRACKING, RETURN };

    public m_state Istate;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();  
    }

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


        if (fDist < speed * Time.deltaTime)
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
                if (TargetObject.CompareTag("Player"))
                {
                    site = 15f;
                }
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
                if (!TargetObject)
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


    /*Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }*/


    void UpdateTargetSphere() // 구형 모양의 오버랩을 이용하여 감지
    {
        //float fdist = 0;

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, site);

        for (int i = 0; i < colliders.Length; i++) // 콜라이더 감지된 것을 배열에 추가
        {
            if (colliders[i].CompareTag("Player"))
            {
                TargetObjects.Add(colliders[i].gameObject);               
                Debug.Log("TargetObject: " + colliders[i].gameObject);
            }
            
        }

        TargetObject = colliders[0].gameObject;
        
    }


}


