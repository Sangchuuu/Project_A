using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI00 : MonoBehaviour
{

    List<GameObject> TargetObjects = new List<GameObject>();

    [SerializeField]
    private GameObject TargetObject;
    private GameObject ReturnPoint;

    [SerializeField]
    NavMeshAgent agent;
    private float speed;

    [SerializeField]
    private bool ismove;
    private float site;
    

    public enum m_state {IDLE, TRACKING, RETURN};

    public m_state Istate;

    private void Start()
    {
       
    }


    private void Update()
    {
       
    }


    private void FixedUpdate()
    {
        UpdateTargetSphere();
    }

    void MovingLogic()
    {
        speed = agent.speed;

        Vector3 TargetPos = TargetObject.transform.position;
        Vector3 vPos = this.gameObject.transform.position;
        float fDist = (TargetPos - vPos).magnitude;
        

        if(fDist > speed * Time.deltaTime)
        {
            ismove = true;
            agent.SetDestination(TargetObject.transform.position);
        }

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
                if(TargetObject == null)
                {
                    SetState(m_state.RETURN);
                }
                break;
            case m_state.RETURN:
                break;
        }


    }


    void UpdateTargetSphere() // ���� ����� �������� �̿��Ͽ� ����
    {
        float fdist = 0;

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, site);

        for(int i = 0; i < colliders.Length; i++) // �ݶ��̴� ������ ���� �迭�� �߰�
        {
            if (colliders[i].CompareTag ("Player"))
            {
                TargetObjects.Add(colliders[i].gameObject);
                Debug.Log("TargetObject: " + colliders[i].gameObject);
            }
           
        }
        for(int i = 0; i<TargetObjects.Count; i++) // �ݶ��̴��� ������ �迭��ŭ �ݺ�
        {
            float dist = (this.gameObject.transform.position - colliders[i].transform.position).magnitude; // �ڽ��ϰ� Ÿ���� ���ӿ�����Ʈ�� �� ���� ���� ũ�⸦ �Ÿ��� ������ ����

            if(fdist < dist)
            {
                fdist = dist;
                TargetObject = colliders[i].gameObject;
            }

        }


    }

}
