using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float speed;
    public float pixelLimit;
    [Range(0.25f, 4f)]
    public float zoomSensivity;

    private Transform topView;
    private Transform perspectiveView;

    private Camera mainCam;
    [SerializeField]
    private Camera miniMapCam;

    //Servirà poi
    private Camera[] playerCameras;

    void Start()
    {
        mainCam = GetComponent<Camera>();
    }

    void Update()
    {
        //Depth minore --> renderizzata prima, si vede solo l'ultima a schermo
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(miniMapCam.depth < mainCam.depth)
            {
                miniMapCam.depth = -1;
                mainCam.depth = -2;
            }
            else
            {
                miniMapCam.depth = -2;
                mainCam.depth = -1;
            }
        }

        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x > Screen.width - pixelLimit)
        {
            dir += Vector3.right;
        }
        if(Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x < pixelLimit)
        {
            dir += Vector3.left;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y > Screen.height - pixelLimit)
        {
            dir += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y < pixelLimit)
        {
            dir += Vector3.back;
        }
        transform.position += dir.normalized * speed * Time.deltaTime;
        transform.position += transform.forward * Input.mouseScrollDelta.y * zoomSensivity;
    }
}
