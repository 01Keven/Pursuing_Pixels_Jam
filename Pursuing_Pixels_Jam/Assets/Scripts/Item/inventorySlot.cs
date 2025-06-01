using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class inventorySlot : MonoBehaviour, IDropHandler
{
    // public Item storedItem;
    public Image image;
    public Color selectedColor, notSelectedColor;

    public SlotType slotType;
    //public bool isEmpty => storedItem == null; // Propriedade para verificar se o slot est� vazio
    private void Awake()
    {
        if (image == null)
            image = GetComponent<Image>();

        Deselect();
    }

    public void Select()
    {
        if (image != null)
            image.color = selectedColor;
    }

    public void Deselect()
    {
        if (image != null)
            image.color = notSelectedColor;
    }

    public void OnDrop(PointerEventData eventData) // M�todo chamado quando um item � solto neste slot
    {
        InventoryItem draggedItem = eventData.pointerDrag.GetComponent<InventoryItem>();

        if (draggedItem == null)
            return;

        if (transform.childCount == 0)
        {
            draggedItem.parentAfterDrag = transform;
            draggedItem.transform.SetParent(transform);
            draggedItem.transform.localPosition = Vector3.zero;
        }
        else
        {
            Transform itemInThisSlot = transform.GetChild(0);
            InventoryItem otherItem = itemInThisSlot.GetComponent<InventoryItem>();

            Transform previousSlot = draggedItem.parentAfterDrag;

            otherItem.transform.SetParent(previousSlot);
            otherItem.transform.localPosition = Vector3.zero;
            otherItem.parentAfterDrag = previousSlot;

            draggedItem.transform.SetParent(transform);
            draggedItem.transform.localPosition = Vector3.zero;
            draggedItem.parentAfterDrag = transform;
        }
    }
}
