using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPickup : MonoBehaviour
{
    public RuneData item;
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;
    private SpriteRenderer spriteRenderer;
    private int originalSortingOrder;

    private void Awake()
{
    mainCamera = Camera.main;
    spriteRenderer = GetComponent<SpriteRenderer>();
}

    private void Start()
    {
        if (spriteRenderer != null)
        {
            originalSortingOrder = spriteRenderer.sortingOrder;
        }
        else
        {
            Debug.LogError("SpriteRenderer está NULL no Start em: " + gameObject.name);
        }
    }

private void OnMouseDown()
{
    if (EventSystem.current == null)
    {
        Debug.LogError("EventSystem.current está NULL!");
        return;
    }
    
    if (EventSystem.current.IsPointerOverGameObject())
    {
        Debug.Log("Pointer está sobre UI, ignorando clique.");
        return;
    }

    if (mainCamera == null)
    {
        Debug.LogError("mainCamera está NULL!");
        return;
    }

    isDragging = true;
    offset = transform.position - GetMouseWorldPos();

    if (spriteRenderer == null)
    {
        Debug.LogError("spriteRenderer está NULL em OnMouseDown! GameObject: " + gameObject.name);
    }
    else
    {
        spriteRenderer.sortingLayerName = "UI";
        spriteRenderer.sortingOrder = 1000;
    }
}



    private void OnMouseUp()
    {
        if (!isDragging) return;
        isDragging = false;

        if (EventSystem.current.IsPointerOverGameObject())
        {
            bool added = InventoryManager.Instance.AddItem(item);
            if (added)
            {
                Destroy(gameObject);
                return;
            }
        }

        // Volta para ordenação original
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = originalSortingOrder;
        }
    }

    private void Update()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(mainCamera.transform.position.z);
        return mainCamera.ScreenToWorldPoint(mousePos);
    }
}
