using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelPicker;

    private void Awake()
    {
        SceneManager.LoadScene( SceneName.ALWAYS_RUNNING_SCENE, LoadSceneMode.Additive );
    }

    public void PlayTutorial()
    {
        List<string> scenesToLoad = new List<string>();
        scenesToLoad.Add( SceneName.TUTORIAL_SCENE );
        LoaderCallback.SetScenesToLoad( scenesToLoad );
        CommonPlayLevel();
    }

    public void PlayLevel1()
    {
        List<string> scenesToLoad = new List<string>();
        scenesToLoad.Add( SceneName.LEVEL_1_SCENE );
        LoaderCallback.SetScenesToLoad( scenesToLoad );
        CommonPlayLevel();
    }

    public void PlaySample()
    {
        List<string> scenesToLoad = new List<string>();
        scenesToLoad.Add( SceneName.SAMPLE_SCENE );
        LoaderCallback.SetScenesToLoad( scenesToLoad );
        CommonPlayLevel();
    }

    private void CommonPlayLevel()
    {
        List<string> scenesToLoad = new List<string>();

        //If just replay, then don't load the player
        if(!RecordPlayerRun.replay)
        {
            scenesToLoad.Add( SceneName.PLAYER_SCENE );
        }

        scenesToLoad.Add( SceneName.BOTS_SCENE );
        scenesToLoad.Add( SceneName.RECORD_ON_COMMAND_SCENE );
        scenesToLoad.Add( SceneName.RECORD_LEVEL_RUN_SCENE );
        LoaderCallback.SetScenesToLoad( scenesToLoad );

        List<string> scenesToUnload = new List<string>();
        scenesToUnload.Add( SceneName.MAIN_MENU_SCENE );
        LoaderCallback.SetScenesToUnload( scenesToUnload );

        SceneManager.LoadScene( SceneName.LOADING_SCENE, LoadSceneMode.Additive );
    }

    public void Controls()
    {
        SceneManager.LoadScene( SceneName.CONTROLS_MAIN_MENU_SCENE, LoadSceneMode.Additive );
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetMainMenuActive()
    {
        mainMenu.SetActive( true );
    }

    public void SetMainMenuDisabled()
    {
        mainMenu.SetActive( false );
    }

    public void SetLevelPicker()
    {
        levelPicker.SetActive( true );
        mainMenu.SetActive( false );
    }

    public void ExitLevelPicker()
    {
        levelPicker.SetActive( false );
        mainMenu.SetActive( true );
    }
}
