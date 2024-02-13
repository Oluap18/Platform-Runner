using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour
{

    private bool optionsMenuOpen;
    private PlayerInputActions playerInputActions;
    private PlayerInputManager playerInputManager;

    // Start is called before the first frame update
    void Awake()
    {
        optionsMenuOpen = false;

        List<string> loadScenes = new List<string>();
        loadScenes.Add( SceneName.OVERLAY_UI_SCENE );

        StartCoroutine( GeneralFunctions.LoadScenes( loadScenes ) );

        loadScenes.Clear();
        loadScenes.Add( SceneName.START_COUNTDOWN_TIMER_UI_SCENE );

        StartCoroutine( GeneralFunctions.LoadScenes( loadScenes ) );

        playerInputManager = FindObjectOfType<PlayerInputManager>();
        playerInputActions = playerInputManager.GetPlayerInputActions();

        PlayerKeybindsStructure playerKeybindsStructure = CommonDataMethods.LoadData( CommonGameObjectsVariables.PLAYER_KEYBINDS_PATH, CommonGameObjectsVariables.PLAYER_KEYBINDS_FILENAME ) as PlayerKeybindsStructure;
        playerInputManager.GetPlayerInputActions().LoadBindingOverridesFromJson( playerKeybindsStructure.playerInputActions );
        playerInputManager.SetCameraSensitivity( playerKeybindsStructure.cameraSensitivity );
        playerInputManager.SetInvertedCamera( playerKeybindsStructure.invertedCamera );

    }

    private void OnEnable()
    {
        playerInputActions.PlayerMovement.OptionsMenu.performed += OpenOptionsMenu;
    }

    private void OnDisable()
    {
        playerInputActions.PlayerMovement.OptionsMenu.performed -= OpenOptionsMenu;
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
