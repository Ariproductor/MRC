using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class PlayerNicknameMenu : MonoBehaviour
{
    public GameObject playerNicknameMenu;
    public GameObject gameManager;
    public NetworkManager networkManager;
    


    [Header("TextMeshPro Inputs")]
    public TMP_InputField inputNickName;


    [Header("Registry")]
    public string playerNickName;


    private void Start()
    {
        networkManager = gameManager.GetComponent<NetworkManager>();
    }
    public void ButtonRegister()
    {
        if (inputNickName.text.Length > 0)
        {
            playerNickName = inputNickName.text;
            PhotonNetwork.LocalPlayer.NickName = playerNickName;
            Debug.Log("Your Nickname is: " + inputNickName.text);
            playerNicknameMenu.SetActive(false);


            networkManager.ConnectToPhoton();

        }
        else return;


    }
}
