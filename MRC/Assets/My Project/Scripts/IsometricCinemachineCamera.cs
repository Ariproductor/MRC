using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IsometricCinemachineCamera : MonoBehaviour
{
    public Transform target; // O objeto que a câmera seguirá

    void Start()
    {
        CinemachineFreeLook freeLook = GetComponent<CinemachineFreeLook>();

        if (freeLook != null && target != null)
        {
            freeLook.Follow = target;
            freeLook.LookAt = target;
        }
    }
}
