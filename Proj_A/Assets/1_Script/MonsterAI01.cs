using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI01 : MonoBehaviour
{

    NavMeshAgent agent;

    public enum m_state { IDLE, PATROL, CHASE, MOVE };

    [SerializeField]
    Transform target;
    Transform WayPoint01;

    

    private void Awake()
    {      
        agent = GetComponent<NavMeshAgent>();
    }

   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {

        }
    }

}
