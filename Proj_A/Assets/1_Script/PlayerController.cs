using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //public Transform objectFrontVector;
    public Camera vcamera;

    public GameObject flashlight;
    private bool flashilightstate = false;

    private float h = 0.0f;
    private float v = 0.0f;

    [Header("걷기, 달리기, 앉기 스피드")]
    public float walkSpeed = 3.0f;
    public float runSpeed = 6.0f;
    public float sitSpeed = 2.0f;

    [Header("점프파워, 조작불가 시간")]
    public float jumppos = 5f;
    public float jumpTime = 0.6f;
    private float jumpTimeCycle = 0f;

    [Header("슬라이딩 파워와, 조작불가 시간")]
    public float slidingPower = 8f;
    public float slidingTime = 1f;
    private float slidingTimeCycle = 0f;
    //private float moveSpeed = 3.0f;


    private float cameraH = 0.85f;
    [Header("카메라 높이 관련")]
    public float cameraHstand = 0.85f;
    public float cameraHsit = 0.4f;

    [Header("스태미나 관련")]
    [Space(5f)]
    [Header("최대 스태미나")]
    public float maxstamina = 100;

    [Header("동작별 소모 스태미나")]
    public float runstamina = 1;
    public float jumpstamina = 20;
    public float slidingstamina = 20;
    public float pakurustamina = 20;
    private float nowstamina;

    [Header("스태미나 회복 관련")]
    public float recoverystamina = 10;
    private bool staminarecoveryONOFF = false;
    public float staminarecoverytime = 2f;
    public float staminarecoverytime2 = 4f;
    private float staminarecoverytimeCycle = 0f;

    [Header("스태미나 사라지는 속도 (200/n초)")]
    private float staminaRGBmax = 200;
    public float staminaRGBdis = 100;
    private float nowstaminaRGB = 0;
    RawImage staminaRGB;

    [Header("(E키 및 마우스 클릭)의 동작 허용 길이")]
    public float PlayerRaycastL = 5f;

    [Space(15f)]



    public GameObject staminabar1;
    public GameObject staminabar2;
    private Vector2 staminabarmax;


    [Space(15f)]



    public CapsuleCollider PlayerCollider;



    private float yRotate, yRotateMove;
    public float rotateSpeed = 500.0f;

    private int playermovestate = 0;
    private bool jumpstate = false;

    private Rigidbody Playerrigidbody;

    void Start()
    {
        Playerrigidbody = GetComponent<Rigidbody>();

        staminaRGB = staminabar1.GetComponent<RawImage>();

        playermovestate = 1;

        nowstaminaRGB = 0;

        staminabarmax = staminabar1.transform.localScale; //스태미나바의 최대 길이를 저장

        nowstamina = maxstamina; //시작시 현재 스태미나를 맥스 스태미나로
    }


    void Update()
    {
        //Debug.Log("플레이어 상태 : "+playermovestate);
        PlayerRayCast();
        StaminaBarController(nowstamina, maxstamina);

        if (staminarecoveryONOFF == false)
        {
            if (nowstamina != 0)
            {
                staminarecoverytimeCycle += Time.deltaTime;
                if (staminarecoverytimeCycle > staminarecoverytime)
                {
                    staminarecoveryONOFF = true;
                    staminarecoverytimeCycle = 0;
                }
            }
            else
            {
                staminarecoverytimeCycle += Time.deltaTime;
                if (staminarecoverytimeCycle > staminarecoverytime2)
                {
                    staminarecoveryONOFF = true;
                    staminarecoverytimeCycle = 0;
                }
            }
        }



        if (staminarecoveryONOFF == true)
        {
            nowstamina += recoverystamina * Time.deltaTime;
            if (nowstamina > maxstamina)
            {
                nowstamina = maxstamina;
                staminarecoveryONOFF = false;
            }
        }

        Color color = staminaRGB.color;

        if (nowstamina < maxstamina)
        {
            nowstaminaRGB = staminaRGBmax;
            color.a = nowstaminaRGB / staminaRGBmax;
            staminaRGB.color = color;
        }

        if (nowstamina == maxstamina)
        {
            nowstaminaRGB -= staminaRGBdis * Time.deltaTime;
            color.a = nowstaminaRGB / staminaRGBmax;
            staminaRGB.color = color;
            //Debug.Log(nowstaminaRGB);
            //Debug.Log(staminaRGB.color);

        }





        Camerafollow();
        PlayerRotate();

        if (jumpstate == false)
        {
            if (playermovestate == 1 || playermovestate == 2 || playermovestate == 3 || playermovestate == 6)
            {
                if (Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(1))
                {
                    if (flashilightstate)
                    {
                        flashilightstate = false;
                        flashlight.SetActive(false);
                    }
                    else
                    {
                        flashilightstate = true;
                        flashlight.SetActive(true);
                    }
                }
            }
        }


        if (playermovestate == 1 || playermovestate == 6)
        {
            PlayerWalk();
        }


        if (jumpstate == false)
        {
            if (playermovestate == 1 || playermovestate == 2)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Playerjump();
                }
            }
        }

        if (playermovestate == 2)
            PlayerRun();

        if (playermovestate == 3)
            PlayerSitDown();

        if (jumpstate == false)
        {
            if (playermovestate == 1 || playermovestate == 2 || playermovestate == 6)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))//달리기로 상태전환
                {
                    if (Input.GetKey(KeyCode.W) && playermovestate != 2)
                        playermovestate = 2;
                    else if (playermovestate == 2)
                    {
                        playermovestate = 1;
                    }
                }

                if (Input.GetKeyUp(KeyCode.W) && playermovestate == 2)
                {
                    playermovestate = 1;
                }
            }
        }

        if (playermovestate == 2 && nowstamina == 0)//달리다가 스태미나가 0이되면 걷기로 자동 전환
        {
            playermovestate = 1;
        }

        if (jumpstate == false)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))//앉기 상태 전환
            {
                if (playermovestate == 1 || playermovestate == 6)
                {
                    playermovestate = 3;
                }
                else if (playermovestate == 3)
                {
                    playermovestate = 1;
                }

                if (playermovestate == 2)//달리고 있으면 슬라이딩
                {

                    PlayerSliding();
                }
            }
        }

        if (playermovestate == 5)
        {
            slidingTimeCycle += Time.deltaTime;

            if (slidingTimeCycle >= slidingTime)
            {
                slidingTimeCycle = 0;
                playermovestate = 3;
            }
        }

        if (jumpstate == true)
        {
            jumpTimeCycle += Time.deltaTime;

            if (jumpTimeCycle >= jumpTime)
            {
                jumpTimeCycle = 0;
                jumpstate = false;
            }
        }

    }

    public void Camerafollow()
    {
        Camera.main.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + cameraH, this.transform.position.z);
    }

    public void PlayerWalk()
    {

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        transform.Translate(moveDir.normalized * Time.deltaTime * walkSpeed, Space.Self);

        PlayerCollider.center = new Vector3(0, 0.91f, 0);
        PlayerCollider.height = 1.8f;
        cameraH = cameraHstand;

    }

    public void PlayerRun()
    {
        if (nowstamina > 0)
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");

            Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

            transform.Translate(moveDir.normalized * Time.deltaTime * runSpeed, Space.Self);

            nowstamina -= (runstamina * Time.deltaTime);
            staminarecoveryONOFF = false;
            staminarecoverytimeCycle = 0;

            if (nowstamina < 0)
            {
                nowstamina = 0;
            }
        }

    }

    public void PlayerRotate()
    {
        //yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;

        //yRotate = transform.eulerAngles.y + yRotateMove;

        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotate, 0);

        Vector3 aa = vcamera.transform.eulerAngles;
        aa.z = 0;
        aa.x = 0;
        transform.eulerAngles = aa;
    }

    public void Playerjump()
    {

        if (nowstamina > 0)
        {
            jumpstate = true;
            staminarecoveryONOFF = false;
            staminarecoverytimeCycle = 0;

            nowstamina -= jumpstamina;
            if (nowstamina < 0)
            {
                nowstamina = 0;
            }
            Playerrigidbody.AddForce(Vector3.up * jumppos, ForceMode.Impulse);
        }

    }

    public void PlayerSitDown()
    {


        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        transform.Translate(moveDir.normalized * Time.deltaTime * sitSpeed, Space.Self);

        PlayerCollider.center = new Vector3(0, 0.33f, 0);
        PlayerCollider.height = 0.64f;
        cameraH = cameraHsit;


    }

    public void PlayerSliding()
    {
        if (nowstamina > 0)
        {
            playermovestate = 5;
            staminarecoveryONOFF = false;
            staminarecoverytimeCycle = 0;
            nowstamina -= slidingstamina;
            if (nowstamina < 0)
            {
                nowstamina = 0;
            }

            PlayerCollider.center = new Vector3(0, 0.33f, 0);
            PlayerCollider.height = 0.64f;
            cameraH = cameraHsit;

            Vector3 aa = new Vector3(this.transform.forward.x, 0f, this.transform.forward.z);

            //Playerrigidbody.velocity = (aa * slidingPower);

            Playerrigidbody.AddForce(aa * slidingPower, ForceMode.Impulse);
        }

    }

    public void StaminaBarController(float state, float state_max)
    {

        float fStateRat = state / state_max;

        float rectsize = staminabarmax.x * fStateRat;


        staminabar1.transform.localScale = new Vector2(rectsize, staminabarmax.y);

    }

    public void PlayerRayCast()
    {
        RaycastHit hit;
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            string objectname = null;
            string objecttag = null;

            if (Physics.Raycast(vcamera.transform.position, vcamera.transform.forward, out hit, PlayerRaycastL))
            {
                Transform objectHit = hit.transform;

                GameObject gameObject;

                Debug.Log(objectHit.name);
                objectname = objectHit.name;
                objecttag = objectHit.tag;

                if (objecttag == "Door")
                {
                    gameObject = hit.transform.gameObject;
                    gameObject.GetComponent<Door>().ChangeDoorState();

                }

                if (objectname == "Door_Wood")
                {
                    gameObject = hit.transform.gameObject;
                    gameObject.GetComponent<BloodyDoor>().ChangeDoorState();

                }



                //if (objecttag == "Item")
                //{
                //    gameObject = hit.transform.gameObject;
                //    gameObject.GetComponent<Item1>().Item1drop();
                //}


            }
            else
            {
               // Debug.Log("없어");
            }


        }

    }

    public void pakuruOn()
    {
        playermovestate = 6;
    }
    public void pakuru2On()
    {
        playermovestate = 7;
    }
    public void pakuruOff()
    {
        playermovestate = 1;
    }

    public void GravityOff()
    {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<CapsuleCollider>().isTrigger = true;
    }
    public void GravityOn()
    {
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<CapsuleCollider>().isTrigger = false;
    }

    public bool pakuruStamina()
    {
        bool aa = false;
        if (nowstamina > 0)
        {
            staminarecoveryONOFF = false;
            staminarecoverytimeCycle = 0;
            aa = true;
            nowstamina -= jumpstamina;
            if (nowstamina < 0)
            {
                nowstamina = 0;
            }
        }
        else
        {
            aa = false;
        }

        return aa;
    }

}
