using UnityEngine;


public abstract class UIHandlerBase : MonoBehaviour
{
    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] public MenuType menuType;

    public abstract void ShowMenu();
    public abstract void HideMenu();
}