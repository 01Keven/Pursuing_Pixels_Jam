using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Esse script é responsável por gerenciar o item do inventário, permitindo que ele seja arrastado e solto na UI ou no mundo do jogo.
public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public RuneData item;
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


    public void Initialiseitem(RuneData newItem) // Método para inicializar o item na UI
    {
        item = newItem;

        if (itemIcon != null)
            itemIcon.sprite = newItem.runeIcon;
        else
            Debug.LogWarning("InventoryItem: itemIcon está nulo em InitialiseItem.");
    }

    public void OnBeginDrag(PointerEventData eventData) // Método chamado quando o arrasto começa
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


    public void OnDrag(PointerEventData eventData) // Método chamado enquanto o item está sendo arrastado
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) // Método chamado quando o arrasto termina
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
        else // Se não foi solto sobre UI, então é um spawn no mundo
        {
            // FOI SOLTO FORA DA UI → spawn no mundo
            Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPos.z = 0f;

            InventoryManager.Instance.SpawnWorldItem(item, spawnPos, gameObject.GetComponentInParent<inventorySlot>().slotType);
            Destroy(gameObject); // remove da UI
        }
    }

}
