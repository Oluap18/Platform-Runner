using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(StartLevelProcedure());
    }

    IEnumerator StartLevelProcedure()
    {

        List<string> loadScenes = new List<string>();
        loadScenes.Add( SceneName.OVERLAY_UI_SCENE );
        loadScenes.Add( SceneName.DIALOGUE_SCENE );

        yield return StartCoroutine( GeneralFunctions.LoadScenes( loadScenes ) );

        loadScenes.Clear();

        SetupLevelConfiguration();

    }

    private void SetupLevelConfiguration()
    {
        List<string> loadScenes = new List<string>();

        if(this.gameObject.scene.name != SceneName.TUTORIAL_SCENE)
        {
            loadScenes.Add( SceneName.START_COUNTDOWN_TIMER_UI_SCENE );
            StartCoroutine( GeneralFunctions.LoadScenes( loadScenes ) );
        }

        if(LocalVariablesScript.singlePlayer)
        {
            NetworkManagerUI networkManagerUI = FindObjectOfType<NetworkManagerUI>();
            networkManagerUI.HostButton();
        }
    }
   
}
