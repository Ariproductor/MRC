using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static MenuManager;

public class MRCLevelGameManager : MonoBehaviour
{

    [SerializeField] GameObject playerPrefab;

    private void Start()
    {
        MenuManager.instance.SetScreen(Screens.None);
        CreatePlayerAvatar();
    }


    [PunRPC]
    public void CreatePlayerAvatar()
    {
        Vector3 pos = new Vector3(0f, 5f, 0f);
        PhotonNetwork.Instantiate(playerPrefab.name, transform.position, Quaternion.identity);
        MenuManager.instance.SetScreen(MenuManager.Screens.None);

    }
}
