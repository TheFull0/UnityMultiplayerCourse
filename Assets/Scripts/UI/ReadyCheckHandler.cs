using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class ReadyCheckHandler : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(AnotherPlayerReady))] private int _readyPlayerCount { get; set; }

    

    public override void Spawned()
    {
        Debug.Log(Object.HasStateAuthority
            ? "Ready Check Handler spawned with state authority."
            : "Ready Check Handler spawned without state authority.");
        
        NetworkRunnerManager.Instance.SetReadyCheckHandler(this);

        NetworkEvents.OnPlayerReady += PlayerReadyRPC;
        NetworkEvents.OnPlayerNotReady += PlayerNotReadyRPC;
    }

    private void AnotherPlayerReady()
    {
        NetworkEvents.PublishPlayerReadyStatusChanged(_readyPlayerCount);
    }

    [Rpc]
    private void PlayerReadyRPC(PlayerRef player)
    {
        if (Object.HasStateAuthority)
        {
            Debug.Log($"Player {player} is ready.");
            _readyPlayerCount++;

            int totalPlayersNotReady = _readyPlayerCount;
            
            foreach (var activePlayer in Runner.ActivePlayers)
            {
                totalPlayersNotReady--;
            }

            if (totalPlayersNotReady == 0)
            {
                Debug.Log("All players are ready. can start the game.");
                NetworkEvents.PublishAllPlayersReady();
            }
        }
        
    }

    [Rpc]
    private void PlayerNotReadyRPC(PlayerRef player)
    {
        if (Object.HasStateAuthority)
        { 
            Debug.Log($"Player {player} is ready.");
            _readyPlayerCount--;
        }
    }
}
