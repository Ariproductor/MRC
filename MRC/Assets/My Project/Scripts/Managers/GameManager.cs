using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{

    public static GameManager instance;
    [SerializeField] GameObject playerPrefab;


    [HideInInspector] public Transform cameraPlayer;

    [Header("Player Nickname")]
    public string playerNickName;


    public enum Levels
    {
        none,
        LojasAmericanas,
        Açougue
    }

    [Header("Selected Level")]
    public Levels selectedLevel;

    private void Awake()
    {
        instance = this;
    }
}
