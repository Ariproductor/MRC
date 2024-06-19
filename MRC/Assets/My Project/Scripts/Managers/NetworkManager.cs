using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance; //singleton

    [SerializeField] GameObject playerPrefab;

    [SerializeField] int playerCount = 2;

    private void Awake()
    {
        instance = this;
    }

    public void ConnectToPhoton()
    {
        Debug.Log("ConnectToPhoton");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = (byte)playerCount;

        PhotonNetwork.CreateRoom(null);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");

    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        Debug.Log("Playercount:" + PhotonNetwork.CurrentRoom.PlayerCount);
        MenuManager.instance.SetScreen(MenuManager.Screens.DesktopScreen);

    }
}
