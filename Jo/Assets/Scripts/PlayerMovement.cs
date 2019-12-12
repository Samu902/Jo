using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType
{
    None, Move, Shoot, BulletChange, Rest
}

public class Move
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

    private MoveType oldMoveType;
    private MoveType currentMoveType;
    private List<GameObject> moves;
    private GameObject oldTile;
    private GameObject currentTile;
    public GameObject selectedTile;
    public GameObject SelectedBullet
    {
        get
        {
            foreach (UnityEngine.UI.Toggle t in player.ui.bulletToggles)
            {
                if(t.isOn)
                    return t.gameObject;
            }
            return null;
        }
    }

    [HideInInspector]
    public bool movesAreVisible;
    public Move selectedMove;

    private Color Hint => Color.cyan;
    private Color Hover => Color.yellow;
    private Color Select => Color.green;
    private Color Off => Color.white;

    private Player player;

    void Start()
    {
        player = GetComponent<Player>();

        movesAreVisible = false;

        foreach (UnityEngine.UI.Toggle t in player.ui.bulletToggles)
            t.interactable = false;
    }

    void Update()
    {
        if (moves != null && movesAreVisible && !player.isReady)
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

    private void CalculateMoves(MoveType moveType)
    {
        Collider[] tiles;
        if(moveType == MoveType.Move)
        {
            //Gets adiacent tiles using movementRange
            tiles = Physics.OverlapBox(transform.position, 2 * movementRange * Vector3.one * 1.01f);
        }
        else
        {
            //Gets adiacent tiles using shootRange and additionalRange
            tiles = Physics.OverlapBox(transform.position, 2 * Mathf.Clamp(shootRange + SelectedBullet.GetComponent<Bullet>().additionalRange, 0, Mathf.Infinity) * Vector3.one * 1.01f);
        }

        if (tiles == null)
            return;

        //Fills the array with the data
        moves = new List<GameObject>();
        for (int i = 0; i < tiles.Length; i++)
        {
            //The tile under the player is not a valid move
            if (tiles[i].transform.position.x == transform.position.x && tiles[i].transform.position.z == transform.position.z)
                continue;

            moves.Add(tiles[i].gameObject);
        }
    }

    public void ToggleAvailableMoves(MoveType newMoveType)
    {
        //Ogni bottone -- Move - Shoot - Proiettili vari
        //devono mostrare le possibili mosse
        //Se le mosse sono già visibili, le deve nascondere
        //Se le mosse sono di un tipo diverso da quello precedente, le deve aggiornare

        //Ricordarsi di disattivare i bottoni proiettili quando non siamo in modalità shoot

        bool none = true;

        //Update old and current moveType
        oldMoveType = currentMoveType;
        currentMoveType = newMoveType;

        //Disable bulletToggles if not in shoot or bulletChange mode
        if (!(currentMoveType == MoveType.Shoot || currentMoveType == MoveType.BulletChange))
        {
            foreach (UnityEngine.UI.Toggle t in player.ui.bulletToggles)
                t.interactable = false;
        }
        else
        {
            foreach (UnityEngine.UI.Toggle t in player.ui.bulletToggles)
                t.interactable = true;
        }

        //If last moveType was different or last time it was all off or we want to change bullet (with visible moves), now it's time to turn on, otherwise we turn off
        movesAreVisible = oldMoveType == MoveType.None || oldMoveType != currentMoveType || (currentMoveType == MoveType.BulletChange && movesAreVisible) ? true : false;

        if (moves != null)
        {
            //If they have to be turned off, that's it. If they need to be turned on, they also have to be cleaned
            for (int i = 0; i < moves.Count; i++)
                SetTileColor(moves[i], Off);
        }

        //If the moves will be turned on
        if (movesAreVisible)
        {
            //First recalculate them
            CalculateMoves(currentMoveType);

            //Color tiles
            for (int i = 0; i < moves.Count; i++)
                SetTileColor(moves[i], Hint);

            //If moveType has changed or move isn't in the range anymore
            if (oldMoveType != currentMoveType || !moves.Contains(selectedTile))
                selectedTile = null;
            else    //Continue to show it in green
                SetTileColor(selectedTile, Select);

            none = false;
        }

        //If the tiles don't get turned on or updated, this turn is none
        if (none)
        {
            currentMoveType = MoveType.None;
            foreach (UnityEngine.UI.Toggle t in player.ui.bulletToggles)
                t.interactable = false;
        }

        //v----------VECCHIO METODO-------v

        //oldMoveType = currentMoveType;
        //currentMoveType = newMoveType;

        ////Toggles the state of the bool var
        ////If you switch to another moveType, it will be turned on, no matter the state on/off
        //movesAreVisible = oldMoveType == MoveType.None ? true : !movesAreVisible;

        ////Since they have to be resetted in both cases, I put it here: if they have to be turned on, they'll do so later in this method
        //if(moves != null)
        //{
        //    for (int i = 0; i < moves.Count; i++)
        //        SetTileColor(moves[i], Off);
        //}

        //if (movesAreVisible)
        //{
        //    CalculateMoves(newMoveType);

        //    for (int i = 0; i < moves.Count; i++)
        //    {
        //        //If you switch to another moveType, the selected tile will be gone
        //        if (currentMoveType == MoveType.None)
        //            SetTileColor(moves[i], moves[i].GetComponent<Tile>().oldColor);
        //        else
        //        {
        //            SetTileColor(moves[i], Hint);
        //            selectedTile = null;
        //        }
        //    }
        //}
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

    public void Rest()
    {
        oldMoveType = currentMoveType;
        currentMoveType = MoveType.Rest;

        if (moves == null)
            return;

        for (int i = 0; i < moves.Count; i++)
            SetTileColor(moves[i], Off);

        moves = null;
    }

    public void SendMove()
    {
        //Turn off the moves, since the player won't be able to mess around anymore
        if (moves != null)
        {
            for (int i = 0; i < moves.Count; i++)
                SetTileColor(moves[i], Off);
        }

        //Build the packet
        //If the player was in the move/shoot menu but it didn't choose any target, it will simply rest
        if (selectedTile == null)
            currentMoveType = MoveType.Rest;

        if (selectedMove == null)
            selectedMove = new Move();

        switch (currentMoveType)
        {
            case MoveType.None:
            case MoveType.Rest:
                selectedMove.type = MoveType.Rest;
                selectedMove.targetTile = null;
                selectedMove.bullet = null;
                break;
            case MoveType.Move:
                selectedMove.type = MoveType.Move;
                selectedMove.targetTile = selectedTile;
                selectedMove.bullet = null;
                break;
            case MoveType.Shoot:
            case MoveType.BulletChange:
                selectedMove.type = MoveType.Shoot;
                selectedMove.targetTile = selectedTile;
                selectedMove.bullet = SelectedBullet.GetComponent<Bullet>();
                break;
        }

        //Save it in player general script: the one who is in players game manager array
        player.move = selectedMove;
        player.isReady = true;
    }
}
