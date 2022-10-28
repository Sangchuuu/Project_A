using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pakuru_wall : MonoBehaviour
{
    private int pakurustate = 0;
    RaycastHit hit;
    public Camera vcamera;
    PlayerController player;

    GameObject startpoint1;
    GameObject startpoint2;
    GameObject waypoint1;
    GameObject waypoint2;

    public MainCameraController camerascript;

    private Vector3 start1;
    private Vector3 start2;
    private Vector3 way1;
    private Vector3 way2;

    [Header("파쿠르 상승속도, 넘는속도, 하강속도")]

    public float hspeed = 3f;
    public float vspeed = 3f;
    public float dspeed = 5f;

    [Header("파쿠르 가능 범위 활성화 레이캐스트 길이")]
    public float pakuruRay = 5f;

    // Start is called before the first frame update
    void Start()
    {
        vcamera = Camera.main;
        player = FindObjectOfType<PlayerController>();
        camerascript = FindObjectOfType<MainCameraController>();
        startpoint1 = transform.GetChild(0).gameObject;
        startpoint2 = transform.GetChild(1).gameObject;
        waypoint1 = transform.GetChild(2).gameObject;
        waypoint2 = transform.GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 backrotate3 = vcamera.transform.eulerAngles;
        //Debug.Log(backrotate3);
        if (pakurustate == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Physics.Raycast(vcamera.transform.position, vcamera.transform.forward, out hit, pakuruRay))
                {
                    GameObject ddd = hit.transform.gameObject; ;

                    Debug.Log(ddd.name);
                    string objectname = ddd.name;

                    //Debug.Log("큐브1이 없다");
                    if (objectname == "pakuru_look")
                    {
                        bool staminacheck= player.pakuruStamina();

                        if (staminacheck == true)
                        {
                            camerascript.pakuruOn();
                            float startpoint1distance = Vector3.Distance(player.transform.position, startpoint1.transform.position);
                            float startpoint2distance = Vector3.Distance(player.transform.position, startpoint2.transform.position);
                            if (startpoint1distance > startpoint2distance)
                            {
                                pakurustate = 2;
                                start1 = startpoint2.transform.position;
                                way1 = waypoint2.transform.position;
                                way2 = waypoint1.transform.position;
                                start2 = startpoint1.transform.position;

                            }
                            else
                            {
                                pakurustate = 2;
                                start1 = startpoint1.transform.position;
                                way1 = waypoint1.transform.position;
                                way2 = waypoint2.transform.position;
                                start2 = startpoint2.transform.position;
                            }
                        }

                    }
                }
            }
        }

        if (pakurustate == 2)
        {
            player.pakuru2On();
            player.GravityOff();

            PlayerFollow(player.transform.position, start1, 3f);

            if ((int)player.transform.position.x == (int)start1.x && (int)player.transform.position.z == (int)start1.z)
            {
                pakurustate = 3;
            }
        }

        if (pakurustate == 3)
        {
            PlayerFollow(player.transform.position, way1, hspeed);

            if ((int)player.transform.position.x == (int)way1.x && (int)player.transform.position.z == (int)way1.z && (int)player.transform.position.y == (int)way1.y)
            {
                pakurustate = 4;
            }

        }

        if (pakurustate == 4)
        {
            PlayerFollow(player.transform.position, way2, vspeed);
            Vector3 backrotate2 = vcamera.transform.eulerAngles;
            float bb = backrotate2.x;
            if (bb > 180)
                bb -= 360;
            float aa = Mathf.Lerp(bb, 0, Time.deltaTime * 3f);
            if (aa < 0)
                aa += 360;
            backrotate2.x = aa;
            Debug.Log(backrotate2.x);
            vcamera.transform.eulerAngles = backrotate2;

            if ((int)player.transform.position.x == (int)way2.x && (int)player.transform.position.z == (int)way2.z && (int)player.transform.position.y == (int)way2.y)
            {
                pakurustate = 5;
            }
        }

        if (pakurustate == 5)
        {
            PlayerFollow(player.transform.position, start2, dspeed);
            //Vector3 backrotate2 = vcamera.transform.eulerAngles;
            //backrotate2.x = 45;
            //vcamera.transform.eulerAngles = backrotate2;

            if ((int)player.transform.position.x == (int)start2.x && (int)player.transform.position.z == (int)start2.z && (int)player.transform.position.y == (int)start2.y)
            {
                pakurustate = 0;
                player.pakuruOff();
                player.GravityOn();
                camerascript.pakuruOff();
                Vector3 backrotate = vcamera.transform.eulerAngles;
                backrotate.x = 0;
                player.transform.eulerAngles = backrotate;

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        {
            if (other.tag == "Player" && pakurustate == 0)
            {
                Debug.Log("테스트");
                player.pakuruOn();
                pakurustate = 1;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && pakurustate == 1)
        {
            player.pakuruOff();
            pakurustate = 0;
        }
    }

    public void PlayerFollow(Vector3 vPlayerPos, Vector3 vTargetPos, float speed)
    {
        float fTargetDist = Vector3.Distance(vPlayerPos, vTargetPos);
        Vector3 vTargetDir = vTargetPos - vPlayerPos;

        Vector3 vDir = vTargetDir.normalized;

        Vector3 vSpeed = vDir * speed;
        Vector3 vMove = vSpeed * Time.deltaTime;
        player.transform.position += vMove;
    }

   

}
