using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField]
    private Transform[] wayPoints;


    private void OnTriggerStay(Collider other)
    {
        int ranPoint = Random.Range(0, 3);
        if(other.gameObject.tag == "Player" && GameObject.Find("Player").GetComponent<User>().canTel)
        {
            other.transform.position = wayPoints[ranPoint].position;
            
        }
    }
}
