using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        items.Add(item);
        Debug.Log("Item adicionado ao invent�rio!");
        // Voc� pode adicionar l�gica adicional aqui, como atualizar o UI do invent�rio
    }
}
