using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterBase
{
    private GameObject player; // Refer�ncia ao GameObject com a tag "Player"
    private Transform playerTransform; // Transform do player
    private Transform myTransform; // Transform deste objeto
    private Vector3 direction; // Dire��o para o player
    private float followRadius = 2f; // Raio de seguir o jogador
    public override void Start()
    {
        SetMaxHealth();


        FindPlayer();


        base.Start();
    }
    public override void Update()
    {
        FindDirection();

        base.Update();
    }
    protected virtual void SetMaxHealth()
    {
        maxHealth = 100;
    }
    public virtual void FindPlayer()
    {
        // Encontra o objeto com o tag "Player" e pega seu Transform. "player"
        player = GameObject.FindGameObjectWithTag("Player");

        // Obtendo refer�ncias ao transform deste objeto e do player
        myTransform = transform;
        playerTransform = player.transform;

        // Obtendo o componente Rigidbody deste objeto
        rb = GetComponent<Rigidbody>();
    }
    public virtual void FindDirection() 
    { 
        // Calcular a dist�ncia para o jogador
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        Debug.Log(distanceToPlayer);


        // Calculando a dire��o para o player
        movementDirection = (playerTransform.position - myTransform.position).normalized;
        Debug.Log(movementDirection);

        // Verificar se o jogador est� dentro do raio de seguir
        if (distanceToPlayer >= followRadius)
        {
            // Aplicar movimento na dire��o do jogador
            rb.AddForce(movementDirection * moveSpeed);
            //ApplyMovement(movementDirection);
        }

    }
}
 