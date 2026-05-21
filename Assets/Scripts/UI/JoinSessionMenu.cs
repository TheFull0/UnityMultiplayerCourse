using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class JoinSessionMenu : UIHandlerBase
{
    [SerializeField] private Button _startSessionButton;
    [SerializeField] private Button _joinSessionButton;
    [SerializeField] private Button _refreshButton;
    [SerializeField] private Button _backButton;
    
    private void Awake()
    {
        _startSessionButton.onClick.AddListener(OnStartSessionClicked);
        _joinSessionButton.onClick.AddListener(OnJoinSessionClicked);
        _backButton.onClick.AddListener(OnBackClicked);
    }

    private void OnStartSessionClicked()
    {
        UIManager.Instance.SwapMenu(MenuType.MakeSessionMenu);
    }

    private void OnJoinSessionClicked()
    {
        
    }

    private void OnBackClicked()
    {
        UIManager.Instance.SwapMenu(MenuType.JoinLobbyMenu);
    }


    public override void ShowMenu()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public override void HideMenu()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
