using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using System.Linq;


public class NetworkRunnerManager : MonoBehaviour, INetworkRunnerCallbacks
{
    public static NetworkRunnerManager Instance; 
    [SerializeField] private NetworkRunner _networkRunnerPrefab;

    private NetworkRunner _networkRunnerInstance;
    
    private List<SessionInfo> _cachedSessionList = new List<SessionInfo>();
    
    [SerializableType] private ReadyCheckHandler readyCheckHandlerPrefab;
    private ReadyCheckHandler _readyCheckHandlerInstance;


    private void Awake()
    {
        // Ensure only one instance of NetworkRunnerManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        // Ensure only one instance of Persistant NetworkRunner exists
        if (_networkRunnerInstance == null)
        {
            _networkRunnerInstance = Instantiate(_networkRunnerPrefab);
            DontDestroyOnLoad(_networkRunnerInstance.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        _networkRunnerInstance.AddCallbacks(this);
        
        NetworkEvents.OnCreateSessionRequested+= CreateSession;
        NetworkEvents.OnJoinLobbyRequested+= JoinLobbySession;
        NetworkEvents.OnJoinSessionRequested += JoinSession;
    }

    private async void JoinLobbySession(string lobbyName)
    {
        var results = 
            await _networkRunnerInstance.JoinSessionLobby(SessionLobby.Custom, lobbyName);
        if (results.Ok)
        {
            Debug.Log($"Successfully joined lobby: {lobbyName}");
        }
        else 
        {
            Debug.LogWarning($"Failed to join lobby: {lobbyName}. Error: {results.ShutdownReason}");
        }
    }
    
    private void JoinSession(string sessionName)
    {
        var startGameArgs = new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = sessionName,
            OnGameStarted = OnGameJoined
        };
        _networkRunnerInstance.StartGame(startGameArgs);
    }

    private void OnGameJoined(NetworkRunner obj)
    {
        Debug.Log("Joined Game Successfully!");
    }


    private void CreateSession(string lobbyName, string sessionName, int maxPlayerCount)
    {
        var startGameArgs = new StartGameArgs()
        {
            CustomLobbyName = lobbyName,
            GameMode = GameMode.Shared,
            SessionName = sessionName,
            PlayerCount = maxPlayerCount,
            OnGameStarted = OnGameStarted,
        };
        _networkRunnerInstance.StartGame(startGameArgs);
    }

    private void OnGameStarted(NetworkRunner obj)
    {
        Debug.Log("Game Started Successfully!");
    }


    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        bool isLocalPlayer = false;
        if (runner.LocalPlayer == player)
        {
            isLocalPlayer = true;
            Debug.Log("Local Player Joined the Game");
        }
        Debug.Log($"OnPlayerJoined called in NetworkRunnerManager. Player: {player.PlayerId} Joined, IsLocalPlayer: {isLocalPlayer}");
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("OnConnectedToServer called in NetworkRunnerManager");
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        _cachedSessionList = sessionList ?? new List<SessionInfo>();

        if (_cachedSessionList.Count == 0)
        {
            // Lobby exists but has no sessions — notify UI to block proceeding
            NetworkEvents.PublishLobbyEmpty();
            return;
        }
        
        NetworkEvents.PublishSessionList(_cachedSessionList);
    }


    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }
}
