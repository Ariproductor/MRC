using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    [Range(0, 100)][SerializeField] public float playerStamina = 100f;
    [Range(100, 150)][SerializeField] public float maxStamina = 100f;




    [Header("Related Scripts")]
    public PlayerController playerController = null;
    public HealthManager healthManager = null;

    [Header("Walk Parameters")]
    public bool isWalk = false;
    public float walkSpeed = 6f;

    [Header("Stamina Regen Parameters")]
    public float staminaDrain = 5f;
    public float staminaRegen = 0.5f;

    [Header("Sprint Parameters")]
    public bool isSprint = false;
    public float sprintSpeed = 18f;

    [Header("Crouch Parameters")]

    public bool isCrouch = false;
    public bool isCrouchMove = false;
    public float crouchSpeed = 4f;


    [Header("Status Ailment Parameters")]

    public bool isStunned = false;
    public bool isRooted = false;
    public bool isSlow = false;

    public float slowCoeficient = 2;

    [Header("Current Parameters")]
    public bool isMove = false;
    public float currentMoveSpeed = 6f;

    [Header("Stamina UI Elements")]
    public Image staminaProgressUI = null;



    private void Start()
    {
        #region Reset Variables

        playerController = GetComponent<PlayerController>();
        healthManager = GetComponent<HealthManager>();

        #endregion
        StartCoroutine(StaminaTick());

    }
    public void Update()
    {
        MovementStateChecker();
        SpeedCalculator();
        UpdateStaminaUI();
    }

    void MovementStateChecker()
    {
        CheckStatusAilments();
        CheckCrouchStatus();
        CheckMoveStatus();
        CheckSprintStatus();
    }

    #region Movement State Checker


    void CheckStatusAilments()
    {
        if (healthManager.stunnedTimer > 0)
        {
            isStunned = true;
        }
        else
        {
            isStunned = false;
        }

        if (healthManager.rootedTimer > 0)
        {
            isRooted = true;
        }
        else
        {
            isRooted = false;
        }

        if (healthManager.slowedTimer > 0)
        {
            isSlow = true;
        }
        else
        {
            isSlow = false;
        }

    }
    void CheckCrouchStatus()
    {
        if (isStunned)
        {
            isCrouch = false;
        }
        else
        {
            if (isSprint)
            {
                isCrouch = false;
            }
            else
            {
                if (playerController.isCrouchAttempt)
                {
                    isCrouch = true;
                }
                else
                {
                    isCrouch = false;
                }

            }
        }
    }
    void CheckMoveStatus()
    {
        if (isStunned)
        {
            isMove = false;
        }
        else
        {
            if (isRooted)
            {
                isMove = false;
            }
            else
            {
                if (playerController.moveDirection !=Vector3.zero)
                {
                    isMove = true;
                }
                else
                {
                    isMove= false;
                }

            }
        }
    }
    void CheckSprintStatus()
    {
        if (isStunned)
        {
            isSprint = false;
        }
        else
        {
            if (isRooted)
            {
                isSprint = false;
            }
            else
            {
                if (isSlow)
                {
                    isSprint= false;
                }
                else
                {
                    if (!isMove)
                    {
                        isSprint = false;
                    }
                    else
                    {
                        if (!playerController.isSprintAttempt)
                        {
                            isSprint = false;
                        }
                        else
                        {
                            if (playerStamina <= 0)
                            {
                                isSprint = false;
                            }
                            else
                            {
                                isSprint = true;

                            }
                        }
                    }
                }
            }
        }
    }


    #endregion

    void SpeedCalculator()
    {
        if (isSlow)
        {
            CalculateNormalSpeed();
            currentMoveSpeed = (currentMoveSpeed / healthManager.slowCoeficient);
        }
        else
        {
            CalculateNormalSpeed();
        }
    }

    #region Speed Calculator


    void CalculateNormalSpeed()
    {
        if (isCrouch)
        {
            currentMoveSpeed = crouchSpeed;
        }
        else
        {
            if (isSprint)
            {
                currentMoveSpeed = sprintSpeed;
            }
            else
            {
                currentMoveSpeed = walkSpeed;
            }
        }
    }


    #endregion

    public void UpdateStaminaUI()
    {
        staminaProgressUI.fillAmount = playerStamina / maxStamina;
    }

    public IEnumerator StaminaTick()
    {
        while (true)
        {
            if (isSprint)
            {
                playerStamina -= staminaDrain;
            }
            else if (!playerController.isSprintAttempt)
            {
                playerStamina += staminaRegen;
            }
            yield return new WaitForSeconds(.1f);
        }
    }

    


}
