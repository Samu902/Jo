using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType
{
    Move, Shoot, Rest
}

public struct Move
{
    public MoveType type;
    public GameObject targetTile;
    public Bullet bullet;
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private int movementRange;
    [SerializeField]
    private int shootRange;
    private List<GameObject> moves;
    private GameObject oldTile;
    private GameObject currentTile;
    public GameObject selectedTile;
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
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
                SelectMove();
            else
                HoverMove();
        }
    }

    #region TileUtility
    private void SetTileColor(GameObject g, Color c)
    {
        Tile t = g.GetComponent<Tile>();
        t.oldColor = t.rend.material.color;
        t.rend.material.color = c;
    }

    private Color GetTileColor(GameObject g)
    {
        return g.GetComponent<Tile>().rend.material.color;
    }
    #endregion

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
        {
            if (tiles[i].transform.position.x == transform.position.x && tiles[i].transform.position.z == transform.position.z)
                continue;

            moves.Add(tiles[i].gameObject);
        }

        SortMoves();

        return true;
    }

    private void SortMoves()
    {
        for (int i = 0; i < moves.Count - 1; i++)
        {
            for (int j = i + 1; j < moves.Count; j++)
            {
                //The precedence is for higher Z
                if(moves[j].transform.position.z > moves[i].transform.position.z)
                {
                    GameObject temp = moves[i];
                    moves[i] = moves[j];
                    moves[j] = temp;
                }
                else if(moves[j].transform.position.z == moves[i].transform.position.z)
                {
                    //If Z is equal, the precedence is for lower X
                    if(moves[j].transform.position.x < moves[i].transform.position.x)
                    {
                        GameObject temp = moves[i];
                        moves[i] = moves[j];
                        moves[j] = temp;
                    }
                }
            }
        }
    }

    public void ToggleAvailableMoves()
    {
        //Toggles the state of the bool var
        movesAreVisible = !movesAreVisible;

        if (!CalculateMoves())
            return;

        //Colors tiles
        for (int i = 0; i < moves.Count; i++)
        {
            Color c = movesAreVisible ? moves[i].GetComponent<Tile>().oldColor : Off;
            SetTileColor(moves[i], c);
        }
    }

    private void HoverMove()
    {
        //The previous current tile becomes the old one
        oldTile = currentTile;
        if (oldTile != null)
        {
            //If the old tile wasn't green, color it in cyan
            if (GetTileColor(oldTile) != Select)
                SetTileColor(oldTile, Hint);
        }

        //This is the case where the target is a valid move
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 1 << 8) && moves.Contains(hit.collider.gameObject))
        {
            currentTile = hit.collider.gameObject;
            if(GetTileColor(currentTile) != Select)
                SetTileColor(currentTile, Hover);
        }
        else    //In this case it could be nothing, a generic object or a generic tile
        {
            currentTile = null;
        }
    }

    private void SelectMove()
    {
        //The previous current tile becomes the old one
        oldTile = currentTile;
        if (oldTile != null)
        {
            SetTileColor(oldTile, Hint);
        }

        //Unmark the old selected green tile
        if (selectedTile != null)
            SetTileColor(selectedTile, Hint);

        //This is the case where the target is a valid move
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 1 << 8) && moves.Contains(hit.collider.gameObject))
        {
            currentTile = hit.collider.gameObject;
            SetTileColor(currentTile, Select);
            selectedTile = currentTile;
        }
        else    //In this case it could be nothing, a generic object or a generic tile
        {
            currentTile = null;
            selectedTile = null;
        }
    }
    #endregion

    #region Shoot

    #endregion
}
