using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1 : MonoBehaviour
{
    public GameObject item1UI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Item1drop()
    {
        item1UI.SetActive(true);
        this.gameObject.SetActive(false);

    }
}
