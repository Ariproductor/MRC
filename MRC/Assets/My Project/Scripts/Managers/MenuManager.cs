using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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


    [Header("DesktopMenu SubWindows")]
    public GameObject chatWindow;
    public bool chatWindowToggle = false;
    public GameObject optionsWindow;
    public bool optionsWindowToggle = false;
    public GameObject mRCWindow;
    public bool mRCWindowToggle = false;


    [Header("TextMeshPro Inputs")]
    public TMP_InputField inputfieldNickName;
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
        else return;
    }

    #region Toggle Windows
    public void BtnChat()
    {
        chatWindowToggle = !chatWindowToggle;
        if (chatWindowToggle)
        {
            chatWindow.SetActive(true);
        }
        else
        {
            chatWindow.SetActive(false);
        }
    }
    public void BtnMRC()
    {
        mRCWindowToggle = !mRCWindowToggle;
        if (mRCWindowToggle)
        {
            mRCWindow.SetActive(true);
        }
        else
        {
            mRCWindow.SetActive(false);
        }
    }
    public void BtnOptions()
    {
        optionsWindowToggle = !optionsWindowToggle;
        if (optionsWindowToggle)
        {
            optionsWindow.SetActive(true);
        }
        else
        {
            optionsWindow.SetActive(false);
        }
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

    public void BtnLojasAmericanas()
    {
        GameManager.instance.selectedLevel = GameManager.Levels.LojasAmericanas;
    }

    public void BtnA�ougue()
    {
        GameManager.instance.selectedLevel = GameManager.Levels.A�ougue;
    }

    public void BtnStartGame()
    {
        if (GameManager.instance.selectedLevel == GameManager.Levels.none) return;

        if (GameManager.instance.selectedLevel == GameManager.Levels.LojasAmericanas)
        {
            SceneManager.LoadScene("MRC");
            SetScreen(Screens.None);
        }
        else if (GameManager.instance.selectedLevel == GameManager.Levels.A�ougue)
        {
            SceneManager.LoadScene("MRC");
            SetScreen(Screens.None);
        }
    }




    #endregion
}
