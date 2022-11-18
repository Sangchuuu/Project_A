using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField]
    private GameObject loading;

    private void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            loading.SetActive(true);
            Invoke("Change", 2f);
            
        }
            
    }

    void Change()
    {
        gameManager.TestStage();
    }
}
