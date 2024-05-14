using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    protected Rigidbody rb;

    public int damageTaken;

    public Vector3 currentPosition;
    public int currentHealth = 1;

    public int maxHealth = 1;
    public int defense = 1;

    public int moveSpeed = 5;
    public int moveDrag = 5;
    public Vector3 movementDirection;

    public int attackRange;
    public int attackSpeed;
    public int attackDamage = 10;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = moveDrag;
        ResetHealth();
    }
    public virtual void Update()
    {
        UpdatePosition();
    }
    public virtual void ResetHealth()
    {
        currentHealth = maxHealth;
    }
    public virtual void UpdatePosition()
    {
        currentPosition = transform.position;
    }
    public void ApplyMovement(Vector3 movementDirection)
    {
        // Normalizar o vetor de direção para manter uma velocidade constante quando se move na diagonal
        movementDirection.Normalize();

        // Aplicar movimento
        rb.AddForce (movementDirection * moveSpeed);
    }
    public void Attack()
    {

    }
    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
