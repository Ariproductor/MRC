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

    //Fazer uma lista de players para lojas americanas

    //fazer uma lista de players para o acougue

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
    }

    [PunRPC]
    public void SelectLevel()
    {
        switch (selectedLevel)
        {
            case Levels.none:
                selectedLevel = Levels.none;
                break;

            case Levels.LojasAmericanas:
                selectedLevel = Levels.LojasAmericanas;
                break; 
            
            case Levels.Acougue:
                selectedLevel = Levels.Acougue;
                break;
        }
    }

    [PunRPC]
    public void StartGame()
    {
        
    }



}
