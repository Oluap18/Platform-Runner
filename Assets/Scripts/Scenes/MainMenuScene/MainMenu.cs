using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField] private GameObject mainMenu;

    private void Start()
    {
        SceneManager.LoadScene( SceneName.ALWAYS_RUNNING_SCENE, LoadSceneMode.Additive );
    }
    public void PlayGame()
    {
        List<string> scenesToLoad = new List<string>();
        scenesToLoad.Add( SceneName.LEVEL_1_SCENE );
        scenesToLoad.Add( SceneName.PLAYER_SCENE );
        LoaderCallback.SetScenesToLoad( scenesToLoad );

        List<string> scenesToUnload = new List<string>();
        scenesToUnload.Add( SceneName.MAIN_MENU_SCENE );
        LoaderCallback.SetScenesToUnload( scenesToUnload );

        SceneManager.LoadScene( SceneName.LOADING_SCENE, LoadSceneMode.Additive );

    }

    public void Controls()
    {
        SceneManager.LoadScene( SceneName.CONTROLS_MAIN_MENU_SCENE, LoadSceneMode.Additive );
        mainMenu.SetActive( false );
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetMainMenuActive()
    {
        mainMenu.SetActive( true );
    }
}
