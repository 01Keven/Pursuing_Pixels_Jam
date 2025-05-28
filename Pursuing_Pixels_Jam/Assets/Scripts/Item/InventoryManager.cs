using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public inventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    public Transform ItemContent;

    public List<Item> Items = new List<Item>();

    private void Awake()
    {
        Instance = this;
    }

    int selectedSlot = -1;

    public void ListItems()
    {
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(inventoryItemPrefab, ItemContent);
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<UnityEngine.UI.Image>();
            itemIcon.sprite = item.icon;
        }
    }

    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

public bool AddItem(Item item)
{
    for (int i = 0; i < inventorySlots.Length; i++)
    {
        inventorySlot slot = inventorySlots[i];

        // Verifica se o slot já tem um ícone atribuído (já tem item)
        Image iconImage = slot.transform.Find("ItemIcon").GetComponent<Image>();
        if (iconImage != null && iconImage.sprite == null)
        {
            // Slot vazio – atribui o item
            iconImage.sprite = item.icon;

            // Armazena o item, se quiser acesso futuro
            slot.storedItem = item;

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

//     public Item GetSelectedItem()
//     {
//         inventorySlot slot = inventorySlots[selectedSlot];
//     }
}
