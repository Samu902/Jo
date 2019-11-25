using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject[] avatars;
    private int current;

    void Start()
    {
        current = 0;
    }

    void Update()
    {
        RotateAvatar();

        if (Input.GetKeyDown(KeyCode.Mouse1))
            current = current == avatars.Length - 1 ? 0 : current + 1;

        if(Input.GetKeyDown(KeyCode.Mouse0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 200, 1 << 8))
        {
            MoveAvatar(hit.transform.position);
        }
    }

    private void MoveAvatar(Vector3 newPos)
    {
        avatars[current].transform.position = newPos + Vector3.up * 0.5f;
    }

    private void RotateAvatar()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 200, 1 << 8))
        {
            avatars[current].transform.LookAt(hit.collider.transform.position);
        }
    }
}
