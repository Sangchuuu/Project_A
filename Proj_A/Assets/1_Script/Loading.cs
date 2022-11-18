using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingBack;
    [SerializeField]
    private GameObject loadingFront;
    [SerializeField]
    private Image loadingTxtImage;

    bool CheckLoad;
    // Start is called before the first frame update

    
    void Start()
    {
        loadingBack.SetActive(true);
        loadingFront.SetActive(true);
        CheckLoad = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckLoad)
        {
            StartCoroutine("FadeOut");
        }
    }

    IEnumerator FadeOut()
    {
        Color color = loadingTxtImage.color;
        for (int i = 100; i > 0; i--)
        {
            color.a -= Time.deltaTime * 0.005f;
            loadingTxtImage.color = color;
            if(loadingTxtImage.color.a < 0)
            {
                loadingBack.SetActive(false);
                loadingFront.SetActive(false);
                CheckLoad = false;
            }
        }
        yield return null;
    }
}
