using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class PlayerCreatedLogic : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(!IsOwner) return;

        List<string> scenesToLoad = new List<string>();

        scenesToLoad.Add( SceneName.RECORD_ON_COMMAND_SCENE );
        scenesToLoad.Add( SceneName.RECORD_LEVEL_RUN_SCENE );

        StartCoroutine( GeneralFunctions.LoadScenes( scenesToLoad ) );
    }
}
