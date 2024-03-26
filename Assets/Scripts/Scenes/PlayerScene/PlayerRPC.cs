using System.Collections;
using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;

public class PlayerRPC : NetworkBehaviour
{
    [Header( "References" )]
    [SerializeField] private Rigidbody playerObjectRigidbody;
    [SerializeField] private PlayerAnimator playerAnimator;

    private NetworkList<PlayerDataInGame> playersData;

    

    private void Awake()
    {
        playersData = new NetworkList<PlayerDataInGame>(null, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    }


    public override void OnNetworkSpawn()
    {
        if(IsServer)
        {
            NetworkManager.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;
        }
    }

    private void NetworkManager_OnClientConnectedCallback( ulong obj )
    {
        
        StartCoroutine( StartChangingNetworkVariable( obj ) );
    }

    private IEnumerator StartChangingNetworkVariable( ulong obj )
    {
        PlayerDataInGame newClient = new PlayerDataInGame()
        {
            _id = obj,
            _velocity = Vector3.zero,
            _forceToBeAdded = ""
        };
        playersData.Add( newClient );
        GameObject startPosition = GameObject.Find( CommonGameObjectsName.PLAYER_START_POSITION );
        this.transform.position = startPosition.transform.position;
        NetworkManager.OnClientConnectedCallback -= NetworkManager_OnClientConnectedCallback;
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ServerRpc]
    public void AddPlayerForceServerRpc( Vector3 force )
    {
        if(force == Vector3.zero) return;
        playerObjectRigidbody.AddForce( force );

    }
}
