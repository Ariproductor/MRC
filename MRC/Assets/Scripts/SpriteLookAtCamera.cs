using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLookAtCamera : MonoBehaviour
{

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Ensure the main camera is assigned
        if (mainCamera != null)
        {
            // Get the forward direction of the camera
            Vector3 cameraForward = mainCamera.transform.forward;

            // Zero out the y component to keep the sprite aligned horizontally
            cameraForward.y = 0;

            // If the forward vector is not zero, align the sprite
            if (cameraForward != Vector3.zero)
            {
                // Make the sprite look in the direction the camera is facing
                transform.forward = cameraForward;

            }
        }
    }
}
