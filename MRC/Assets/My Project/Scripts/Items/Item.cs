using UnityEngine;
using Photon.Pun;

public class Item : MonoBehaviourPunCallbacks
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Adiciona o item ao jogador (implemente esta função conforme necessário)
            other.GetComponent<PlayerInventory>().AddItem(this);

            // Destrói o item em todas as instâncias de jogo (rede)
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
