using Fusion;
using UnityEngine;

public class ReadyCheckHandler : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(AnotherPlayerReady))] private int _readyPlayerCount { get; set; }

    public override void Spawned()
    {
        Debug.Log(Object.HasStateAuthority
            ? "Ready Check Handler spawned with state authority."
            : "Ready Check Handler spawned without state authority.");
    }

    private void AnotherPlayerReady()
    {
        
    }
}
