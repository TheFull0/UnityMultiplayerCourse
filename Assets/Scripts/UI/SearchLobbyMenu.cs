using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchLobbyMenu : UIHandlerBase
{
    [SerializeField] private TMP_InputField lobbyNameInputField;
    [SerializeField] private Button searchLobbyButton;
    [SerializeField] private Button quitButton;
    
    [SerializeField] private int maxLobbyNameLength = 4;

    private bool isValidLobby;

    private void Awake()
    {
        searchLobbyButton.onClick.AddListener(OnSearchLobbyClicked);
        quitButton.onClick.AddListener(OnQuitClicked);
        
        NetworkEvents.OnSessionListReceived += SessionListReceived;
        NetworkEvents.OnLobbyWasEmpty += OnLobbyWasEmpty;

    }

    private void OnLobbyWasEmpty()
    {
        searchLobbyButton.interactable = true;
        Debug.LogWarning("Lobby has no sessions or does not exist.");
        // Show error UI feedback here
    }

    private void SessionListReceived(List<SessionInfo> sessions)
    {
        Debug.Log($"Received {sessions.Count} sessions from the network.");
        foreach (var session in sessions)
        {
            Debug.Log($"Session Name: {session.Name}, Players: {session.PlayerCount}/{session.MaxPlayers}");
        }
        
        searchLobbyButton.interactable = true;
        UIManager.Instance.SwapMenu(MenuType.JoinSessionMenu);
    }

    private void OnSearchLobbyClicked()
    {
        var lobbyName = lobbyNameInputField.text;

        if (ValidateLobbyName(lobbyName))
        {
            Debug.Log($"Searching for lobby: {lobbyName}");
            NetworkEvents.RequestJoinLobby(lobbyName);
        }
    }

    private void OnQuitClicked()
    {
        UIManager.Instance.SwapMenu(MenuType.MainMenu);
    }

    private bool ValidateLobbyName(string lobbyName)
    {
        if (lobbyName == "TEST")
        {
            UIManager.Instance.SwapMenu(MenuType.JoinSessionMenu);
            return true;
        }
        
        if (string.IsNullOrEmpty(lobbyName))
        {
            Debug.LogWarning("Lobby name cannot be empty. Please enter a valid lobby name.");
            return false;
        }

        if (lobbyName.Length > maxLobbyNameLength)
        {
            Debug.LogWarning($"Session name must be shorter than {maxLobbyNameLength} characters. Please enter a shorter session name.");
            return false;
        }

        // Proceed with session creation logic using the valid lobby name
        Debug.Log($"Creating session with lobby name: {lobbyName}");
        return true;
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
    
    
    private void OnDestroy()
    {
        NetworkEvents.OnSessionListReceived -= SessionListReceived;
        NetworkEvents.OnLobbyWasEmpty -= OnLobbyWasEmpty;
    }
}
