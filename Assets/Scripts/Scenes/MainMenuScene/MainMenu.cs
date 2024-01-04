using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private void Start()
    {
        SceneManager.LoadScene( SceneName.ALWAYS_RUNNING_SCENE, LoadSceneMode.Additive );
    }
    public void PlayGame()
    {
        
        List<string> scenesToLoad = new List<string>();
        scenesToLoad.Add( SceneName.LEVEL_1 );
        scenesToLoad.Add( SceneName.PLAYER_SCENE );
        scenesToLoad.Add( SceneName.OVERLAY_UI_SCENE );
        LoaderCallback.SetScenesToLoad( scenesToLoad );

        List<string> scenesToUnload = new List<string>();
        scenesToUnload.Add( SceneName.MAIN_MENU_SCENE );
        LoaderCallback.SetScenesToUnload( scenesToUnload );
        SceneManager.LoadScene( SceneName.LOADING_SCENE, LoadSceneMode.Additive );

    }
}
