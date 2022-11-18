using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    public GameObject InventoryRayout;
    [SerializeField]
    private bool inventoryopen;

    [SerializeField]
    private RectTransform go_Inventory;
   private static GameManager _instance;
    
    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else if(_instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    

    void Start()
    {
        inventoryopen = false;
        go_Inventory.anchoredPosition = Vector3.up * 3000;

    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryopen == true)
            {
                inventoryopen = false;
            }
            else if (inventoryopen == false)
            {
                inventoryopen = true;
            }
            OpenInventoryUI(inventoryopen);
        }

    }

    public void StageChange()
    {
        SceneManager.LoadScene("Map 1F_002");
    }

    public void OpenInventoryUI(bool open)
    {
        if (open == true)
        {
            //InventoryRayout.gameObject.SetActive(open);
            go_Inventory.anchoredPosition = Vector3.zero;
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
            //InventoryRayout.gameObject.SetActive(open);
            go_Inventory.anchoredPosition = Vector3.up * 3000;
        }
    }



}
