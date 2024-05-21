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
        // Inicialmente, ativa a c�mera de descanso e desativa a c�mera isom�trica
        ActivateRestingPositionCamera();
    }

    void Update()
    {
        // Exemplo de altern�ncia ao pressionar a tecla espa�o
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
        // Certifique-se de que a c�mera isom�trica est� configurada para seguir o jogador
        isometricCamera.Follow = player;
        isometricCamera.LookAt = player;

        restingPositionCamera.gameObject.SetActive(false);
        isometricCamera.gameObject.SetActive(true);
    }
}
