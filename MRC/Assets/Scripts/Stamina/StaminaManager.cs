using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    [Range(0, 100)][SerializeField] public float playerStamina = 100f;
    [Range(100, 150)][SerializeField] public float maxStamina = 100f;

    public float staminaDrain = 10f;
    public float staminaRegen = 0.5f;

    private bool isSprinting = false;

    public void SetSprinting(bool sprinting)
    {
        isSprinting = sprinting;
    }

    private void Start()
    {
        StartCoroutine(StaminaTick());
    }

    private IEnumerator StaminaTick()
    {
        while (true)
        {
            if (!isSprinting && playerStamina < maxStamina)
            {
                playerStamina += staminaRegen;
            }
            else if (isSprinting && playerStamina >= staminaDrain)
            {
                playerStamina -= staminaDrain;
            }
            else if (isSprinting && playerStamina < staminaDrain)
            {
                playerStamina = Mathf.Max(0, playerStamina - staminaDrain);
            }

            yield return new WaitForSeconds(.1f);
        }
    }
}
