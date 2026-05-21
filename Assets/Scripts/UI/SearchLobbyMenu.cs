using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchLobbyMenu : UIHandlerBase
{
    [SerializeField] private TMP_InputField lobbyNameInputField;
    [SerializeField] private Button searchLobbyButton;
    [SerializeField] private Button quitButton;
    
    [SerializeField] private int maxLobbyNameLength = 4;

    private void Awake()
    {
        searchLobbyButton.onClick.AddListener(OnSearchLobbyClicked);
        quitButton.onClick.AddListener(OnQuitClicked);
    }

    private void OnSearchLobbyClicked()
    {
        var lobbyName = lobbyNameInputField.text;

        if (ValidateLobbyName(lobbyName))
        {
            // Proceed with lobby search logic using the valid lobby name
            Debug.Log($"Searching for lobby with name: {lobbyName}");
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
}
