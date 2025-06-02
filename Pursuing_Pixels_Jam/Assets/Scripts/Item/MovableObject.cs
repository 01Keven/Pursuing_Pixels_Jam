using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MovableObject : MonoBehaviour
{
    private bool isDragging = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // botão direito pressionado
        {
            if (IsMouseOver() && PlayerAbilities.Instance.hasTelecinesis)
            {
                isDragging = true;
                gameObject.layer = 9; // Muda o layer para "Telekinesis" (layer 9)
            }
        }

        if (Input.GetMouseButtonUp(1)) // botão direito solto
        {
            isDragging = false;
            gameObject.layer = 0; // Restaura o layer original (layer 0)
        }

        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
        }
    }

    private bool IsMouseOver()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D col = GetComponent<Collider2D>();
        return col == Physics2D.OverlapPoint(mousePos);
    }
}
