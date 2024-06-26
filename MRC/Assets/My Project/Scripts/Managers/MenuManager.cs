using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using static GameManager;
using UnityEngine.UI;

public class MenuManager : MonoBehaviourPunCallbacks
{
    public static MenuManager instance; //singleton

    public enum Screens
    {
        None,
        MainMenuScreen,
        LoadingScreen,
        DesktopScreen
    }

    [Header("Screens")]
    public Screens currentScreen;
    public GameObject mainMenuScreen;
    public GameObject loadingScreen;
    public GameObject desktopScreen;

    [Header("RegisterWindowElements")]
    public TMP_InputField inputfieldNickName;
    public Button registerButton;


    [Header("LoadingScreen")]

    public TextMeshProUGUI inputFieldWelcomePlayer;

    [Header("DesktopMenu")]
    public TextMeshProUGUI inputFieldNumberOfPlayers;



    [Header("DesktopMenu SubWindows")]
    public GameObject chatWindow;
    public bool chatWindowToggle = false;
    public GameObject optionsWindow;
    public bool optionsWindowToggle = false;
    public GameObject mRCWindow;
    public bool mRCWindowToggle = false;

    [Header("DesktopMenu SubWindows")]
    public Button startGameButton;



    [Header("Chat")]
    public TMP_InputField inputFieldChatMsg;
    public TextMeshProUGUI chatLogText;
    public Button chatSendButton;
    




    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SetScreen(currentScreen);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        chatWindow.SetActive(false);
        optionsWindow.SetActive(false);
        mRCWindow.SetActive(false);

        SwitchStartButton();

    }


    public void SetScreen(Screens _screen)
    {
        mainMenuScreen.SetActive(false);
        loadingScreen.SetActive(false);
        desktopScreen.SetActive(false);

        currentScreen = _screen;

        switch (currentScreen)
        {
            case Screens.MainMenuScreen:
                mainMenuScreen.SetActive(true);
                break;

            case Screens.LoadingScreen:
                loadingScreen.SetActive(true);
                break;

            case Screens.DesktopScreen:
                desktopScreen.SetActive(true);
                break;

        }
    }


    #region RegisterWindow
    public void BtnRegister()
    {
        if (inputfieldNickName.text.Length > 0)
        {
            GameManager.instance.playerNickName = inputfieldNickName.text;
            PhotonNetwork.LocalPlayer.NickName = GameManager.instance.playerNickName;
            Debug.Log("Your Nickname is: " + inputfieldNickName.text);
            inputFieldWelcomePlayer.text = string.Format("Welcome " + GameManager.instance.playerNickName);
            SetScreen(Screens.LoadingScreen);
            NetworkManager.instance.ConnectToPhoton();

        }
    }

    public void OnChangedLengthNickInput()
    {
        if (inputfieldNickName.text.Length >= 3)

        {
            registerButton.interactable = true;
        }

        else
        {
            registerButton.interactable = false;
        }
    }

        #endregion

    #region Toggle Windows
        public void BtnChat()
    {
        chatWindowToggle = !chatWindowToggle;
        chatWindow.SetActive(chatWindowToggle);

        /*if (chatWindowToggle)
        {
            chatWindow.SetActive(true);
        }
        else
        {
            chatWindow.SetActive(false);
        }*/
    }
    public void BtnMRC()
    {
        mRCWindowToggle = !mRCWindowToggle;
        mRCWindow.SetActive(mRCWindowToggle);

        /*if (mRCWindowToggle)
        {
            mRCWindow.SetActive(true);
        }
        else
        {
            mRCWindow.SetActive(false);
        }*/
    }
    public void BtnOptions()
    {
        optionsWindowToggle = !optionsWindowToggle;
        optionsWindow.SetActive(optionsWindowToggle);

        /*if (optionsWindowToggle)
        {
            optionsWindow.SetActive(true);
        }
        else
        {
            optionsWindow.SetActive(false);
        }*/
    }
    #endregion

    #region Chat
    public void BtnEnviarMsg()
    {
        if (inputFieldChatMsg.text.Length > 0)
        {

            photonView.RPC("Log", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName, inputFieldChatMsg.text);
            inputFieldChatMsg.text = "";
        }

        EventSystem.current.SetSelectedGameObject(null);
    }

    [PunRPC]
    void Log(string playerName, string message)
    {
        // atualiza o chat log com as mensagens enviadas
        chatLogText.text += string.Format("<b>{0}:</b> {1}\n", playerName, message);

        // ajusta o tamanho do chat log conforme o tamanho do texto
        chatLogText.rectTransform.sizeDelta = new Vector2(chatLogText.rectTransform.sizeDelta.x, chatLogText.mesh.bounds.size.y + 20);
    }



    public void UpdateChatStatus()
    {
        if (PhotonNetwork.InRoom)
        {
            inputFieldChatMsg.interactable = true;
            chatSendButton.interactable = true;
            
            chatLogText.text = string.Format("You are Connected" + "\n");
        }
        else
        {
            inputFieldChatMsg.interactable = false;
            chatSendButton.interactable = false;
            chatLogText.text = "You are Disconected";
        }



    }


    #endregion

    #region MRCWindow


    [PunRPC]
    public void BtnLojasAmericanas()
    {
        GameManager.instance.selectedLevel = Levels.LojasAmericanas;
        NetworkManager.instance.JoinRoomAmericanas();

    }

    [PunRPC]
    public void BtnAcougue()
    {
        GameManager.instance.selectedLevel = Levels.Acougue;
        NetworkManager.instance.JoinRoomAcougue();

    }

   public void SwitchStartButton()
    {
        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            startGameButton.interactable = true;
        }
        else
        {
            startGameButton.interactable = false;
        }
    }

    [PunRPC]
    public void BtnStartGame()
    {
        if (PhotonNetwork.CurrentRoom.Name.Contains("Americanas") && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            SetScreen(Screens.LoadingScreen);
            photonView.RPC("StartGame", RpcTarget.All);
        }

        if (PhotonNetwork.CurrentRoom.Name.Contains("Acougue") && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            SetScreen(Screens.LoadingScreen);
            photonView.RPC("StartGame", RpcTarget.All);
        }


    }

    [PunRPC]
    public void StartGame()
    {
        if (PhotonNetwork.CurrentRoom.Name.Contains("Americanas") && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            PhotonNetwork.LoadLevel("MRC");
        }
        else if (PhotonNetwork.CurrentRoom.Name.Contains("Acougue") && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            PhotonNetwork.LoadLevel("MRC");
        }
        else
        {
            Debug.Log("Error: MenuManager / StartGame Failed");
            return;
        }

        /*
        if (GameManager.instance.selectedLevel == GameManager.Levels.Acougue && GameManager.instance.acougue.Count >= 2)
        {
            PhotonNetwork.LoadLevel("MRC");
            SetScreen(Screens.None);
            return;
        }

        if (GameManager.instance.selectedLevel == GameManager.Levels.LojasAmericanas && GameManager.instance.americanas.Count >= 2)
        {
            PhotonNetwork.LoadLevel("MRC");
            SetScreen(Screens.None);
            return;
        }
        */
    }

    [PunRPC]
    public void LoadLevel()
    {
        switch (GameManager.instance.selectedLevel)
        {
            
            case GameManager.Levels.LojasAmericanas:
                PhotonNetwork.LoadLevel("MRC");
                break;

            case GameManager.Levels.Acougue:
                PhotonNetwork.LoadLevel("MRC");
                break;
        }
    }

    public void UpdateNumberOfPlayers()
    {
        if (PhotonNetwork.InRoom)
        {
            inputFieldNumberOfPlayers.text = string.Format("N°: " + PhotonNetwork.CurrentRoom.PlayerCount);
            SwitchStartButton();
        }
        else
        {
            inputFieldNumberOfPlayers.text = string.Format("N°: " + "0");
            SwitchStartButton();
        }

    }


    #endregion
}