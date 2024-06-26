using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance; // Singleton

    [SerializeField] GameObject playerPrefab;

    private void Awake()
    {
        instance = this;
    }

    public void ConnectToPhoton()
    {
        Debug.Log("Connect To Photon");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        Debug.Log("Current lobby: (" + PhotonNetwork.CurrentLobby.Name + ")");
        MenuManager.instance.SetScreen(MenuManager.Screens.DesktopScreen);
        MenuManager.instance.UpdateChatStatus();
    }

    public void DisconnectFromRoom()
    {
        PhotonNetwork.LeaveRoom();
        Debug.Log("Leaving Room");
        MenuManager.instance.UpdateNumberOfPlayers();
        MenuManager.instance.UpdateChatStatus();
    }

    public void JoinRoomAmericanas()
    {
        if (PhotonNetwork.InRoom)
        {
            if (PhotonNetwork.CurrentRoom.Name == "Americanas")
            {
                Debug.Log("Already in Room: Americanas");
                return;
            }


        }
        else
        {
            Debug.Log("Trying to Join Americanas");
            PhotonNetwork.JoinRoom("Americanas");
        }
    }

    public void JoinRoomAcougue()
    {
        if (PhotonNetwork.InRoom)
        {
            if (PhotonNetwork.CurrentRoom.Name == "Acougue")
            {
                Debug.Log("Already in Room: Acougue");
                return;
            }

        }
        else
        {
            Debug.Log("Trying to Join Acougue");
            PhotonNetwork.JoinRoom("Acougue");
        }

    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join room: " + message);
        if (GameManager.instance.selectedLevel == GameManager.Levels.LojasAmericanas)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4; // Set the max players to 4, adjust as necessary
            PhotonNetwork.CreateRoom("Americanas", roomOptions);
            Debug.Log("Creating room: Americanas");

        }

        if (GameManager.instance.selectedLevel == GameManager.Levels.Acougue)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4; // Set the max players to 4, adjust as necessary
            PhotonNetwork.CreateRoom("Acougue", roomOptions);
            Debug.Log("Creating room: Acougue");

        }
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created a Room");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined the room: " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("Players currently in the room: " + PhotonNetwork.CurrentRoom.PlayerCount);
        MenuManager.instance.UpdateNumberOfPlayers();
        MenuManager.instance.UpdateChatStatus();

        MenuManager.instance.SetScreen(MenuManager.Screens.DesktopScreen);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left the Room");
        MenuManager.instance.UpdateChatStatus();

        while (PhotonNetwork.InRoom)
        {
            return;
        }

        if (GameManager.instance.selectedLevel == GameManager.Levels.LojasAmericanas)
        {
            Debug.Log("Trying to Join Americanas");
            PhotonNetwork.JoinRoom("Americanas");
        }

        if (GameManager.instance.selectedLevel == GameManager.Levels.Acougue)
        {
            Debug.Log("Trying to Join Acougue");
            PhotonNetwork.JoinRoom("Acougue");
        }

    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player entered: " + newPlayer.NickName);
        Debug.Log("Players currently in the room: " + PhotonNetwork.CurrentRoom.PlayerCount);
        MenuManager.instance.UpdateNumberOfPlayers();

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Player left: " + otherPlayer.NickName);
        Debug.Log("Players currently in the room: " + PhotonNetwork.CurrentRoom.PlayerCount);
        MenuManager.instance.UpdateNumberOfPlayers();

    }



}
