using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Item item;
    [SerializeField] private Image itemIcon;
    [HideInInspector] public Transform parentAfterDrag;

    private void Awake()
    {
        if (itemIcon == null)
        {
            Transform iconTransform = transform.Find("ItemIcon");
            if (iconTransform != null)
                itemIcon = iconTransform.GetComponent<Image>();
            else
                itemIcon = GetComponent<Image>(); // fallback
        }
    }

    public void Initialiseitem(Item newItem)
    {
        item = newItem;
        itemIcon.sprite = newItem.icon;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemIcon.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);

        // Mostra o ícone do item no mouse (pegando o mesmo ícone que este InventoryItem já mostra)
        MouseItemDisplay.Instance.Show(itemIcon.sprite);
    }


    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemIcon.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;

        // Esconde o ícone do mouse
        MouseItemDisplay.Instance.Hide();
    }

}
