using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public inventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    int selectedSlot = -1;

    // private void Start()
    // {
    //     ChangeSelectedSlot(1);
    // }

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
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
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
