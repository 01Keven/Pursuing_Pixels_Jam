using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Item item;
    [SerializeField] private Image itemIcon;
    [HideInInspector] public Transform parentAfterDrag;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        if (itemIcon == null)
            itemIcon = GetComponent<Image>();

        if (itemIcon == null)
            Debug.LogError("InventoryItem: Image para itemIcon não encontrada!");

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }


    public void Initialiseitem(Item newItem)
    {
        item = newItem;

        if (itemIcon != null)
            itemIcon.sprite = newItem.icon;
        else
            Debug.LogWarning("InventoryItem: itemIcon está nulo em InitialiseItem.");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;

        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            transform.SetParent(canvas.transform);
            transform.SetAsLastSibling();
        }

        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = false;
        }
    }


    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Foi solto sobre UI
            transform.SetParent(parentAfterDrag);
            transform.localScale = Vector3.one;
            transform.localPosition = Vector3.zero;

            if (canvasGroup != null)
            {
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1f;
                canvasGroup.interactable = true;
            }
        }
        else
        {
            // FOI SOLTO FORA DA UI → spawn no mundo
            Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPos.z = 0f;

            InventoryManager.Instance.SpawnWorldItem(item, spawnPos);
            Destroy(gameObject); // remove da UI
        }
    }

}
