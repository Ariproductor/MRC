using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class NetworkManager : MonoBehaviourPunCallbacks
{

    [SerializeField] GameObject playerPrefab;



    private void Start()
    {
        Debug.Log("Start");
        ConnectToPhoton();
    }

    private void ConnectToPhoton()
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
        photonView.RPC("CreatePlayerAvatar", PhotonNetwork.LocalPlayer);
    }

    [PunRPC]
    void CreatePlayerAvatar()
    {
        Vector3 pos = new Vector3(Random.Range(-3f, 3f), 2f, Random.Range(-3f, 3f));
        PhotonNetwork.Instantiate(playerPrefab.name, transform.position, Quaternion.identity);
    }

}
