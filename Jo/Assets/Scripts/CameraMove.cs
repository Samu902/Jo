using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speed;
    public float pixelLimit;
    [Range(0.25f, 4f)]
    public float zoomSensivity;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x > Screen.width - pixelLimit)
        {
            dir += Vector3.right;
        }
        if(Input.GetKey(KeyCode.A) || Input.mousePosition.x < pixelLimit)
        {
            dir += Vector3.left;
        }
        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y > Screen.height - pixelLimit)
        {
            dir += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y < pixelLimit)
        {
            dir += Vector3.back;
        }
        transform.position += dir.normalized * speed * Time.deltaTime;
        transform.position += transform.forward * Input.mouseScrollDelta.y * zoomSensivity;
    }
}
