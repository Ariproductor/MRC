using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera restingPositionCamera;
    public CinemachineVirtualCamera isometricCamera;
    public Transform player;

    void Start()
    {
        // Inicialmente, ativa a câmera de descanso e desativa a câmera isométrica
        ActivateRestingPositionCamera();
    }

    void Update()
    {
        // Exemplo de alternância ao pressionar a tecla espaço
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isometricCamera.gameObject.activeSelf)
            {
                ActivateRestingPositionCamera();
            }
            else
            {
                ActivateIsometricCamera();
            }
        }
    }

    public void ActivateRestingPositionCamera()
    {
        restingPositionCamera.gameObject.SetActive(true);
        isometricCamera.gameObject.SetActive(false);
    }

    public void ActivateIsometricCamera()
    {
        // Certifique-se de que a câmera isométrica está configurada para seguir o jogador
        isometricCamera.Follow = player;
        isometricCamera.LookAt = player;

        restingPositionCamera.gameObject.SetActive(false);
        isometricCamera.gameObject.SetActive(true);
    }
}
