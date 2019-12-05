using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private int movementRange;
    private GameObject[] moves;
    private bool movesAreVisible;

    private Player player;

    void Start()
    {
        player = GetComponent<Player>();

        movesAreVisible = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
            SelectMove();
    }

    private void ColorTile(GameObject t, Color c)
    {
        t.transform.GetComponentInChildren<Renderer>().material.color = c;
    }

    #region Move
    private bool CalculateMoves()
    {
        //Gets adiacent tiles using movementRange
        Collider[] tiles = Physics.OverlapBox(transform.position, 2 * movementRange * Vector3.one * 1.01f);

        //Returns false if there aren't any moves
        if (tiles == null)
            return false;

        //Fills the array with the data
        moves = new GameObject[tiles.Length];
        for (int i = 0; i < moves.Length; i++)
            moves[i] = tiles[i].gameObject;

        return true;
    }

    public void ToggleAvailableMoves()
    {
        //Toggles the state of the bool var
        Color c = movesAreVisible ? Color.white : Color.yellow;
        movesAreVisible = !movesAreVisible;

        if (!CalculateMoves())
            return;

        //Colors tiles
        for (int i = 0; i < moves.Length; i++)
            ColorTile(moves[i], c);
    }

    private void SelectMove()
    {
        if (!movesAreVisible)
            return;

        //Resets all tiles' (moves) color to yellow 
        for (int i = 0; i < moves.Length; i++)
            ColorTile(moves[i], Color.yellow);

        //If it hits something and that is one of the available moves, it colors it
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            for (int i = 0; i < moves.Length; i++)
            {
                if (moves[i] == hit.collider.gameObject)
                {
                    ColorTile(hit.collider.gameObject, Color.green);
                    break;
                }
            }
        }
    }
    #endregion

    #region Shoot

    #endregion
}
