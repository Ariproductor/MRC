using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] private Player myPlayer;
    private Vector3 movimento;

    private void Start()
    {
        movimento = myPlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Detecta os inputs do jogador e os transforma em variáveis
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Transforma as variáveis em uma unica variavel de movimento.
        Vector3 movimento = new Vector3(horizontal, 0, vertical);


        //Zera e impossibilita o Movimento vertical atraves desse tipo de movimento
        movimento.y = 0;

        if (movimento != Vector3.zero)
        {
            //Atribui a rotação do personagem de acordo com a direção do movimento
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movimento), Time.deltaTime * 10);
        }
    }
}
