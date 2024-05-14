using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : CharacterBase
{
    // Update is called once per frame
    public override void Update()
    {
        HandleInput();
        base.Update();
    }
    void HandleInput()
    {
        // Get input from arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput);

        ApplyMovement(movementDirection);
    }
    void Shoot()
    {

    }
}
