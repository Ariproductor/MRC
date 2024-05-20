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

        //Detecta os inputs do jogador e os transforma em vari�veis
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Transforma as vari�veis em uma unica variavel de movimento.
        Vector3 movimento = new Vector3 (horizontal,0 , vertical);


        //Zera e impossibilita o Movimento vertical atraves desse tipo de movimento
        movimento.y = 0;

        //usa a fun��o move do CharacterController, tratando da movimenta��o do personagem.
        controller.Move(movimento * Time.deltaTime * moveSpeed);
        //usa a fun��o move do CharacterController, tratando da gravidade sobre o personagem
        controller.Move(new Vector3 (0, -10, 0) * Time.deltaTime * moveSpeed);


        if (movimento != Vector3.zero)
        {
            //Atribui a rota��o do personagem de acordo com a dire��o do movimento
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movimento), Time.deltaTime * 10);
        }

    }
}
