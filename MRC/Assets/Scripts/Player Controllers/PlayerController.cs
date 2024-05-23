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

    [SerializeField] private bool canMove = true;
    [SerializeField] private bool canAct = true;

    [Header("Walk Parameters")]
    [SerializeField] private bool isWalking = false;
    [SerializeField] private float walkSpeed = 6f;

    [Header("Sprint Parameters")]
    [SerializeField] private bool isSprinting = false;
    [SerializeField] private float sprintSpeed = 18f;

    [Header("Slow Parameters")]
    [SerializeField] private bool isSlow = false;
    [SerializeField] private float slowCoeficient = 2;

    [Header("Crouch Parameters")]
    [SerializeField] private bool isCrouch = false;
    [SerializeField] private float crouchSpeed = 4f;

    [Header("Current Speed Parameters")]
    [Range(1, 20)][SerializeField] private float currentMoveSpeed = 6f;

    [Header("Stamina Parameters")]
    [SerializeField] private StaminaManager staminaManager = null;

    [Header("Stamina UI Elements")]
    [SerializeField] private Image staminaProgressUI = null;

    #endregion

    Vector3 moveDirection = Vector3.zero;
    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        staminaManager = GetComponent<StaminaManager>();
    }

    void Update()
    {
        #region Slow Switch

        if (Input.GetKeyDown(KeyCode.F1))
        {
            isSlow = !isSlow;

            if (isSlow)
            {
                Debug.Log("I am Slow");
            }

            if (!isSlow)
            {
                Debug.Log("I am not Slow");
            }
        }

        #endregion

        HandleMovement();

        if (canAct)
        {
            if (canMove)
            {
                if (isSlow)
                {
                    ApplySlowMovement();
                }
                else
                {
                    ApplyMovement();
                }
            }
        }

        if (Input.GetKey(KeyCode.LeftControl) && moveDirection != Vector3.zero)
        {
            isCrouch = true;
            Debug.Log("I am Crouched");
            currentMoveSpeed = crouchSpeed;
            isWalking = false;
            isSprinting = false;
            staminaManager.SetSprinting(false);
        }
        else if (Input.GetKey(KeyCode.LeftShift) && moveDirection != Vector3.zero)
        {
            Debug.Log("I am Trying to Sprint");
            if (staminaManager.playerStamina >= staminaManager.staminaDrain)
            {
                isSprinting = true;
                Debug.Log("I am Sprinting");
                currentMoveSpeed = sprintSpeed;
                isWalking = false;
                isCrouch = false;
                staminaManager.SetSprinting(true);
            }
            else
            {
                Debug.Log("I am Out of Stamina");
                isSprinting = true;
                currentMoveSpeed = walkSpeed;
                isWalking = false;
                isCrouch = false;
                staminaManager.SetSprinting(true);

            }
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && moveDirection != Vector3.zero)
        {
            isWalking = true;
            Debug.Log("I am Walking");
            currentMoveSpeed = walkSpeed;
            isSprinting = false;
            isCrouch = false;
            staminaManager.SetSprinting(false);
        }
        else if (moveDirection == Vector3.zero)
        {
            isSprinting = false;
            isCrouch = false;
            isWalking = false;
            Debug.Log("I think I'm just standing still...");
            currentMoveSpeed = walkSpeed;
            staminaManager.SetSprinting(false);
        }
        else
        {
            Debug.Log("I don't really know what's going on...");
        }

        UpdateStaminaUI();
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

        if (moveDirection == Vector3.zero)
        {
            isSprinting = false;
            isWalking = false;
        }
    }

    void ApplyMovement()
    {
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void ApplySlowMovement()
    {
        characterController.Move((moveDirection / slowCoeficient) * Time.deltaTime);
    }

    void UpdateStaminaUI()
    {
        if (staminaProgressUI != null)
        {
            staminaProgressUI.fillAmount = staminaManager.playerStamina / staminaManager.maxStamina;
        }
    }
}
