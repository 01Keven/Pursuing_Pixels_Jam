using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MovableObject : MonoBehaviour
{
    private bool isDragging = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // botão direito pressionado
        {
            if (IsMouseOver() && PlayerAbilities.Instance.canMoveObjects)
            {
                isDragging = true;
            }
        }

        if (Input.GetMouseButtonUp(1)) // botão direito solto
        {
            isDragging = false;
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
