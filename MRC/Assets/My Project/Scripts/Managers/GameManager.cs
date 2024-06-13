using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


[RequireComponent(typeof(NetworkManager))]
[RequireComponent(typeof(MenuManager))]

public class GameManager : MonoBehaviourPunCallbacks
{

    public static GameManager instance;
        
    
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
    void Start()
    {


    }
    void Update()
    {
        
    }
}
