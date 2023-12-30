using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private void Start()
    {

    }


    public void PlayGame()
    {

        List<string> scenesToLoad = new List<string>();
        scenesToLoad.Add( SceneName.SAMPLE_SCENE );
        scenesToLoad.Add( SceneName.PLAYER_SCENE );
        scenesToLoad.Add( SceneName.OVERLAY_UI_SCENE );
        scenesToLoad.Add( SceneName.ALWAYS_RUNNING_SCENE );
        LoaderCallback.SetScenesToLoad( scenesToLoad );

        List<string> scenesToUnload = new List<string>();
        scenesToUnload.Add( SceneName.MAIN_MENU_SCENE );
        LoaderCallback.SetScenesToUnload( scenesToUnload );
        SceneManager.LoadScene( SceneName.LOADING_SCENE, LoadSceneMode.Additive );

    }
}
