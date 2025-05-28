using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItemFromWorld : MonoBehaviour
{
    public Item item;
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return; // Evita conflito com UI
        isDragging = true;
        offset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseUp()
    {
        if (!isDragging) return;
        isDragging = false;

        if (EventSystem.current.IsPointerOverGameObject()) // Está sobre a UI?
        {
            bool added = InventoryManager.Instance.AddItem(item);
            if (added)
            {
                Destroy(gameObject); // remove item do mundo
            }
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
        mousePos.z = Mathf.Abs(mainCamera.transform.position.z); // distância da câmera ao objeto
        return mainCamera.ScreenToWorldPoint(mousePos);
    }
}
