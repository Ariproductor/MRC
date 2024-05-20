using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToOrigin : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        // Obtém o componente NavMeshAgent anexado a este GameObject
        agent = GetComponent<NavMeshAgent>();

        // Define o destino do agente para as coordenadas (0, 0, 0)
        agent.SetDestination(Vector3.zero);
    }
    void Update()
    {
        if (agent != null)
        {
            // Define o destino do agente para as coordenadas (0, 0, 0)
            agent.SetDestination(Vector3.zero);
        }
    }
}

