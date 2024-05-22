using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    private bool isRunning = false;

    private int staminaAtual;
    private int staminaTotal = 100;

    [SerializeField] private StaminaBar staminaBar;

    void Start()
    {
        staminaAtual = staminaTotal;

        staminaBar.AlterarStaminaBar(staminaAtual, staminaTotal);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.T) && !isRunning)
        {
            StartCoroutine(ExecuteEverySecond());
        }
        else if (!Input.GetKey(KeyCode.T) && isRunning)
        {
            StopCoroutine(ExecuteEverySecond());
            isRunning = false;
            Debug.Log("False");
        }
    }

    private IEnumerator ExecuteEverySecond()
    {
        isRunning = true;
        Debug.Log("true");

        while (isRunning && staminaAtual > 0)
        {
            GastandoStamina();
            yield return new WaitForSeconds(.1f);
        }
        while (!isRunning && staminaAtual < staminaTotal)
        {
            RecuperandoStamina();
            yield return new WaitForSeconds(.1f);
        }
    }
        private void GastandoStamina()
    {
        staminaAtual -= 5;
        if (staminaAtual < 0) staminaAtual = 0;
        staminaBar.AlterarStaminaBar(staminaAtual, staminaTotal);
    }
        private void RecuperandoStamina()
    {
        staminaAtual += 5;
        if (staminaAtual > staminaTotal) staminaAtual = staminaTotal;
        staminaBar.AlterarStaminaBar(staminaAtual, staminaTotal);
    }

}
