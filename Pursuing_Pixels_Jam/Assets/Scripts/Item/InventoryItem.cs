using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Item item;
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;

    public void Initialiseitem(Item newItem)
    {
        item = newItem;

        // Aqui pegamos a Image corretamente do filho "ItemIcon"
        if (image == null)
        {
            Transform iconTransform = transform.Find("ItemIcon");
            if (iconTransform != null)
            {
                image = iconTransform.GetComponent<Image>();
            }
            else
            {
                Debug.LogError("ItemIcon não encontrado como filho do InventoryItem!");
                return;
            }
        }

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
