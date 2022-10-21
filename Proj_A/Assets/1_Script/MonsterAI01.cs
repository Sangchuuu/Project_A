using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI01 : MonoBehaviour
{

    //NavMeshAgent agent;

    public enum m_state { IDLE, PATROL, CHASE, MOVE };

    int r_indexnumber = Random.Range(0, 2);

    [SerializeField]
    Transform[] m_targetsarray = new Transform[15];

    [SerializeField]
    Transform target;
    Transform WayPoint01;
    Transform WayPoint02;
    Transform WayPoint03;
    

    private void Awake()
    {      
        //agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateState();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {

        }
    }

    void WaypointIndex()
    {
        WayPoint01 = m_targetsarray[0];
        WayPoint02 = m_targetsarray[1];
        WayPoint03 = m_targetsarray[2];
    }

    void UpdateState(m_state m_state)
    {
        switch (m_state) 
        {
            case m_state.IDLE:
                m_state = m_state.MOVE;
                break;
            case m_state.MOVE:
                MoveMonster();
                break;
            case m_state.PATROL:
                break;
            case m_state.CHASE:
                break;
            default:
                m_state = m_state.IDLE;
                break;       
        }

    }

    void MoveMonster()
    {
        //agent.SetDestination(m_targetsarray[r_indexnumber].position);
    }

}
