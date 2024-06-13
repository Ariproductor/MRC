using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class NetworkManager : MonoBehaviourPunCallbacks
{

    [SerializeField] GameObject playerPrefab;

    [Header("Related Scripts")]
    public GameManager gameManager;
    public MenuManager menuManager;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        menuManager = GetComponent<MenuManager>();
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
        menuManager.loadingScreen.SetActive(false);
        menuManager.chatScreen.SetActive(true);
    }

    [PunRPC]
    void CreatePlayerAvatar()
    {
        Vector3 pos = new Vector3(Random.Range(-3f, 3f), 2f, Random.Range(-3f, 3f));
        PhotonNetwork.Instantiate(playerPrefab.name, transform.position, Quaternion.identity);
    }

}
