using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class GameLobby : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject playersParent;

    private TMP_Text[] playerNames;
    private Toggle[] readyToggles;

    private void Start()
    {
        readyToggles = new Toggle[8];
        playerNames = new TMP_Text[8];

        for (int i = 0; i < 8; i++)
        {
            readyToggles[i] = playersParent.transform.GetChild(i).GetComponentInChildren<Toggle>();
            readyToggles[i].isOn = false;

            if (PhotonNetwork.LocalPlayer.ActorNumber - 1 != i)
                readyToggles[i].interactable = false;

            playerNames[i] = playersParent.transform.GetChild(i).GetComponentInChildren<TMP_Text>();
            playerNames[i].text = "";

            if(i < PhotonNetwork.PlayerList.Length)
                playerNames[i].text = PhotonNetwork.PlayerList[i].NickName;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerNames[PhotonNetwork.LocalPlayer.ActorNumber - 1].text);
            stream.SendNext(readyToggles[PhotonNetwork.LocalPlayer.ActorNumber - 1].isOn);
        }

        if(stream.IsReading)
        {
            playerNames[PhotonNetwork.LocalPlayer.ActorNumber - 1].text = (string)stream.ReceiveNext();
            readyToggles[PhotonNetwork.LocalPlayer.ActorNumber - 1].isOn = (bool)stream.ReceiveNext();
        }
    }

    public void SetReadyState(bool on)
    {
        //if the player has turned off, just do nothing
        if (!on)
            return;

        //Check if all players are ready: if it finds a non-ready one, it exits
        for (int i = 0; i < readyToggles.Length; i++)
        {
            if (!readyToggles[i].isOn)
                return;
        }
        OnAllPlayersReady();
    }

    private void OnAllPlayersReady()
    {
        Debug.Log("Tutti sono pronti - possiamo iniziare");
        //Carica il gioco
        //PhotonNetwork.LoadLevel("Game");
    }
}
