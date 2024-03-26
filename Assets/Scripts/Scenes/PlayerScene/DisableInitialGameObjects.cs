using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class DisableInitialGameObjects : NetworkBehaviour
{
    [Header( "Initially Disabled Objects" )]
    [SerializeField] private GameObject optionsMenuCanvas;

    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        optionsMenuCanvas.SetActive( false );
    }
}
