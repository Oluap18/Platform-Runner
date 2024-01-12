using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsMainMenu : MonoBehaviour
{

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
    private PlayerInputManager playerInputManager;

     public void OnControlsOpen()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();
        playerInputActions = playerInputManager.GetPlayerInputActions();

        KeybindActions.LoadKeybindsText( playerInputActions.PlayerMovement.Jump, jumpButtonText );
        KeybindActions.LoadKeybindsText( playerInputActions.PlayerMovement.Respawn, respawnButtonText );
        KeybindActions.LoadKeybindsText( playerInputActions.PlayerMovement.Restart, restartButtonText );

        InputAction basicMovement = playerInputActions.PlayerMovement.BasicMovement;

        KeybindActions.LoadKeybindsTextComposit( basicMovement, forwardButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_FORWARD_INDEX );
        KeybindActions.LoadKeybindsTextComposit( basicMovement, backwardsButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_BACKWARD_INDEX );
        KeybindActions.LoadKeybindsTextComposit( basicMovement, leftButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_LEFT_INDEX );
        KeybindActions.LoadKeybindsTextComposit( basicMovement, rightButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_RIGHT_INDEX );
        
        KeybindActions.LoadKeybindsCameraInverted( cameraInvertedButtonText, playerInputManager.GetInvertedCamera() );
    }

    public void ForwardButton()
    {
        InputAction s = playerInputActions.PlayerMovement.BasicMovement;
        KeybindActions.StartRebindingComposit( s, forwardButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_FORWARD_INDEX );
    }

    public void BackwardsButton()
    {
        InputAction s = playerInputActions.PlayerMovement.BasicMovement;
        KeybindActions.StartRebindingComposit( s, backwardsButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_BACKWARD_INDEX );
    }

    public void LeftButton()
    {
        InputAction s = playerInputActions.PlayerMovement.BasicMovement;
        KeybindActions.StartRebindingComposit( s, leftButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_LEFT_INDEX );
    }

    public void RightButton()
    {
        InputAction s = playerInputActions.PlayerMovement.BasicMovement;
        KeybindActions.StartRebindingComposit( s, rightButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_RIGHT_INDEX );
    }

    

    public void JumpButton()
    {

        InputAction s = playerInputActions.PlayerMovement.Jump;

        KeybindActions.StartRebinding( s, jumpButtonText );

    }

    public void CameraInvertedButton()
    {
        KeybindActions.ChangeCameraInverted( cameraInvertedButtonText );
    }

    public void PlayerRespawnButton()
    {
        InputAction s = playerInputActions.PlayerMovement.Respawn;

        KeybindActions.StartRebinding( s, respawnButtonText );
    }

    public void PlayerRestartButton()
    {
        InputAction s = playerInputActions.PlayerMovement.Restart;

        KeybindActions.StartRebinding( s, restartButtonText );
    }

    public void ExitButton()
    {

        MainMenu mainmenu = FindObjectOfType<MainMenu>();
        mainmenu.SetMainMenuActive();

        List<string> scenesToUnload = new List<string>();
        scenesToUnload.Add( SceneName.CONTROLS_MAIN_MENU_SCENE );
        LoaderCallback.SetScenesToUnload( scenesToUnload );

    }

    public void SaveKeybindsButton()
    {
        PlayerKeybinds.SavePlayerKeybinds( playerInputManager );
        ExitButton();
    }

}
