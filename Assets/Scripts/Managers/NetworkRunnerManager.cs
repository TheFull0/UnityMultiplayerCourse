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
        
        _networkRunnerInstance.AddCallbacks();
    }

    private void Start()
    {
        
    }
    
    
    public bool SessionExists(string sessionName)
    {
        if (string.IsNullOrEmpty(sessionName)) return false;

        // Quick check using the runner's LobbyInfo if you're already in a lobby
        if (_networkRunnerInstance != null)
        {
            var lobby = _networkRunnerInstance.LobbyInfo;
            if (lobby.IsValid && string.Equals(lobby.Name, sessionName, StringComparison.Ordinal))
                return true;
        }

        // Check the cached session list from OnSessionListUpdated
        return _cachedSessionList.Any(s => string.Equals(s.Name, sessionName, StringComparison.Ordinal));
    }

// Replace the existing OnSessionListUpdated implementation with this:
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        // Cache the list for later queries
        _cachedSessionList = sessionList != null ? new List<SessionInfo>(sessionList) : new List<SessionInfo>();

        // Optional: debug/log
        Debug.Log($"Session list updated: {_cachedSessionList.Count} sessions cached.");
    }

// Example usage (can be called after you receive session list or in Start)
    private void ExampleCheck(string name)
    {
        bool exists = SessionExists(name);
        Debug.Log($"Session '{name}' exists: {exists}");
    }
    
    
    


    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        throw new NotImplementedException();
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        throw new NotImplementedException();
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        throw new NotImplementedException();
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        throw new NotImplementedException();
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        throw new NotImplementedException();
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
        throw new NotImplementedException();
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
        throw new NotImplementedException();
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        throw new NotImplementedException();
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        throw new NotImplementedException();
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }
    

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        throw new NotImplementedException();
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        throw new NotImplementedException();
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }
}
