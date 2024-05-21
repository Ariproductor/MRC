using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 10f; // Raio de detecção do jogador
    public float attackRadius = 2f; // Raio de ataque
    private NavMeshAgent agent;
    private bool isPlayerInRange;
    private int damage = 10;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        FindPlayer();
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRadius)
            {
                isPlayerInRange = true;
            }
            else
            {
                isPlayerInRange = false;
            }

            if (isPlayerInRange)
            {
                agent.SetDestination(player.position);

                if (distanceToPlayer <= attackRadius)
                {
                    // Adicionar lógica de ataque aqui
                    Debug.Log("Atacar jogador");


                }
            }
            else
            {
                agent.SetDestination(transform.position); // Inimigo para de se mover quando jogador está fora de alcance
            }
        }
    }
    void FindPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }
    void OnDrawGizmosSelected()
    {
        // Desenhar gizmos para visualizar o raio de detecção e ataque no editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}


