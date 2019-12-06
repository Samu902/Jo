using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{
    public GameObject mainPanel;
    public GameObject joinPanel;
    public GameObject createPanel;

    private TMP_InputField roomField;
    private TMP_InputField nickField;

    private void Start()
    {
        mainPanel.SetActive(true);
        joinPanel.SetActive(false);
        createPanel.SetActive(false);

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnJoinClick()
    {
        mainPanel.SetActive(false);
        joinPanel.SetActive(true);
        createPanel.SetActive(false);
        nickField = joinPanel.transform.GetChild(0).GetComponent<TMP_InputField>();
        roomField = joinPanel.transform.GetChild(1).GetComponent<TMP_InputField>();
    }

    public void OnCreateClick()
    {
        mainPanel.SetActive(false);
        joinPanel.SetActive(false);
        createPanel.SetActive(true);
        nickField = createPanel.transform.GetChild(0).GetComponent<TMP_InputField>();
        roomField = createPanel.transform.GetChild(1).GetComponent<TMP_InputField>();
    }

    public void OnNickNameSet(string nick)
    {
        PhotonNetwork.LocalPlayer.NickName = nick;
    }

    public void OnRoomSelect()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnRoomToCreate()
    {
        PhotonNetwork.CreateRoom(roomField.text);
    }

    public void Back()
    {
        mainPanel.SetActive(true);
        joinPanel.SetActive(false);
        createPanel.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connesso al master server");   
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Stanza '" + PhotonNetwork.CurrentRoom.Name + "' creata");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Connesso alla stanza " + PhotonNetwork.CurrentRoom.Name);
        //PhotonNetwork.LoadLevel("GameLobby");
        SceneManager.LoadScene("GameLobby");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("La stanza non esiste e verrà creata");
        PhotonNetwork.CreateRoom(roomField.text);
    }
}
