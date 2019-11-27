using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLobby : MonoBehaviour
{
    public GameObject playersParent;

    private Toggle[] players;

    private void Start()
    {
        for (int i = 0; i < 8; i++)
            players[i] = playersParent.transform.GetChild(i).GetComponentInChildren<Toggle>();
    }

    private void OnAllPlayersReady()
    {
        //Carica il gioco
    }
}
