using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI01c : MonoBehaviour
{
    public GameObject objTarget;
    public float Speed = 1;
    public float Site = 0.5f;
    public GameObject objResponPoint;
    public GameObject objPatrolPoint;
    public bool isMove;

    public enum E_AI_STATE { TRACKING, RETRUN, PATROL };
    public E_AI_STATE curAIState;

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
                UpdatePatrol(objResponPoint, objPatrolPoint);
                break;
        }
    }

    public void UpdatePatrol(GameObject objA, GameObject objB)
    {
        if (objTarget != null)
        {
            if (isMove == false)
            {
                if (objTarget.name == objA.name)
                {
                    objTarget = objB;
                }
                else if (objTarget.name == objB.name)
                {
                    objTarget = objA;
                }

            }
        }

    }

    private void Awake()
    {
        //curAIState = E_AI_STATE.TRACKING;
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
        Debug.Log("isMove: " + isMove);
    }

    private void FixedUpdate()
    {
        if (UpdateFindTargetLayer())
            SetAIState(E_AI_STATE.TRACKING);
        UpdateFindTargetLayerAll();

    }

    void SetReturn()
    {
        objTarget = objResponPoint;
    }

    void UpdataMove()
    {
        if (objTarget)
        {
            Vector3 vTargetPos = objTarget.transform.position;
            vTargetPos.y = 0;
            Vector3 vPos = this.transform.position;
            vPos.y = 0;
            Vector3 vDist = vTargetPos - vPos;
            Vector3 vDir = vDist.normalized;
            float fDist = vDist.magnitude;

            if (fDist > Speed * Time.deltaTime)
            {
                transform.position += vDir * Speed * Time.deltaTime;
                isMove = true;
            }
            else
            { 
                isMove = false; 
            }

           // Debug.Log("fDist: " + fDist);
        }
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


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, Site);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            objTarget = collision.gameObject;
    }
}
