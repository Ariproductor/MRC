using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class MenuManager : MonoBehaviourPunCallbacks
{

    public static MenuManager instance;


    [Header("Related Components")]
    [HideInInspector] public Transform cameraPlayer;

    [Header("Related Scripts")]
    public NetworkManager networkManager;
    public GameManager gameManager;

    [Header("Screens")]
    public GameObject loadingScreen;
    public GameObject enterYourNicknameScreen;
    public GameObject chatScreen;

    [Header("TextMeshPro Inputs")]
    public TMP_InputField inputNickName;

    [Header("Registry")]
    public string playerNickName;
    

    

    private void Awake()
    {
        instance = this;
        networkManager = GetComponent<NetworkManager>();
        gameManager = GetComponent<GameManager>();
    }
    void Start()
    {
        loadingScreen.SetActive(false);
        enterYourNicknameScreen.SetActive(true);


    }
    void Update()
    {
        
    }
    public void Register()
    {
        if (inputNickName.text.Length > 0)
        {
            playerNickName = inputNickName.text;
            gameManager.playerNickName = playerNickName;
            Debug.Log("Your Nickname is: " + inputNickName.text);
            enterYourNicknameScreen.SetActive(false);
            loadingScreen.SetActive(true);
            networkManager.ConnectToPhoton();
           
        }
        else return;
        
    }
}
