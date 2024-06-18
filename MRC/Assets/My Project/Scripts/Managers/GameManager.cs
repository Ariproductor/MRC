using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(NetworkManager))]
[RequireComponent(typeof(MenuManager))]

public class GameManager : MonoBehaviourPunCallbacks
{

    public static GameManager instance;
    [SerializeField] GameObject playerPrefab;


    [HideInInspector] public Transform cameraPlayer;

    [Header("Related Scripts")]
    public NetworkManager networkManager;
    public MenuManager menuManager;

    public string playerNickName;

    private void Awake()
    {
        instance = this;
        networkManager = GetComponent<NetworkManager>();
        menuManager = GetComponent<MenuManager>();
    }
}
