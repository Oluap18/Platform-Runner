using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private StartLevel startLevel;

    [Header( "PauseMenuReferences" )]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject controlsMenu;

    // Update is called once per frame
    void Start()
    {
        startLevel = FindObjectOfType<StartLevel>();
    }

    public void ControlsButton()
    {
        pauseMenu.SetActive( false );
        controlsMenu.SetActive( true );

    }

    public void ResumeButton()
    {
        startLevel.CloseOptionsMenu();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        List<string> unloadScenes = new List<string>();
        unloadScenes.Add( SceneName.OPTIONS_MENU_SCENE );

        StartCoroutine( GeneralFunctions.UnLoadScenes( unloadScenes ) );
    }

    public void ExitButton()
    {
        SceneManager.LoadScene( SceneName.MAIN_MENU_SCENE );
    }
}
