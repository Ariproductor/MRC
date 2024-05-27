using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager1 : MonoBehaviourPunCallbacks
{
    public static NetworkManager1 instance; //singleton
    [SerializeField] GameObject playerprefabTeamRed;
    [SerializeField] GameObject playerprefabTeamBlue;
    [HideInInspector] public Transform cameraPlayer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        Debug.Log("Start");
        ConnectToPhoton();

    }

    #region connect
    void ConnectToPhoton()
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
        Debug.Log("OnconnectedToMaster");
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
    #endregion

    #region Create player
    [PunRPC]

    void CreatePlayerAvatar()
    {
        Vector3 pos = new Vector3(Random.Range(-3f, 3f), 2f, Random.Range(-3f, 3f));

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.Instantiate(playerprefabTeamRed.name, transform.position, Quaternion.identity);

        }
        else
        {
            PhotonNetwork.Instantiate(playerprefabTeamBlue.name, transform.position, Quaternion.identity);
        }

    }
    #endregion

}

