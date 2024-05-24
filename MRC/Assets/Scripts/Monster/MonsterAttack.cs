using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class MonsterAttack : MonsterBase
{



    [Header("Basic Parameters")]
    public float damage = 10f; //Quantidade de dano causado pelo ataque 
    public float attackRange = 1f; //Alcance do ataque
    public LayerMask defaultlayer; //camada que o jogador está
    public Light attacklight;
    public GameObject detectedPlayer;

    public GameObject player; //referência ao jogador
    public bool playerInRange; //verificar se o jogador está no alcance

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //encontrar o jogador na cena
        
        attacklight.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null) 
        {
            detectedPlayer = other.gameObject;
            Debug.Log("Player detectado" + detectedPlayer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == detectedPlayer) 
        {
            detectedPlayer = null;
            Debug.Log("Player saiu");
        }
    }
    private void Update()
    {
    //Verifica se o jogador está no alcance de ataque
    //playerInRange = Physics.CheckSphere(transform.position, attackRange, defaultlayer);

    //Se o jogador estiver no alcance, ataque
    //if (playerInRange)
    //{
    //Attack();
    //}

        if (detectedPlayer != null)
        {
            attacklight.enabled = true;
        }
        else if (detectedPlayer = null) 
        { 
            attacklight.enabled = false;
        }
    }

    void Attack()
    {
        //Logica para causa dan no jogador
        Debug.Log("Inimigo bateu!");
        attacklight.enabled = true;
    }

  
}
