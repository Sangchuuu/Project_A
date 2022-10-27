using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public Camera vcamera;
    private float xRotate, yRotate, xRotateMove, yRotateMove;
    public float rotateSpeed = 500.0f;
    RaycastHit hit;
    bool pakurubool = false;

    private void Start()
    {
        Cursor.visible = false;
        yRotate = 0;
        xRotate = 0;
    }
    void Update()
    {

        if (pakurubool == false)
        {
            xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;
            yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;
            yRotate = transform.eulerAngles.y + yRotateMove;
            //xRotate = transform.eulerAngles.x + xRotateMove; 

            xRotate = xRotate + xRotateMove;

            xRotate = Mathf.Clamp(xRotate, -90, 90); // 위, 아래 고정
                                                     //if(pakurubool == true)
                                                     //{
                                                     //    yRotate = Mathf.Clamp(yRotate, -90, 90);
                                                     //}

            transform.eulerAngles = new Vector3(xRotate, yRotate, 0);

        }
    }

    public void MouseMove()
    {

    }
    public void pakuruOn()
    {
        pakurubool = true;
    }
    public void pakuruOff()
    {
        if (pakurubool == true)
        {
            //xRotateMove = transform.rotation.eulerAngles.x;
            //yRotateMove = transform.rotation.eulerAngles.y;
            yRotate = 0;
            xRotate = 0;
            pakurubool = false;
        }
    }

}

