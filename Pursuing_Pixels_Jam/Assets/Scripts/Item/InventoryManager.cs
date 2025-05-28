using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public GameObject[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public Transform ItemContent;
    public List<Item> Items = new List<Item>(); // Inicialmente vazia

    private void Awake()
    {
        Instance = this;
    }

    public void ListItems()
    {
        // Limpa itens antigos antes de listar novamente
        foreach (Transform child in ItemContent)
        {
            Destroy(child.gameObject);
        }

        // Adiciona novos itens se houver
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(inventoryItemPrefab, ItemContent);
            var inventoryItem = obj.GetComponent<InventoryItem>();
            inventoryItem.Initialiseitem(item);
        }
    }

public bool AddItem(Item item)
{
    for (int i = 0; i < inventorySlots.Length; i++)
    {
        Transform slot = inventorySlots[i].transform;

        // Verifica se o slot já tem um item dentro (ícone, etc.)
        if (slot.childCount == 0)
        {
            GameObject newItemGO = Instantiate(inventoryItemPrefab, slot);
            InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
            inventoryItem.Initialiseitem(item);
            return true;
        }
    }

    Debug.Log("Inventário cheio!");
    return false;
}


    void SpawnNewItem(Item item, inventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.Initialiseitem(item);
    }

    public void ClearInventory()
    {
        Items.Clear();

        foreach (var slot in inventorySlots)
        {
            foreach (Transform child in slot.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
