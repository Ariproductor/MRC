using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController controller;
    private Transform myCamera;
    private int moveSpeed = 5;
    private Vector3 movimento = Vector3.zero;


    private int vidaAtual;
    private int vidaTotal = 100;
    [SerializeField] private BarraDeVida barraDeVida;


    private bool isRunning = false;
    private int staminaAtual;
    private int staminaTotal = 100;
    [SerializeField] private StaminaBar staminaBar;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        myCamera = Camera.main.transform;

        vidaAtual = vidaTotal;
        barraDeVida.AlterarBarraDeVida(vidaAtual, vidaTotal);

        staminaAtual = staminaTotal;
        staminaBar.AlterarStaminaBar(staminaAtual, staminaTotal);

    }

    // Update is called once per frame
    void Update()
    {
        GerarMovimento();
        AplicarMovimento();



        if (Input.GetKeyDown(KeyCode.Space))
        {
            AplicarDano(10);
        }


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
        moveSpeed = 10;
    }
    private void RecuperandoStamina()
    {
        staminaAtual += 5;
        if (staminaAtual > staminaTotal) staminaAtual = staminaTotal;
        staminaBar.AlterarStaminaBar(staminaAtual, staminaTotal);
        moveSpeed = 5;
    }
    private void AplicarDano(int dano)
    {
        vidaAtual -= 10;
        barraDeVida.AlterarBarraDeVida(vidaAtual, vidaTotal);
    }
    private void GerarMovimento()
    {

        //Detecta os inputs do jogador e os transforma em variáveis
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Transforma as variáveis em uma unica variavel de movimento.
        movimento = new Vector3(horizontal, 0, vertical);


        //Zera e impossibilita o Movimento vertical atraves desse tipo de movimento
        movimento.y = 0;

    }
    private void AplicarMovimento()
    {
        //usa a função move do CharacterController, tratando da movimentação do personagem.
        controller.Move(movimento * Time.deltaTime * moveSpeed);
        //usa a função move do CharacterController, tratando da gravidade sobre o personagem
        controller.Move(new Vector3(0, -10, 0) * Time.deltaTime * moveSpeed);
    }
}
