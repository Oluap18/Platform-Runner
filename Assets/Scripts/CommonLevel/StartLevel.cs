using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour
{

    private bool optionsMenuOpen;
    private PlayerInputActions playerInputActions;

    // Start is called before the first frame update
    void Start()
    {
        optionsMenuOpen = false;
        List<string> loadScenes = new List<string> ();
        loadScenes.Add( SceneName.OVERLAY_UI_SCENE );

        StartCoroutine(GeneralFunctions.LoadScenes( loadScenes ));

        loadScenes.Clear();
        loadScenes.Add( SceneName.START_COUNTDOWN_TIMER_UI_SCENE );

        StartCoroutine(GeneralFunctions.LoadScenes( loadScenes ));

        playerInputActions = new PlayerInputActions();
        playerInputActions.PlayerMovement.Enable();
        playerInputActions.PlayerMovement.OptionsMenu.performed += OpenOptionsMenu;
    }

    private void OpenOptionsMenu( InputAction.CallbackContext obj )
    {
        if(!optionsMenuOpen) {
            List<string> loadScenes = new List<string>();
            loadScenes.Add( SceneName.OPTIONS_MENU_SCENE );

            StartCoroutine( GeneralFunctions.LoadScenes( loadScenes ) );
            loadScenes.Clear();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            optionsMenuOpen = true;
        }
    }

    public void CloseOptionsMenu()
    {
        optionsMenuOpen = false;
    }
}
