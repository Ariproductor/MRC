using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviourPunCallbacks
{

    public static GameManager instance;
    [SerializeField] GameObject playerPrefab;

    public List<Player> americanas = new List<Player>();
    public List<Player> acougue = new List<Player>();
    

    [Header("Player Nickname")]
    public string playerNickName;


    public enum Levels
    {
        none,
        LojasAmericanas,
        Acougue
    }

    [Header("Selected Level")]
    public Levels selectedLevel;


    private void Awake()
    {
        instance = this;
        if (americanas == null)
        {
            americanas = new List<Player>();
        }
        if (acougue == null)
        {
            acougue = new List<Player>();
        }
    }
    /*
    [PunRPC]
    public void SelectLevel()
    {
        if (americanas.Contains(PhotonNetwork.LocalPlayer))
        {
            americanas.Remove(PhotonNetwork.LocalPlayer);
        }

        if (acougue.Contains(PhotonNetwork.LocalPlayer))
        {
            acougue.Remove(PhotonNetwork.LocalPlayer);
        }
                
        switch (selectedLevel)
        {
            case Levels.none:
                selectedLevel = Levels.none;
                break;

            case Levels.LojasAmericanas:
                selectedLevel = Levels.LojasAmericanas;
                americanas.Add(PhotonNetwork.LocalPlayer);
                Debug.Log(americanas.Count + " Americanas");
                break; 
            
            case Levels.Acougue:
                selectedLevel = Levels.Acougue;
                acougue.Add(PhotonNetwork.LocalPlayer);
                Debug.Log(americanas.Count + "Acougue");
                break;
        }
        
    }
    */

}
