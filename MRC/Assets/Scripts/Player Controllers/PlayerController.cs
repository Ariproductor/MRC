using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(StaminaManager))]
[RequireComponent(typeof(HealthManager))]
public class PlayerController : MonoBehaviourPun
{
    #region Variables

    [Header("Related Scripts")]
    public StaminaManager staminaManager = null;
    public HealthManager healthManager = null;

    [Header("Related Components")]
    public CharacterController characterController = null;

    [Header("Primitive Movement Parameters")]
    public bool isSprintAttempt = false;
    public bool isCrouchAttempt = false;
    [Range(1, 20)] public float currentMoveSpeed = 6f;
    public Vector3 moveDirection = Vector3.zero;

    #endregion


    void Start()
    {
        #region Reset Variables


        characterController = GetComponent<CharacterController>();
        staminaManager = GetComponent<StaminaManager>();
        healthManager = GetComponent<HealthManager>();
        isSprintAttempt = false;
        isCrouchAttempt = false;


        #endregion

    }

    void Update()
    {
        IsCrouchAttempt();

        IsSprintAttempt();

        HandleMovement();

        SetSpeed();

        ApplyMovement();
    }
    void IsSprintAttempt()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprintAttempt = true;
        }
        else
        {
            isSprintAttempt = false;
        }

    }
    void IsCrouchAttempt()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouchAttempt = true;
        }
        else
        {
            isCrouchAttempt = false;
        }

    }
    void HandleMovement()
    {
        if (!photonView.IsMine) return;
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
    void SetSpeed()
    {
        currentMoveSpeed = staminaManager.currentMoveSpeed;
    }
    void ApplyMovement()
    {
        if (staminaManager.isStunned)
        {

        }
        else
        {
            if (staminaManager.isRooted)
            {

            }
            else
            {
                if (characterController.isGrounded)
                {
                    characterController.Move(moveDirection * Time.deltaTime);
                }
                else
                {
                    characterController.Move( new Vector3(0, -10, 0) *Time.deltaTime);
                }
            }
        }
    }

}