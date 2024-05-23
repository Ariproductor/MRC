using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(StaminaManager))]
[RequireComponent(typeof(HealthManager))]
public class PlayerController : MonoBehaviour
{
    #region Variables

    [Header("Related Scripts")]
    public StaminaManager staminaManager = null;
    public HealthManager healthManager = null;

    [Header("Related Components")]
    public CharacterController characterController = null;

    [Header("Primitive Movement Parameters")]
    [Range(1, 20)] public float currentMoveSpeed = 6f;
    public bool canMove = true;
    public bool canAct = true;
    public Vector3 moveDirection = Vector3.zero;

    #endregion


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        staminaManager = GetComponent<StaminaManager>();
        healthManager = GetComponent<HealthManager>();
    }

    void Update()
    {

        HandleMovement();

        if (healthManager.canAct)
        {
            if (healthManager.canMove)
            {
                if (healthManager.isSlow)
                {
                    ApplySlowMovement();
                }
                else
                {
                    ApplyMovement();
                }
            }
        }
    }


    void HandleMovement()
    {
        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
        Vector3 right = Camera.main.transform.TransformDirection(Vector3.right);

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        float curSpeedX = currentMoveSpeed * Input.GetAxis("Vertical");
        float curSpeedY = currentMoveSpeed * Input.GetAxis("Horizontal");

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

    }

    void ApplyMovement()
    {
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void ApplySlowMovement()
    {
        characterController.Move((moveDirection / healthManager.slowCoeficient) * Time.deltaTime);
    }

    void UpdateStaminaUI()
    {

    }
}
