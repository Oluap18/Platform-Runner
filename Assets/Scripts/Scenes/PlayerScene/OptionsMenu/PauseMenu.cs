using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class PauseMenu : NetworkBehaviour
{

    [Header( "PauseMenuReferences" )]
    [SerializeField] private PlayerInputManager playerInputManager;


    public void ControlsButton()
    {
        playerInputManager.OpenControlsMenu();

    }

    public void ResumeButton()
    {
        playerInputManager.CloseOptionsMenu();
    }

    public void ExitButton()
    {
        NetworkManager.Singleton.Shutdown();
        Destroy( NetworkManager.Singleton.gameObject );
        SceneManager.LoadScene( SceneName.MAIN_MENU_SCENE );
    }
}
