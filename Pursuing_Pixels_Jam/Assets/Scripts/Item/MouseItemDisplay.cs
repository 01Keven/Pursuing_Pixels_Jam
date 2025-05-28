using UnityEngine;
using UnityEngine.UI;

public class MouseItemDisplay : MonoBehaviour
{
    public static MouseItemDisplay Instance;

    private Image iconImage;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        Instance = this;

        iconImage = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();

        Hide();
    }

    private void Update()
    {
        if (canvasGroup.alpha > 0)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void Show(Sprite icon)
    {
        if (icon == null) return;

        iconImage.sprite = icon;
        canvasGroup.alpha = 1;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
    }
}
