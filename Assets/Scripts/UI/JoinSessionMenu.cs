using System.Collections.Generic;
using DefaultNamespace;
using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class JoinSessionMenu : UIHandlerBase
{
    [SerializeField] private Button _startSessionButton;
    [SerializeField] private Button _joinSessionButton;
    [SerializeField] private Button _refreshButton;
    [SerializeField] private Button _backButton;

    [SerializeField] private SessionButtonWrapper sessionButtonPrefab;

    private void Awake()
    {
        _startSessionButton.onClick.AddListener(OnStartSessionClicked);
        _joinSessionButton.onClick.AddListener(OnJoinSessionClicked);
        _backButton.onClick.AddListener(OnBackClicked);

        NetworkEvents.OnSessionListReceived += OnSessionListReceived;
    }

    private void OnSessionListReceived(List<SessionInfo> sessionInfos)
    {
        if (sessionInfos.Count == 0)
        {
            Debug.LogWarning("No sessions found in lobby.");
            // Show error UI feedback here
            return;
        }

        Debug.Log($"Received {sessionInfos.Count} sessions from the network.");
        foreach (var session in sessionInfos)
        {
            Debug.Log($"Session Name: {session.Name}, Players: {session.PlayerCount}/{session.MaxPlayers}");
        }

        UpdateButtonWrapper(sessionInfos);
    }

    private void UpdateButtonWrapper(List<SessionInfo> info)
    {
        if (sessionButtonPrefab == null)
        {
            Debug.LogError("SessionButtonWrapper reference is missing in JoinSessionMenu.");
            return;
        }
        
        sessionButtonPrefab.UpdateSessionButtons(info);
    }

    private void OnStartSessionClicked()
    {
        UIManager.Instance.SwapMenu(MenuType.MakeSessionMenu);
    }

    private void OnJoinSessionClicked()
    {
        NetworkEvents.RequestJoinSession("TEST");
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