using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;

    private Transform originalParent;
    private Transform canvasTransform;

    private void Start()
{
    mainCamera = Camera.main;

    // Corrigido para Unity 2023+
    Canvas canvas = Object.FindFirstObjectByType<Canvas>();
    if (canvas != null)
    {
        canvasTransform = canvas.transform;
    }

    originalParent = transform.parent;
}


    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return; // evita conflito com UI
        isDragging = true;
        offset = transform.position - GetMouseWorldPos();

        // Move para o Canvas para aparecer sobre a UI
        if (canvasTransform != null)
        {
            originalParent = transform.parent;
            transform.SetParent(canvasTransform, true); // true mantém posição mundial
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
                Destroy(gameObject); // removido com sucesso
                return;
            }
        }

        // Se não foi solto sobre a UI, volta pro parent original
        transform.SetParent(originalParent, true);
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
        mousePos.z = Mathf.Abs(mainCamera.transform.position.z); // distância da câmera ao objeto
        return mainCamera.ScreenToWorldPoint(mousePos);
    }
}
