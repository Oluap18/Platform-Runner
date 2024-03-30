using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ServerObjectScripts : NetworkBehaviour
{
    private List<ulong> clientIDs;
    private int playerCount = 2;
    private Dictionary<ulong, float> clientTimes;
    // Start is called before the first frame update

    public override void OnNetworkSpawn()
    {
        clientIDs = new List<ulong>();
        clientTimes = new Dictionary<ulong, float>();
        if(IsServer)
        {
            NetworkManager.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;
        }
        if(IsHost)
        {
            StartCoroutine( ClientAdd( OwnerClientId ) );
        }
    }

    private void NetworkManager_OnClientConnectedCallback( ulong obj )
    {

        StartChangingNetworkVariable( obj );
    }

    private void StartChangingNetworkVariable( ulong obj )
    {
        NetworkManager.OnClientConnectedCallback -= NetworkManager_OnClientConnectedCallback;
        StartCoroutine( ClientAdd( obj ) );
    }

    private IEnumerator ClientAdd( ulong obj )
    {
        Debug.Log( IsServer );
        clientIDs.Add( obj );
        Debug.Log( "Added Client: " + obj );
        if(clientIDs.Count == playerCount)
        {
            StartGame();
        }
        yield return null;
    }

    private void StartGame()
    {
        if(!IsServer) return;
        Debug.Log( OwnerClientId );
        StartCountdownTimer[] countdownTimers = FindObjectsOfType<StartCountdownTimer>();

        while(countdownTimers.Length != playerCount)
        {
            countdownTimers = FindObjectsOfType<StartCountdownTimer>();
        }
        Debug.Log( "Found all StartCountdownTimers" );
        for(int i = 0; i < countdownTimers.Length; i++)
        {
            countdownTimers[i].StartCountDownClientRpc();
        }
    }
}
