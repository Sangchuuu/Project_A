using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    public string targetObjectName;
    public string sceneName;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == targetObjectName)
        {
            SceneManager.LoadScene(sceneName);
            Debug.Log("¥Ÿ¿Ω æ¿¿∏∑Œ");
        }
    }


}
