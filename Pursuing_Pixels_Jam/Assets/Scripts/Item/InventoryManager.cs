using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public GameObject[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public Transform ItemContent;
    public GameObject worldItemPrefab; // arraste o prefab do item com SpriteRenderer aqui via inspector
    public List<RuneData> Items = new List<RuneData>(); // Inicialmente vazia

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

    public bool AddItem(RuneData item)
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
                RuneManager.Instance.UpdateAbilities(); // Atualiza as habilidades do RuneManager após equipar a runa
                return true;
            }
        }

        Debug.Log("Inventário cheio!");
        return false;
    }


    void SpawnNewItem(RuneData item, inventorySlot slot)
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
    


    public void SpawnWorldItem(RuneData item, Vector3 position, SlotType slot)
    {
        GameObject obj = Instantiate(worldItemPrefab, position, Quaternion.identity);

        // Define a escala para 1 (tamanho original)
        Vector3 originalScale = worldItemPrefab.transform.localScale;
        obj.transform.localScale = originalScale;


        ItemPickup pickup = obj.GetComponent<ItemPickup>(); // Obtém o componente ItemPickup do objeto instanciado
        if (pickup != null)
        {
            pickup.item = item;

            SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.sprite = item.runeIcon;
                renderer.sortingLayerName = "Gameplay"; // Define a camada de ordenação para UI
                renderer.sortingOrder = 0; // Define a ordem de renderização para que fique acima de outros objetos no mundo
            }
                
                RuneManager.Instance.RemoveRune(item, slot); // Remove a runa do RuneManager após spawnar no mundo
        }
    }


}
