using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlayerCamera : MonoBehaviour
{
    public GameObject targetToFollow;
    public CinemachineVirtualCamera vCamera;
    public Vector3 cameraOffset = new Vector3(30, 30, -30);


    void Start()
    {
        targetToFollow.AddComponent<CinemachineVirtualCamera>();
        vCamera = targetToFollow.GetComponent<CinemachineVirtualCamera>();


        // Configura a câmera para ser ortogonal
        vCamera.m_Lens.Orthographic = true;
        vCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = cameraOffset;

        // Define o objeto a ser seguido
        if (targetToFollow != null)
        {
            vCamera.Follow = targetToFollow.transform;
            vCamera.LookAt = targetToFollow.transform;
        }
        else
        {
            Debug.LogWarning("Target to follow is not set.");
        }

    }
}
