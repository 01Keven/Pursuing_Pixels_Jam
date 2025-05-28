using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

private void OnMouseDown() // ou outro evento
{
    InventoryManager.Instance.AddItem(item); // item é o ScriptableObject
    Destroy(gameObject); // ou desativar o item do mundo
}

}
