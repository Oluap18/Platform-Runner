using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsMainMenu : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private PlayerInputManager playerInputManager;

    [Header( "Text Keybindings" )]
    [SerializeField] private TextMeshProUGUI forwardButtonText;
    [SerializeField] private TextMeshProUGUI backwardsButtonText;
    [SerializeField] private TextMeshProUGUI rightButtonText;
    [SerializeField] private TextMeshProUGUI leftButtonText;
    [SerializeField] private TextMeshProUGUI jumpButtonText;
    [SerializeField] private TextMeshProUGUI cameraInvertedButtonText;
    [SerializeField] private TextMeshProUGUI respawnButtonText;
    [SerializeField] private TextMeshProUGUI restartButtonText;

    private PlayerInputActions playerInputActions;
    private MainMenu mainmenu;

     void Awake()
    {

        mainmenu = FindObjectOfType<MainMenu>();
        mainmenu.SetMainMenuDisabled();
        playerInputManager = FindObjectOfType<PlayerInputManager>();
        
        PlayerKeybindsStructure playerKeybindsStructure = CommonDataMethods.LoadData( CommonGameObjectsVariables.PLAYER_KEYBINDS_PATH, CommonGameObjectsVariables.PLAYER_KEYBINDS_FILENAME ) as PlayerKeybindsStructure;
        playerInputManager.GetPlayerInputActions().LoadBindingOverridesFromJson( playerKeybindsStructure.playerInputActions );
        playerInputManager.SetCameraSensitivity( playerKeybindsStructure.cameraSensitivity );
        playerInputManager.SetInvertedCamera( playerKeybindsStructure.invertedCamera );

        playerInputActions = playerInputManager.GetPlayerInputActions();

        KeybindActionsMainMenu.LoadKeybindsText( playerInputActions.PlayerMovement.Jump, jumpButtonText );
        KeybindActionsMainMenu.LoadKeybindsText( playerInputActions.PlayerMovement.Respawn, respawnButtonText );
        KeybindActionsMainMenu.LoadKeybindsText( playerInputActions.PlayerMovement.Restart, restartButtonText );

        InputAction basicMovement = playerInputActions.PlayerMovement.BasicMovement;

        KeybindActionsMainMenu.LoadKeybindsTextComposit( basicMovement, forwardButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_FORWARD_INDEX );
        KeybindActionsMainMenu.LoadKeybindsTextComposit( basicMovement, backwardsButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_BACKWARD_INDEX );
        KeybindActionsMainMenu.LoadKeybindsTextComposit( basicMovement, leftButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_LEFT_INDEX );
        KeybindActionsMainMenu.LoadKeybindsTextComposit( basicMovement, rightButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_RIGHT_INDEX );
        
        KeybindActionsMainMenu.LoadKeybindsCameraInverted( cameraInvertedButtonText, playerInputManager.GetInvertedCamera() );
    }

    public void ForwardButton()
    {
        InputAction s = playerInputActions.PlayerMovement.BasicMovement;
        KeybindActionsMainMenu.StartRebindingComposit( s, forwardButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_FORWARD_INDEX );
    }

    public void BackwardsButton()
    {
        InputAction s = playerInputActions.PlayerMovement.BasicMovement;
        KeybindActionsMainMenu.StartRebindingComposit( s, backwardsButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_BACKWARD_INDEX );
    }

    public void LeftButton()
    {
        InputAction s = playerInputActions.PlayerMovement.BasicMovement;
        KeybindActionsMainMenu.StartRebindingComposit( s, leftButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_LEFT_INDEX );
    }

    public void RightButton()
    {
        InputAction s = playerInputActions.PlayerMovement.BasicMovement;
        KeybindActionsMainMenu.StartRebindingComposit( s, rightButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_RIGHT_INDEX );
    }

    

    public void JumpButton()
    {

        InputAction s = playerInputActions.PlayerMovement.Jump;

        KeybindActionsMainMenu.StartRebinding( s, jumpButtonText );

    }

    public void CameraInvertedButton()
    {
        KeybindActionsMainMenu.ChangeCameraInverted( cameraInvertedButtonText );
    }

    public void PlayerRespawnButton()
    {
        InputAction s = playerInputActions.PlayerMovement.Respawn;

        KeybindActionsMainMenu.StartRebinding( s, respawnButtonText );
    }

    public void PlayerRestartButton()
    {
        InputAction s = playerInputActions.PlayerMovement.Restart;

        KeybindActionsMainMenu.StartRebinding( s, restartButtonText );
    }

    public void ExitButton()
    {

        mainmenu.SetMainMenuActive();

        List<string> scenesToUnload = new List<string>();
        scenesToUnload.Add( SceneName.CONTROLS_MAIN_MENU_SCENE );
        StartCoroutine( GeneralFunctions.UnLoadScenes( scenesToUnload ) );
       
    }

    public void SaveKeybindsButton()
    {
        PlayerKeybindsStructure playerKeybindsStructure = new PlayerKeybindsStructure( playerInputManager );
        CommonDataMethods.SaveData( CommonGameObjectsVariables.PLAYER_KEYBINDS_PATH, CommonGameObjectsVariables.PLAYER_KEYBINDS_FILENAME, playerKeybindsStructure );
        ExitButton();
    }

}
