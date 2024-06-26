using UnityEngine;
using Photon.Pun;

public class Item : MonoBehaviourPunCallbacks
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Adiciona o item ao jogador (implemente esta fun��o conforme necess�rio)
            other.GetComponent<PlayerInventory>().AddItem(this);

            // Destr�i o item em todas as inst�ncias de jogo (rede)
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
