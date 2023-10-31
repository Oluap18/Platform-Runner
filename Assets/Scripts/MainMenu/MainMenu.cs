using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {

        SceneManager.LoadScene( "MainMenu" );

    }


    public void PlayGame()
    {
        SceneManager.LoadScene( "SampleScene" );
    }
}
