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


    [Header("DesktopMenu SubWindows")]
    public GameObject chatWindow;
    public bool chatWindowToggle = false;
    public GameObject optionsWindow;
    public bool optionsWindowToggle = false;
    public GameObject mRCWindow;
    public bool mRCWindowToggle = false;

    [Header("DesktopMenu SubWindows")]
    public Button startGameButton;



    [Header("TextMeshPro Inputs")]
    public TMP_InputField inputFieldChatMsg;


    public TextMeshProUGUI chatLogText;
    




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
    #endregion

    #region MRCWindow


    [PunRPC]
    public void BtnLojasAmericanas()
    {
        photonView.RPC("SelectLojasAmericanas", RpcTarget.All);
        
    }

    [PunRPC]
    public void SelectLojasAmericanas()
    {
        GameManager.instance.selectedLevel = GameManager.Levels.LojasAmericanas;
        if (GameManager.instance.acougue.Contains(PhotonNetwork.LocalPlayer))
        {
            GameManager.instance.acougue.Remove(PhotonNetwork.LocalPlayer);
        }
        GameManager.instance.americanas.Add(PhotonNetwork.LocalPlayer);
        Debug.Log(GameManager.instance.americanas.Count + " Americanas");
        startGameButton.interactable = true;
    }

    [PunRPC]
    public void BtnAcougue()
    {
        photonView.RPC("SelectAcougue", RpcTarget.All);
    }

    [PunRPC]
    public void SelectAcougue()
    {
        GameManager.instance.selectedLevel = GameManager.Levels.Acougue;
        if (GameManager.instance.americanas.Contains(PhotonNetwork.LocalPlayer))
        {
            GameManager.instance.americanas.Remove(PhotonNetwork.LocalPlayer);
        }
        GameManager.instance.acougue.Add(PhotonNetwork.LocalPlayer);
        Debug.Log(GameManager.instance.acougue.Count + " Acougue");
        startGameButton.interactable = true;
    }

    [PunRPC]
    public void BtnStartGame()
    {
        photonView.RPC("StartGame", RpcTarget.All);
    }

    [PunRPC]
    public void StartGame()
    {
        SetScreen(Screens.LoadingScreen);

        if (GameManager.instance.selectedLevel == GameManager.Levels.none)
        {
            Debug.Log("Nenhum Level Selecionado");
            SetScreen(Screens.DesktopScreen);
            return;
        }

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


    #endregion
}