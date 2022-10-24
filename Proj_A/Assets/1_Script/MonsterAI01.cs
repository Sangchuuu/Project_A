using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI01 : MonoBehaviour
{

    //NavMeshAgent agent;

    public enum m_state { IDLE, PATROL, CHASE, MOVE, POINT };

    public int r_indexnumber;

    [SerializeField]
    NavMeshAgent agent;


    [SerializeField]
    Transform[] m_targetsarray = new Transform[15];

    [SerializeField]
    Transform target;
    Transform WayPoint01;
    Transform WayPoint02;
    Transform WayPoint03;

   public  m_state Istate;

    public bool visited;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        WaypointIndex();
    }

    // Start is called before the first frame update
    void Start()
    {
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

        Debug.Log(Istate);
        Debug.Log(visited);
    }

    private void FixedUpdate()
    {       
        UpdateState();       
    }

    void WaypointIndex()
    {
        WayPoint01 = m_targetsarray[0];
        WayPoint02 = m_targetsarray[1];
        WayPoint03 = m_targetsarray[2];
    }

    void UpdateState()
    {
        
        switch (Istate)
        {
            case m_state.IDLE:                               
                break;
            case m_state.MOVE:

                /*if (visited == false)
                {
                    MoveMonster();
                    if (Vector3.Distance(m_targetsarray[r_indexnumber].transform.position, this.transform.position) == 0f)
                    {
                        visited = true;
                    }
                }
                else
                {                   
                    Istate = m_state.IDLE;
                }*/

                     
                    break;               
            case m_state.POINT: // 웨이포인트 설정
                SetState(m_state.MOVE);
                break;
            case m_state.PATROL:
                break;
            case m_state.CHASE:
                break;
                 
        }

       
    }

    void SetState(m_state Status)
    {
        switch (Status)
        {
            case m_state.IDLE:
                SetState(m_state.POINT);
                break;
            case m_state.MOVE:
                
                break;
            case m_state.POINT:               
                r_indexnumber = Random.Range(0, 3);
                agent.SetDestination(m_targetsarray[r_indexnumber].position);               
                break;
            case m_state.PATROL:
                
                break;
            case m_state.CHASE:
               
                break;

        }

        Istate = Status;
        
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("OnTriggerEnter:" + other.name);
        if(other.gameObject.tag == ("Waypoint"))
        {
            SetState(m_state.POINT);
        }
    }


}
