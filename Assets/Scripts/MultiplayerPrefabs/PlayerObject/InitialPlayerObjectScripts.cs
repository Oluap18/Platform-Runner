using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class InitialPlayerObjectScripts : NetworkBehaviour
{
    [Header( "Prefabs" )]
    [SerializeField] private GameObject serverObject; 

    // Start is called before the first frame update
    void Start()
    {
        if(IsServer && !IsHost)
        {
            if(GameObject.FindObjectOfType<ServerObjectScripts>() == null)
            {
                GameObject server = Instantiate( serverObject );
                server.GetComponent<NetworkObject>().Spawn( true );
            }
            Destroy( this );
        }
        if(IsHost)
        {
            if(GameObject.FindObjectOfType<ServerObjectScripts>() == null)
            {
                GameObject server = Instantiate( serverObject );
                server.GetComponent<NetworkObject>().Spawn( true );
            }
        }
    }
}
