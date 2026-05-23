using System;
using System.Collections.Generic;
using Fusion;

public static class NetworkEvents
{
    public static event Action<string, string, int> OnCreateSessionRequested;
    public static event Action<string>              OnJoinLobbyRequested;
    public static event Action<string>              OnJoinSessionRequested;
    public static event Action                      OnDisconnectRequested;
    public static event Action<List<SessionInfo>>   OnSessionListReceived;
    public static event Action OnLobbyWasEmpty;

    public static void RequestCreateSession(string lobbyName, string sessionName, int maxPlayers)
        => OnCreateSessionRequested?.Invoke(lobbyName, sessionName, maxPlayers);

    public static void RequestJoinLobby(string lobbyName)
        => OnJoinLobbyRequested?.Invoke(lobbyName);

    public static void RequestDisconnect()
        => OnDisconnectRequested?.Invoke();
    
    public static void RequestJoinSession(string sessionName)
        => OnJoinSessionRequested?.Invoke(sessionName);
    

    public static void PublishSessionList(List<SessionInfo> sessions)
        => OnSessionListReceived?.Invoke(sessions);
    
    public static void PublishLobbyEmpty() => OnLobbyWasEmpty?.Invoke();

}