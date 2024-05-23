using System.Collections;
using System.Collections.Generic;
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
    public bool isWalking = false;
    public float walkSpeed = 6f;

    [Header("Stamina Regen Parameters")]
    public float staminaDrain = 10f;
    public float staminaRegen = 0.5f;

    [Header("Sprint Parameters")]
    public bool isSprintingAttempt = false;
    public bool isSprinting = false;
    public float sprintSpeed = 18f;

    [Header("Crouch Parameters")]
    public bool isCrouch = false;
    public bool isCrouchMove = false;
    public float crouchSpeed = 4f;


    [Header("Status Ailment Parameters")]
    public bool canAct = true;
    public float stunnedTimer = 0f;

    public bool canMove = true;
    public float rootedTimer = 0f
        ;
    public bool isSlow = false;
    public float slowCoeficient = 2;

    [Header("Current Parameters")]
    public float currentMoveSpeed = 6f;

    [Header("Stamina UI Elements")]
    public Image staminaProgressUI = null;



    private void Start()
    {
        #region Reset Variables

        playerController = GetComponent<PlayerController>();
        healthManager = GetComponent<HealthManager>();

        #endregion
    }
    public void Update()
    {

    }



    public IEnumerator StaminaTick()
    {
        while (true)
        {
            if (canAct)
            {
                if (canMove)
                {
                    if (isCrouch)
                    {
                        Debug.Log("I am crouched");
                        currentMoveSpeed = crouchSpeed;


                    }
                    else if (!isCrouch) 
                    {
                        if (playerController.moveDirection == Vector3.zero)
                        {
                            Debug.Log("I am standing still");
                            currentMoveSpeed = walkSpeed;

                        }
                        else if (playerController.moveDirection != Vector3.zero)
                        {
                            Debug.Log("I am moving!");
                            if (isSprintingAttempt) 
                            {
                                Debug.Log("I am attempting to sprint");
                                if (playerStamina >= 5 )
                                {
                                    Debug.Log("I am sprinting");
                                    currentMoveSpeed = sprintSpeed;

                                }
                                else if (playerStamina < 5)
                                {
                                    Debug.Log("I am out of Stamina");
                                    currentMoveSpeed = crouchSpeed;

                                }

                            }
                            else if (!isSprintingAttempt)
                            {
                                Debug.Log("I am walking");  
                                currentMoveSpeed = walkSpeed;  

                            }

                        }


                        
                    }

                }


                else if (!canMove)
                {
                    Debug.Log("I am rooted");
                    currentMoveSpeed = 0f;

                }

            }
            else if (!canAct) 
            {
                Debug.Log("I am Stuned");
                currentMoveSpeed = 0f;

            }

            staminaProgressUI.fillAmount = playerStamina / maxStamina;
            yield return new WaitForSeconds(.2f);
        }
    }


}
