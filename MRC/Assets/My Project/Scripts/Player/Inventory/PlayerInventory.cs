using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        items.Add(item);
        Debug.Log("Item adicionado ao inventário!");
        // Você pode adicionar lógica adicional aqui, como atualizar o UI do inventário
    }
}
