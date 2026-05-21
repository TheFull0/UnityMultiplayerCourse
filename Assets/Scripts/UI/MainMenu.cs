using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu: UIHandlerBase
    {
        [Header("Buttons")]
        [SerializeField] private Button JoinLobbyButton;
        [SerializeField] private Button startSessionButton;
        [SerializeField] private Button quitButton;

        private void Awake()
        {
            JoinLobbyButton.onClick.AddListener(OnJoinLobbyClicked);
            startSessionButton.onClick.AddListener(OnStartSessionClicked);
            quitButton.onClick.AddListener(OnQuitClicked);
        }

        private void OnQuitClicked()
        {
            Application.Quit();
        }

        private void OnStartSessionClicked()
        {
            UIManager.Instance.SwapMenu(MenuType.MakeSessionMenu);
        }

        private void OnJoinLobbyClicked()
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
}