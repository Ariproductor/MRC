using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BarraDeVida : MonoBehaviour
{
    [SerializeField] private Image barraDeVidaImage;

    private void Awake()
    {

    }

    void Update()
    {

    }

    public void AlterarBarraDeVida(int vidaAtual, int vidaMaxima)
    {
        barraDeVidaImage.fillAmount = (float)vidaAtual / vidaMaxima;
    }
}
