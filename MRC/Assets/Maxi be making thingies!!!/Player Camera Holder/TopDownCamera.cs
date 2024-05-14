using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform target; // Reference to the character's transform
    public Vector3 offset; // Offset from the character's position
    public float smoothSpeed = 0.125f; // Smoothness of camera movement

    void Start()
    {
        SetTarget(GameObject.FindGameObjectWithTag("Player").transform); // Set player character as the initial target
    }

    void LateUpdate()
    {
        FollowTarget(); // Call the FollowTarget method
    }

    void FollowTarget()
    {
        if (target != null)
        {
            // Calculate target position with offset
            Vector3 desiredPosition = target.position + offset;

            // Smoothly move the camera towards the target position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Update camera position
            transform.position = smoothedPosition;

            // Look at the character
            transform.LookAt(target);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        // Set a new target for the camera
        target = newTarget;
    }
}
