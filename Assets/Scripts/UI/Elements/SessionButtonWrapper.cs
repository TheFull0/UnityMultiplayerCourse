using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class SessionButtonWrapper : MonoBehaviour
{
    [SerializeField] private GameObject sessionListParentContainer;
    [SerializeField] private SessionButton _sessionButtonPrefab;
    [SerializeField] private GameObject highlight;
    private List<SessionButton> _sessionButtons;
    
    private int _currentSessionIndexChosen = 0;
    
    private void Awake()
    {
        _sessionButtons = new List<SessionButton>();
    }
    
    public void UpdateSessionButtons(List<SessionInfo> sessionInfos)
    {
        // Update or create buttons
        for (int i = 0; i < sessionInfos.Count; i++)
        {
            if (i < _sessionButtons.Count)
            {
                _sessionButtons[i].SessionIndex = i;
                _sessionButtons[i].SessionInfo = sessionInfos[i];
            }
            else
            {
                var newButton = Instantiate(_sessionButtonPrefab, sessionListParentContainer.transform);
                newButton.Initialize(this);
                newButton.SessionIndex = i;
                newButton.SessionInfo = sessionInfos[i];
                _sessionButtons.Add(newButton);
            }
        }

        // Destroy excess buttons
        for (int i = _sessionButtons.Count - 1; i >= sessionInfos.Count; i--)
        {
            Destroy(_sessionButtons[i].gameObject);
            _sessionButtons.RemoveAt(i);
        }
    }

    public void ButtonClicked(int sessionIndex)
    {
        if (sessionIndex < 0 || sessionIndex >= _sessionButtons.Count)
        {
            Debug.LogWarning($"Invalid session index clicked: {sessionIndex}");
            return;
        }
        
        var sessionInfo = _sessionButtons[sessionIndex].SessionInfo;
        highlight.transform.position = _sessionButtons[sessionIndex].transform.position;
        
        if (sessionInfo == null)
        {
            Debug.LogWarning($"SessionInfo is null for button at index: {sessionIndex}");
            return;
        }
        
        Debug.Log($"Join button clicked for session: {sessionInfo.Name}");
        _currentSessionIndexChosen = sessionIndex;
    }

    public SessionInfo GetCurrentChosenSessionInfo()
    {
        
        return _sessionButtons[_currentSessionIndexChosen].SessionInfo;
    }
}