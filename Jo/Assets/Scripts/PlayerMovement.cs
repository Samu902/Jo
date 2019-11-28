using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int movementRange;
    private bool movesVisible;

    bool running;

    void Start()
    {
        movesVisible = false;
        running = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (movesVisible)
                HideAvailableMoves();
            else
                ShowAvailableMoves();

            movesVisible = !movesVisible;
        }
    }

    private void ShowAvailableMoves()
    {
        Collider[] moves = Physics.OverlapBox(transform.position, 2 * movementRange * Vector3.one * 1.01f);
        if (moves == null)
            return;

        foreach (Collider tile in moves)
        {
            tile.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.green;
        }
        Debug.Log("Rese visibili mosse");
    }

    private void HideAvailableMoves()
    {
        Collider[] moves = Physics.OverlapBox(transform.position, 2 * movementRange * Vector3.one * 1.01f);
        if (moves == null)
            return;

        foreach (Collider tile in moves)
        {
            tile.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.white;
        }
        Debug.Log("Rese invisibili mosse");
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        
        if(running)
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(transform.position, 2 * 2 * movementRange * Vector3.one/* * 1.1f*/);
    }
}
