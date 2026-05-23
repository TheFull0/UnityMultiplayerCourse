using System.Collections.Generic;
using DefaultNamespace;
using Fusion;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class JoinSessionMenu : UIHandlerBase
{
    [SerializeField] private Button _startSessionButton;
    [SerializeField] private Button _joinSessionButton;
    [SerializeField] private Button _refreshButton;
    [SerializeField] private Button _backButton;

    [FormerlySerializedAs("sessionButtonPrefab")] [SerializeField] private SessionButtonWrapper sessionButtonWrapper;

    private void Awake()
    {
        _startSessionButton.onClick.AddListener(OnStartSessionClicked);
        _joinSessionButton.onClick.AddListener(OnJoinSessionClicked);
        _refreshButton.onClick.AddListener(OnRefreshClicked);
        _backButton.onClick.AddListener(OnBackClicked);

        NetworkEvents.OnSessionListReceived += OnSessionListReceived;
    }

    private void OnRefreshClicked()
    {
        
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
        if (sessionButtonWrapper == null)
        {
            Debug.LogError("SessionButtonWrapper reference is missing in JoinSessionMenu.");
            return;
        }
        
        sessionButtonWrapper.UpdateSessionButtons(info);
    }

    private void OnStartSessionClicked()
    {
        UIManager.Instance.SwapMenu(MenuType.MakeSessionMenu);
    }

    private void OnJoinSessionClicked()
    {
        var sessionInfo = sessionButtonWrapper.GetCurrentChosenSessionInfo();
        if (sessionInfo == null)
        {
            Debug.LogWarning("No session selected to join.");
            // Show error UI feedback here
            return;
        }
        
        Debug.Log($"Attempting to join session: {sessionInfo.Name}");
        NetworkEvents.RequestJoinSession(sessionInfo.Name);
        
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