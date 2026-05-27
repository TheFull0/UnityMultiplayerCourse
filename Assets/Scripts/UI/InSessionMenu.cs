using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InSessionMenu : UIHandlerBase
{
    [SerializeField] private Button ReadyButton;

    [SerializeField] private Button StartGameButton;
    [SerializeField] private TMP_Text SumOfReadyPlayersText;
    
    [SerializeField] private GameObject ReadyIndicator;

    bool isReady = false;
    
    private void Awake()
    {
        ReadyIndicator.SetActive(false);
        StartGameButton.gameObject.SetActive(false);
        
        ReadyButton.onClick.AddListener(OnReadyClicked);
        StartGameButton.onClick.AddListener(StartGameClicked);
        
        NetworkEvents.OnPlayerReadyStatusChanged += UpdateReadyStatus;
        NetworkEvents.OnAllPlayersReady += () => StartGameButton.gameObject.SetActive(true);
    }

    private void StartGameClicked()
    {
        // This will trigger the game start logic in the NetworkRunnerManager or a similar class
        NetworkEvents.RequestStartGame();
    }

    private void UpdateReadyStatus(int obj)
    {
        SumOfReadyPlayersText.text = $"Ready Players: {obj}";
        if (NetworkRunnerManager.Instance.GetPlayerCountInCurrentSession() != obj)
        {
            StartGameButton.gameObject.SetActive(false);
        }
    }


    private void OnReadyClicked()
    {
        isReady = !isReady;
        ReadyIndicator.SetActive(isReady);

        if (isReady)
        {
            NetworkEvents.PublishPlayerReady();
        }
        else
        {
            NetworkEvents.PublishPlayerNotReady();
        }
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