using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private int movementRange;
    private List<GameObject> moves;
    private GameObject currentTile;
    private bool movesAreVisible;

    private Color Hint => Color.cyan;
    private Color Hover => Color.yellow;
    private Color Select => Color.green;
    private Color Off => Color.white;

    private Player player;

    void Start()
    {
        player = GetComponent<Player>();

        movesAreVisible = false;
    }

    void Update()
    {
        if (movesAreVisible)
            HoverMove();
    }

    private void ColorTile(GameObject t, Color c)
    {
        t.GetComponent<Tile>().oldColor = t.GetComponent<Tile>().rend.material.color;
        t.GetComponent<Tile>().rend.material.color = c;
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
        moves = new List<GameObject>();
        for (int i = 0; i < tiles.Length; i++)
            moves.Add(tiles[i].gameObject);

        return true;
    }

    public void ToggleAvailableMoves()
    {
        //Toggles the state of the bool var
        Color c = movesAreVisible ? Off : Hint;
        movesAreVisible = !movesAreVisible;

        if (!CalculateMoves())
            return;

        //Colors tiles
        for (int i = 0; i < moves.Count; i++)
            ColorTile(moves[i], c);
    }

    private void HoverMove()
    {
        if (currentTile != null)
            ColorTile(currentTile, currentTile.GetComponent<Tile>().oldColor);

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 1 << 8))
        {
            if (moves.Contains(hit.collider.gameObject))
            {
                currentTile = hit.collider.gameObject;

                if (Input.GetKeyDown(KeyCode.Mouse0))
                    ColorTile(hit.collider.gameObject, Select);
                else
                    ColorTile(hit.collider.gameObject, Hover);
            }
        }
    }

    private void SelectMove()
    {
        if(currentTile != null)
            ColorTile(currentTile, Off);

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 1 << 8))
        {
            //if (moves.Contains(hit.collider.gameObject))
            //{
            //    currentTile = moves[moves.IndexOf(hit.collider.gameObject)];

            //    if (Input.GetKeyDown(KeyCode.Mouse0))
            //        ColorTile(moves[moves.IndexOf(hit.collider.gameObject)], Select);
            //    else
            //        ColorTile(moves[moves.IndexOf(hit.collider.gameObject)], Hover);
            //}
            currentTile = hit.collider.gameObject;
            ColorTile(hit.collider.gameObject, Hover);
        }
        //else
        //{
        //    ColorTile(currentTile, Hint);
        //}

        //if(Input.GetKeyDown(KeyCode.Mouse0))
        //{

        //}

        ////Resets all tiles' (moves) color to cyan
        //for (int i = 0; i < moves.Count; i++)
        //    ColorTile(moves[i], Hint);

        ////If it hits something and that is one of the available moves, it colors it
        //if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, (1 << 8) | (1 << 5)))
        //{
        //    //If it's a UI element, keep the old move
        //    if(hit.collider.gameObject.layer == 1 << 5)
        //    {
        //        ColorTile(oldMove, Select);
        //        Debug.Log("Toccata UI");
        //    }

        //    //If the selected tile (or something else) is an available move, color it
        //    if (moves.Contains(hit.collider.gameObject))
        //    {
        //        ColorTile(hit.collider.gameObject, Select);
        //        oldMove = hit.collider.gameObject;
        //    }
        //}
    }
    #endregion

    #region Shoot

    #endregion
}
