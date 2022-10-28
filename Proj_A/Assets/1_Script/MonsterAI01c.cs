using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI01c : MonoBehaviour
{
    public GameObject objTarget;
    public float Speed = 1;
    public float Site = 0.5f;
    public GameObject objResponPoint;
    //public GameObject objPatrolPoint;
    public bool isMove;
    public GameObject m_objwaypoint;

    [SerializeField]
    private List<GameObject> objWayPoints = new List<GameObject>();
    int i_coliders = 0;
    public enum E_AI_STATE { TRACKING, RETRUN, PATROL };
    public E_AI_STATE curAIState;
    NavMeshAgent agent;

    void SetAIState(E_AI_STATE state)
    {
        switch (state)
        {
            case E_AI_STATE.TRACKING:
                break;
            case E_AI_STATE.RETRUN:
                objTarget = objResponPoint;
                break;
            case E_AI_STATE.PATROL:
                break;
        }
        curAIState = state;
    }

    void UpdateAIState()
    {
        switch (curAIState)
        {
            case E_AI_STATE.TRACKING:
                if (objTarget == null)
                {
                    SetAIState(E_AI_STATE.RETRUN);
                }
                break;
            case E_AI_STATE.RETRUN:
                if (isMove == false)
                {
                    SetAIState(E_AI_STATE.PATROL);
                }
                break;
            case E_AI_STATE.PATROL:
                UpdatePatrol(m_objwaypoint);
                break;
        }
    }

    public void UpdatePatrol(GameObject objA)
    {
        
        if (objTarget != null)
        {
            i_coliders = Random.Range(0, (objWayPoints.Count));
            objA = objWayPoints[i_coliders];
            if (isMove == false)
            {
                if (objTarget.tag == objA.tag)
                {
                    objTarget = objA;
                }               

            }
        }

    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //curAIState = E_AI_STATE.TRACKING;
        UpdateFindTargetSphere();
    }
    private void Start()
    {
        //SetAIState(curAIState);
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdataMove();

        UpdateAIState();

        Debug.Log("State: " + curAIState);
        Debug.Log("m_objwayPoint" + m_objwaypoint);
        Debug.Log("isMove: " + isMove);
    }

    private void FixedUpdate()
    {
        /* if (UpdateFindTargetLayer())
             SetAIState(E_AI_STATE.TRACKING);*/
        //UpdateFindTargetLayerAll();
        if (WayPointClear() == true)
        {
            UpdateFindTargetSphere();
        }
    }

    void SetReturn()
    {
        objTarget = objResponPoint;
    }

    void UpdataMove()
    {
        Speed = agent.speed;

        if (objTarget)
        {
            if(objTarget.layer == LayerMask.NameToLayer("Waypoint"))
            {
                objTarget = m_objwaypoint;
            }

            Vector3 vTargetPos = objTarget.gameObject.transform.position;
            vTargetPos.y = 0;
            Vector3 vPos = this.gameObject.transform.position;
            vPos.y = 0;
            Vector3 vDist = vTargetPos - vPos;
            Vector3 vDir = vDist.normalized;
            float fDist = vDist.magnitude;

            if (fDist >= Speed * Time.deltaTime)
            {
                agent.SetDestination(objTarget.transform.position);
                //transform.position += vDir * Speed * Time.deltaTime;
                isMove = true;               
            }
            else
            { 
                isMove = false;
            }

           // Debug.Log("fDist: " + fDist);
        }
    }

    bool WayPointClear()
    {
        if (isMove == false)
        {
            objWayPoints.Clear();
            return true;
        }
        else
            return false;
    }

    bool UpdateFindTargetLayer()
    {
        int nLayer = 1 << LayerMask.NameToLayer("Player");
        Collider[] colliders =
            Physics.OverlapSphere(this.transform.position, Site, nLayer);

        /*if (colliders != null)
        {
            objTarget = colliders.gameObject;
            Debug.Log("FindTarget:" + GetComponent<Collider>().gameObject.name);
            return true;
        }*/
        return false;
    }

    void UpdateFindTargetLayerAll()
    {
        Collider2D[] colliders =
            Physics2D.OverlapCircleAll(this.transform.position, Site);

        for (int i = 0; i < colliders.Length; i++)
        {
            Collider2D collider = colliders[i];
            if (collider.tag == "Player")
            {
                objTarget = collider.gameObject;
                Debug.Log("FindTarget:" + collider.gameObject.name);
            }
        }
    }

    void UpdateFindTargetSphere()
    {        
        float fdist = 0;
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, Site);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Waypoint"))
                objWayPoints.Add(colliders[i].gameObject);
        }
        for (int i = 0; i < objWayPoints.Count; i++)
        {
            float dist = (this.transform.position - objWayPoints[i].transform.position).magnitude;
            if (fdist < dist)
            {
                fdist = dist;
                m_objwaypoint = objWayPoints[i];
            }
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, Site);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
            objTarget = collision.gameObject;
    }
}
