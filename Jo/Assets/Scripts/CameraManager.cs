using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Camera control region")]
    public float speed;
    public float pixelLimit;
    [Range(0.25f, 4f)]
    public float zoomSensivity;

    public Camera mainCam;
    public Camera miniMapCam;

    public Transform playerCamerasParent;
    [HideInInspector]
    public Camera[] playerCameras;

    private void Start()
    {
        playerCameras = null;
        playerCamerasParent.gameObject.SetActive(false);
    }

    private void Update()
    {
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
        mainCam.transform.position += dir.normalized * speed * Time.deltaTime;

        mainCam.transform.position += transform.forward * Input.mouseScrollDelta.y * zoomSensivity;

        //DEBUG
        if(Input.GetKeyDown(KeyCode.K))
        {
            if (playerCameras.Length == 8)
                SwitchCameras(4);
            else if (playerCameras.Length == 4)
                SwitchCameras(2);
            else
                SwitchCameras(8);
        }
    }

    public void SwitchCameras(int n)
    {
        if(!(n == 2 || n == 4 || n == 8))
        {
            Debug.LogError("Numero per switch camera non valido: " + n);
            return;
        }

        if(n == playerCameras.Length)
        {
            Debug.LogError("E' già a questo numero");
            return;
        }

        //Fills the array, then turns on the right group
        playerCameras = new Camera[n];
        for (int i = 0; i < playerCameras.Length; i++)
        {
            switch (n)
            {
                case 8:
                    playerCameras = playerCamerasParent.GetChild(0).GetComponentsInChildren<Camera>();
                    break;
                case 4:
                    playerCameras = playerCamerasParent.GetChild(1).GetComponentsInChildren<Camera>();
                    break;
                case 2:
                    playerCameras = playerCamerasParent.GetChild(0).GetComponentsInChildren<Camera>();
                    break;
            }
        }

        switch (n)
        {
            case 8:
                playerCamerasParent.GetChild(0).gameObject.SetActive(true);
                playerCamerasParent.GetChild(1).gameObject.SetActive(false);
                playerCamerasParent.GetChild(2).gameObject.SetActive(false);
                break;
            case 4:
                playerCamerasParent.GetChild(0).gameObject.SetActive(false);
                playerCamerasParent.GetChild(1).gameObject.SetActive(true);
                playerCamerasParent.GetChild(2).gameObject.SetActive(false);
                break;
            case 2:
                playerCamerasParent.GetChild(0).gameObject.SetActive(false);
                playerCamerasParent.GetChild(1).gameObject.SetActive(false);
                playerCamerasParent.GetChild(2).gameObject.SetActive(true);
                break;
        }
    }

    public void TurnOffCam(int playerIndex)
    {
        playerCameras[playerIndex].cullingMask = 0;
        playerCameras[playerIndex].clearFlags = CameraClearFlags.SolidColor;
        playerCameras[playerIndex].backgroundColor = Color.black;
    }
}
