using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Item item;
    [HideInInspector] public Image image;
    [HideInInspector] public Transform parentAfterDrag;

    // private void Start()
    // {
    //     Initialiseitem(item);
    // }

    public void Initialiseitem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.icon;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root); // Garante que fica por cima dos outros elementos
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag); // Caso não solte em nenhum slot válido
    }
}
