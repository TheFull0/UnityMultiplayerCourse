using System;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SessionButton : MonoBehaviour
{
    [SerializeField] private Button joinButton;
    [SerializeField] private TMP_Text indexText;
    [SerializeField] private TMP_Text sessionNameText;
    [SerializeField] private TMP_Text playerCountText;

    private SessionButtonWrapper _sessionButtonWrapper;
    public int SessionIndex { get; set; }

    private SessionInfo _sessionInfo;

    public SessionInfo SessionInfo
    {
        get => _sessionInfo;
        set
        {
            if (value == null)
            {
                Debug.LogWarning("Attempted to set SessionInfo to null. This is not allowed.");
                return;
            }

            if (value != _sessionInfo)
            {
                _sessionInfo = value;
                UpdateSessionTexts();
            }
        }
    }

    public void Awake()
    {
        if (joinButton == null)
        {
            Debug.LogError("Join Button reference is missing in SessionButton.");
        }
        else
        {
            joinButton.onClick.AddListener(OnJoinButtonClicked);
        }
    }

    public void Initialize(SessionButtonWrapper wrapper)
    {
        if (wrapper == null)
        {
            Debug.LogError("SessionButtonWrapper reference is missing when initializing SessionButton.");
            return;
        }
        _sessionButtonWrapper = wrapper;
    }

    private void UpdateSessionTexts()
    {
        if (_sessionInfo == null)
        {
            Debug.LogWarning("SessionInfo is null. Cannot update session texts.");
            return;
        }

        indexText.text = $"{SessionIndex.ToString()}.";
        sessionNameText.text = _sessionInfo.Name;
        playerCountText.text = $"{_sessionInfo.PlayerCount}/{_sessionInfo.MaxPlayers}";
    }

    private void OnJoinButtonClicked()
    {
        if (_sessionInfo == null)
        {
            Debug.LogWarning("SessionInfo is null. Cannot join session.");
            return;
        }

        Debug.Log($"Join button clicked for session: {_sessionInfo.Name}");
        _sessionButtonWrapper.ButtonClicked(SessionIndex);
    }
}