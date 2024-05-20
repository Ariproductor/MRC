using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoJogador : MonoBehaviour
{

    private CharacterController controller;
    private Transform myCamera;
    private int moveSpeed = 5;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        myCamera = Camera.main.transform;
    }

    void Update()
    {

        //Detecta os inputs do jogador e os transforma em variáveis
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Transforma as variáveis em uma unica variavel de movimento.
        Vector3 movimento = new Vector3 (horizontal,0 , vertical);


        //Zera e impossibilita o Movimento vertical atraves desse tipo de movimento
        movimento.y = 0;

        //usa a função move do CharacterController, tratando da movimentação do personagem.
        controller.Move(movimento * Time.deltaTime * moveSpeed);
        //usa a função move do CharacterController, tratando da gravidade sobre o personagem
        controller.Move(new Vector3 (0, -10, 0) * Time.deltaTime * moveSpeed);


        if (movimento != Vector3.zero)
        {
            //Atribui a rotação do personagem de acordo com a direção do movimento
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movimento), Time.deltaTime * 10);
        }

    }
}
