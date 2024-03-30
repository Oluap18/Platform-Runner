using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour
{
    [Header( "References" )]
    [SerializeField] private PlayerInputManager playerInputManager;
    [SerializeField] private PlayerGeneralFunctions playerGeneralFunctions;

    private bool optionsMenuOpen;
    private PlayerInputActions playerInputActions;

    // Start is called before the first frame update
    void Awake()
    {
        playerInputActions = playerInputManager.GetPlayerInputActions();
        StartCoroutine(StartLevelProcedure());
    }

    IEnumerator StartLevelProcedure()
    {
        SetupInitialBindings();

        optionsMenuOpen = false;

        List<string> loadScenes = new List<string>();
        loadScenes.Add( SceneName.DIALOGUE_SCENE );

        yield return StartCoroutine( GeneralFunctions.LoadScenes( loadScenes ) );

        loadScenes.Clear();

        SetupLevelConfiguration();

    }

    private void SetupLevelConfiguration()
    {
        /*List<string> loadScenes = new List<string>();

        if(this.gameObject.scene.name != SceneName.TUTORIAL_SCENE)
        {
            loadScenes.Add( SceneName.START_COUNTDOWN_TIMER_UI_SCENE );
            StartCoroutine( GeneralFunctions.LoadScenes( loadScenes ) );
        }*/
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
            playerGeneralFunctions.DisableMovementOfPlayer();
            List<string> loadScenes = new List<string>();
            loadScenes.Add( SceneName.OPTIONS_MENU_SCENE );

            StartCoroutine( GeneralFunctions.LoadScenes( loadScenes ) );
            loadScenes.Clear();
            optionsMenuOpen = true;
        }
    }

    public void CloseOptionsMenu()
    {
        playerGeneralFunctions.EnablePlayerMovement();
        optionsMenuOpen = false;
    }

    private void SetupInitialBindings()
    {
        PlayerKeybindsStructure playerKeybindsStructure = CommonDataMethods.LoadData( CommonGameObjectsVariables.PLAYER_KEYBINDS_PATH, CommonGameObjectsVariables.PLAYER_KEYBINDS_FILENAME ) as PlayerKeybindsStructure;
        playerInputManager.GetPlayerInputActions().LoadBindingOverridesFromJson( playerKeybindsStructure.playerInputActions );
        playerInputManager.SetCameraSensitivity( playerKeybindsStructure.cameraSensitivity );
        playerInputManager.SetInvertedCamera( playerKeybindsStructure.invertedCamera );
    }
}
