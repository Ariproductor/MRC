using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class ActivatePlayerCamera : MonoBehaviour
{
    public GameObject playerVirtualCamera;
    public PhotonView photonView;

    private void Awake()
    {
        if (photonView.IsMine)
        {
            playerVirtualCamera.SetActive(true);
        }
        else
        {
            playerVirtualCamera.SetActive(false);
        }
    }
}
