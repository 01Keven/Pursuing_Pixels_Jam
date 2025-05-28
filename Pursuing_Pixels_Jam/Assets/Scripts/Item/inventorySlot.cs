
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class inventorySlot : MonoBehaviour, IDropHandler
{
    public Item storedItem; // Guarda o item atual no slot (opcional)
    public Image image;
    public Color selectedColor, notSelectedColor;

    private void Awake()
    {
        Deselect();
    }
    
    public void Select()
    {
        image.color = selectedColor;
    }

    public void Deselect()
    {
        image.color = notSelectedColor;
    }





    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem draggedItem = eventData.pointerDrag.GetComponent<InventoryItem>();

        // Verifica se há item no slot atual
        if (transform.childCount == 0)
        {
            // Slot vazio - apenas move o item
            draggedItem.parentAfterDrag = transform;
            draggedItem.transform.SetParent(transform);
        }
        else
        {
            // Slot ocupado - troca os itens
            Transform itemInThisSlot = transform.GetChild(0);
            InventoryItem otherItem = itemInThisSlot.GetComponent<InventoryItem>();

            // Troca os pais
            otherItem.transform.SetParent(draggedItem.parentAfterDrag);
            draggedItem.transform.SetParent(transform);

            // Atualiza o parentAfterDrag de cada item
            otherItem.parentAfterDrag = draggedItem.parentAfterDrag;
            draggedItem.parentAfterDrag = transform;
        }
    }
}
