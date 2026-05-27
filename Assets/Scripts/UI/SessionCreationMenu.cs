using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SessionCreationMenu: UIHandlerBase
    {
        [SerializeField] private ChooseOneLogic chooseOneLogic;
        [SerializeField] private TMP_InputField lobbyNameInputField;
        [SerializeField] private TMP_InputField sessionNameInputField;
        
        [SerializeField] private Button createSessionButton;
        [SerializeField] private Button backButton;
        
        [SerializeField] private int maxLobbyNameLength = 4;
        [SerializeField] private int maxSessionNameLength = 20;
        
        private void Awake()
        {
            createSessionButton.onClick.AddListener(OnCreateSessionClicked);
            backButton.onClick.AddListener(OnBackClicked);
        }

        private void OnBackClicked()
        {
            UIManager.Instance.SwapMenu(MenuType.MainMenu);
        }

        private void OnCreateSessionClicked()
        {
            var maxPlayersInSession = chooseOneLogic.GetSelectedOptionIndex();
            var lobbyName = lobbyNameInputField.text;
            var sessionName = sessionNameInputField.text;

            if (ValidateLobbyName(lobbyName) &&
                ValidateSessionName(sessionName))
            {
                // Proceed with session creation logic using the valid lobby name, session name, and max players
                Debug.Log($"Creating session with lobby name: {lobbyName}, session name: {sessionName}, max players: {maxPlayersInSession}");
                NetworkEvents.RequestCreateSession(lobbyName, sessionName, maxPlayersInSession);
            }
        }

        private bool ValidateSessionName(string sessionName)
        {
            if (string.IsNullOrEmpty(sessionName))
            {
                Debug.LogWarning("Session name cannot be empty. Please enter a valid session name.");
                return false;
            }

            if (sessionName.Length > maxSessionNameLength)
            {
                Debug.LogWarning($"Session name must be no longer than {maxSessionNameLength} characters. Please enter a shorter session name.");
                return false;
            }

            // Proceed with session creation logic using the valid session name
            Debug.Log($"Creating session with name: {sessionName}");
            return true;
        }

        private bool ValidateLobbyName(string lobbyName)
        {
            if (string.IsNullOrEmpty(lobbyName))
            {
                Debug.LogWarning("Lobby name cannot be empty. Please enter a valid lobby name.");
                return false;
            }

            if (lobbyName.Length > maxLobbyNameLength)
            {
                Debug.LogWarning($"Session name must be no longer than {maxLobbyNameLength} characters. Please enter a shorter session name.");
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
}