using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("Health Main Parameters")]
    [Range(100f, 150f)] public float maxHealth = 100f;
    [Range(0f, 150f)] public float health = 100f;

    [Range(-50f, 50f)] public float healthRegen = 1;

    [Header("Health Measurement Parameters")]
    public bool isFullHealth = true;
    public bool isAbove75Health = false;
    public bool isAbove50Health = false;
    public bool isAbove25Health = false;
    public bool isAbove1Health = false;
    public bool is0Health = false;

    [Header("Status Ailment Parameters")]
    public bool canAct = true;
    public float stunnedTimer = 0f;


    public bool canMove = true;
    public float rootedTimer = 0f;


    public bool isSlowed = false;
    public float slowedTimer = 0f;
    public float slowCoeficient = 2f;

    [Header("Capture Parameters")]
    public bool isCaptive = false;
    public bool isSacrificed = false;

    [Header("Invulnerability Parameters")]
    public bool isInvulnerable = false;
    [Range(0, 100)] public float invulnTimer = 0f;

    [Header("HealthUIParameters")]
    public Image isFullHealthImage = null;
    public Image isAbove75HealthImage = null;
    public Image isAbove50HealthImage = null;
    public Image isAbove25HealthImage = null;
    public Image isAbove1HealthImage = null;
    public Image is0HealthImage = null;

    


    private void Start()
    {
        #region Reset Parameters on Start

        health = maxHealth;
        canAct = true;
        canMove = true;
        isSlow = false;
        isCaptive = false;
        isSacrificed = false;
        isInvulnerable = false;
        invulnTimer = 0f;


        #endregion

        StartCoroutine(HealthRegenTick());

        StartCoroutine(StatusAilmentTick());

        StartCoroutine(InvulnTick());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) 
        {
            UpdateHealth(-20);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            invulnTimer += 10;
            invulnTimer = Mathf.Clamp(invulnTimer, 0, 100);
        }
    }
    void UpdateHealth(float deltaHealth)
    {
        if (deltaHealth != 0)
        {
            Debug.Log("Something Happened to my Health!");
            if (deltaHealth > 0)
            {
                Debug.Log("I was Healed!");
                if (health == maxHealth) 
                { 
                    Debug.Log("I am Not Gaining Health because i am full Health");

                }
                else if (health < maxHealth)
                {
                    Debug.Log("I am Gaining Health");
                    health += deltaHealth;

                }

            }
            else if (deltaHealth < 0)
            {
                Debug.Log("I was Hit!");
                if (health > 0)
                {
                    Debug.Log("I am Taking Damage");
                    health += deltaHealth;
                }
                else if (health <= 0 && !isInvulnerable)
                {
                    Debug.Log("I am not taking Damage because I should be dead.");

                }
                else if (isInvulnerable)
                {
                    Debug.Log("I am not taking Damage because I am Invulnerable");

                }

            }

        }

        HealthCounter();

        UpdateHealthUI(isFullHealth, isAbove75Health, isAbove50Health, isAbove25Health, isAbove1Health, is0Health);
    }
    void HealthCounter()
    {
        if (health == maxHealth)
        {
            Debug.Log("I am full Health");
            isFullHealth = true;
            isAbove75Health = false;
            isAbove50Health = false;
            isAbove25Health = false;
            isAbove1Health = false;
            is0Health = false;

}
        else if ((health / maxHealth) >= 0.75f)
        {
            Debug.Log("I am between 75% and 99% Health");
            isFullHealth = false;
            isAbove75Health = true;
            isAbove50Health = false;
            isAbove25Health = false;
            isAbove1Health = false;
            is0Health = false;

        }
        else if ((health / maxHealth) >= 0.50f)
        {
            Debug.Log("I am between 50% and 74% Health");
            isFullHealth = false;
            isAbove75Health = false;
            isAbove50Health = true;
            isAbove25Health = false;
            isAbove1Health = false;
            is0Health = false;

        }
        else if ((health / maxHealth) >= 0.25f)
        {
            Debug.Log("I am between 25% and 49% Health");
            isFullHealth = false;
            isAbove75Health = false;
            isAbove50Health = false;
            isAbove25Health = true;
            isAbove1Health = false;
            is0Health = false;

        }
        else if ((health / maxHealth) >= 0.01f)
        {
            Debug.Log("I am between 1% and 24% Health");
            isFullHealth = false;
            isAbove75Health = false;
            isAbove50Health = false;
            isAbove25Health = false;
            isAbove1Health = true;
            is0Health = false;

        }
        else if (health <= 0)
        {
            Debug.Log("I am fucking dead, bro!");
            isFullHealth = false;
            isAbove75Health = false;
            isAbove50Health = false;
            isAbove25Health = false;
            isAbove1Health = false;
            is0Health = true;

        }
    }
    void UpdateHealthUI (bool isFullHealth, bool isAbove75Health, bool isAbove50Health, bool isAbove25Health, bool isAbove1Health, bool is0Health)
    {
        if (isFullHealthImage && isAbove75HealthImage && isAbove50HealthImage && isAbove25HealthImage && isAbove1HealthImage && is0HealthImage) 
        { 
            if (isFullHealth)
            {
                isFullHealthImage.enabled = true;
                isAbove75HealthImage.enabled = false;
                isAbove50HealthImage.enabled = false;
                isAbove25HealthImage.enabled = false;
                isAbove1HealthImage.enabled = false;
                is0HealthImage.enabled = false;
            }
            else if (isAbove75Health)
            {
                isFullHealthImage.enabled = false;
                isAbove75HealthImage.enabled = true;
                isAbove50HealthImage.enabled = false;
                isAbove25HealthImage.enabled = false;
                isAbove1HealthImage.enabled = false;
                is0HealthImage.enabled = false;

            }
            else if (isAbove50Health)
            {
                isFullHealthImage.enabled = false;
                isAbove75HealthImage.enabled = false;
                isAbove50HealthImage.enabled = true;
                isAbove25HealthImage.enabled = false;
                isAbove1HealthImage.enabled = false;
                is0HealthImage.enabled = false;

            }
            else if (isAbove25Health)
            {
                isFullHealthImage.enabled = false;
                isAbove75HealthImage.enabled = false;
                isAbove50HealthImage.enabled = false;
                isAbove25HealthImage.enabled = true;
                isAbove1HealthImage.enabled = false;
                is0HealthImage.enabled = false;

            }
            else if (isAbove1Health)
            {
                isFullHealthImage.enabled = false;
                isAbove75HealthImage.enabled = false;
                isAbove50HealthImage.enabled = false;
                isAbove25HealthImage.enabled = false;
                isAbove1HealthImage.enabled = true;
                is0HealthImage.enabled = false;

            }
            else if (is0Health)
            {
                isFullHealthImage.enabled = false;
                isAbove75HealthImage.enabled = false;
                isAbove50HealthImage.enabled = false;
                isAbove25HealthImage.enabled = false;
                isAbove1HealthImage.enabled = false;
                is0HealthImage.enabled = true;

            }
            else
            {
                Debug.Log("Health Manager Error - Update Health UI Error - Unknown health State");
                isFullHealthImage.enabled = false;
                isAbove75HealthImage.enabled = false;
                isAbove50HealthImage.enabled = false;
                isAbove25HealthImage.enabled = false;
                isAbove1HealthImage.enabled = false;
                is0HealthImage.enabled = false;
            }

        }
        else
        {
            Debug.Log("Health Manager Error - Update Health UI Error - Not all UI References are Set");
        }

    }
    void TemporarelyIncincible()
    {
        Debug.Log("I am Invincible");

    }
    public IEnumerator HealthRegenTick()
    {
        while (true)
        {
            if (healthRegen == 0)
            {
                Debug.Log("I am not Regenerating!");

            }
            else if (healthRegen > 0)
            {
                Debug.Log("I am Regenerating!");
                UpdateHealth(healthRegen);

            }
            else if (healthRegen < 0)
            {
                Debug.Log("I am Poisoned!");
                UpdateHealth(healthRegen);

            }

            yield return new WaitForSeconds(.5f);
        }
    }
    public IEnumerator StatusAilmentTick()
    {
        while (true)
        {
            if (stunnedTimer > 0)
            {
                Debug.Log("I am Stunned!");
                stunnedTimer--;
                canAct = false;

                #region Monster Capture
                if (isCaptive)
                    {
                        Debug.Log("I have been Captured!");
                        if (isSacrificed)
                        {
                            Debug.Log("HE'S KILLING ME PLEASE HELP!!!");
                            UpdateHealth(-30f);

                        }

                    }
                #endregion

            }
            else if (stunnedTimer <= 0)
            {
                canAct = true;
                stunnedTimer = 0;
            }
            if (rootedTimer > 0)
            {
                Debug.Log("I am Rooted!");
                rootedTimer--;
                canMove = false;
            }
            else if (rootedTimer <= 0)
            {
                canMove = true;
                rootedTimer = 0;
            }
            if (slowedTimer > 0)
            {
                Debug.Log("I am Slowed!");
                isSlowed = true;
                slowedTimer--;
            }
            else if (slowedTimer <= 0)
            {
                isSlowed = false;
                slowedTimer = 0;
            }
            yield return new WaitForSeconds(.5f);
        }
    }
    public IEnumerator InvulnTick()
    {
        while (true)
        {
            if (invulnTimer > 0)
            {
                Debug.Log("I am currently invulnerable with" + invulnTimer + "Seconds Remaining");
                invulnTimer -= 1;
                isInvulnerable = true;
            }
            else if (invulnTimer <= 0)
            {
                Debug.Log("My InvulnTimer has run out");
                invulnTimer = 0;
                isInvulnerable = false;

            }

            yield return new WaitForSeconds(.2f);

        }
    }
    





}