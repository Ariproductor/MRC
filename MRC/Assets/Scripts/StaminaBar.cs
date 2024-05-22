using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private Image staminaBarImage;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AlterarStaminaBar(int staminaAtual, int staminaMaxima)
    {
        staminaBarImage.fillAmount = (float)staminaAtual / staminaMaxima;
    }
}
