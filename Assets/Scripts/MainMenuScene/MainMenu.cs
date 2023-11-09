using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {

        //SceneManager.LoadScene( "MainMenu" );

    }


    public void PlayGame()
    {

        List<string> scenesToLoad = new List<string>();
        scenesToLoad.Add( "SampleScene" );
        scenesToLoad.Add( "PlayerScene" );
        LoaderCallback.SetScenesToLoad( scenesToLoad );

        List<string> scenesToUnload = new List<string>();
        scenesToUnload.Add( "MainMenuScene" );
        LoaderCallback.SetScenesToUnload( scenesToUnload );
        SceneManager.LoadScene( "LoadingScene", LoadSceneMode.Additive );
        
    }
}
