using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI01 : MonoBehaviour
{

    //NavMeshAgent agent;

    public enum m_state { PATROL, CHASE, MOVE, POINT };

    //public int r_indexnumber;

    [SerializeField]
    //NavMeshAgent agent;

    public float Speed = 1;
    public float Site = 0.5f;

    /*Transform[] m_targetsarray = new Transform[15];*/

    public GameObject WaypointTrans;
    public GameObject PlayerTrans;
    
    /*Transform WayPoint01;
    Transform WayPoint02;
    Transform WayPoint03;*/

    public m_state Istate;

    public GameObject objtarget;
    //public bool visited;
    public bool isMove;

    private void Awake()
    {
        //agent = GetComponent<NavMeshAgent>();
       
    }

    // Start is called before the first frame update
    void Start()
    {
        //UpdateFindTargetLayerAll();
        SetState(Istate);       
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(UpdateState());/*courtain*/
        /*switch (Istate)
        {
            case m_state.IDLE:
                r_indexnumber = Random.Range(0, 3);
                Istate = m_state.MOVE;
                break;
            case m_state.MOVE:
                MoveMonster();
                Istate = m_state.IDLE;
                break;
            case m_state.PATROL:
                break;
            case m_state.CHASE:
                break;

        }//스테이트를 깡으로 업데이트 돌림(60프레임)*/

        UpdateMove();
        UpdateState();

        Debug.Log(Istate);
        Debug.Log(isMove);
    }

    private void FixedUpdate()
    {
        if (UpdateFindTargetLayerAll())
        {
            SetState(m_state.MOVE);
        }
    }

   

    void UpdateState()
    {
        
        switch (Istate)
        {
            case m_state.MOVE:
                if(objtarget.gameObject.CompareTag("Player"))
                {
                    SetState(m_state.CHASE);
                }
                if(isMove == false)
                {
                    SetState(m_state.POINT);
                }
                break;               
            case m_state.POINT: // 웨이포인트 설정
                SetState(m_state.MOVE);
                break;
            case m_state.PATROL:
                UpdatePatrol(WaypointTrans, PlayerTrans);
                break;
            case m_state.CHASE:
                if(objtarget == null)
                {
                    SetState(m_state.MOVE);
                }
                break;
                 
        }

       
    }

    void SetState(m_state Status)
    {
        switch (Status)
        {
            case m_state.MOVE:
                objtarget = WaypointTrans;
                break;
            case m_state.POINT:               
                break;
            case m_state.PATROL:               
                break;
            case m_state.CHASE:
                objtarget = PlayerTrans;
                break;

        }

        Istate = Status;
        
    }


    public void UpdatePatrol(GameObject objA, GameObject objB)
    {
        if(Istate == m_state.MOVE)
        {
            if(objtarget.layer == LayerMask.NameToLayer("Player"))
            {
                objtarget = objB;
            }
            else if (objtarget.layer == LayerMask.NameToLayer("Waypoint"))
            {
                objtarget = objA;
            }
        }



    }

    void UpdateMove()
    {
        if (objtarget)
        {
            Vector3 vTargetPos = objtarget.transform.position;
            Vector3 vPos = this.transform.position;
            Vector3 vDist = vTargetPos - vPos;
            Vector3 vDir = vDist.normalized;
            float fDist = vDist.magnitude;

            if (fDist > Speed * Time.deltaTime)
            {
                transform.position += vDir * Speed * Time.deltaTime;
                isMove = true;
            }
            else
                isMove = false;

            Debug.Log("Dist: " + fDist);
        }
    }



    bool UpdateFindTargetLayerAll()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, Site);


        for(int i = 0; i<colliders.Length; i++)
        {
            Collider collider = colliders[i];

            if (collider != null)
            {
                if (collider.tag == "Waypoint")
                {
                    objtarget = collider.gameObject;
                    Debug.Log("FindTarget:" + collider.gameObject.name);
                }
                else if (collider.tag == "Player")
                {
                    objtarget = collider.gameObject;
                    Debug.Log("FindTarget:" + collider.gameObject.name);
                }

                return true;
            }

        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, Site);
    }



    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
            objtarget = collision.gameObject;
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
        }
    }


}
