using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class ControlsMenu : NetworkBehaviour
{

    [Header( "GeneralReferences" )]
    [SerializeField] private PlayerInputManager playerInputManager;
    [SerializeField] private KeybindActions keybindActions;

    [Header( "PauseMenuReferences" )]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject controlsMenu;

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

    // Start is called before the first frame update
    void Awake()
    {
        if(!IsOwner) return;
        playerInputActions = playerInputManager.GetPlayerInputActions();

        keybindActions.LoadKeybindsText( playerInputActions.PlayerMovement.Jump, jumpButtonText );
        keybindActions.LoadKeybindsText( playerInputActions.PlayerMovement.Respawn, respawnButtonText );
        keybindActions.LoadKeybindsText( playerInputActions.PlayerMovement.Restart, restartButtonText );

        InputAction basicMovement = playerInputActions.PlayerMovement.BasicMovement;

        keybindActions.LoadKeybindsTextComposit( basicMovement, forwardButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_FORWARD_INDEX );
        keybindActions.LoadKeybindsTextComposit( basicMovement, backwardsButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_BACKWARD_INDEX );
        keybindActions.LoadKeybindsTextComposit( basicMovement, leftButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_LEFT_INDEX );
        keybindActions.LoadKeybindsTextComposit( basicMovement, rightButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_RIGHT_INDEX );
        
        keybindActions.LoadKeybindsCameraInverted( cameraInvertedButtonText, playerInputManager.GetInvertedCamera() );
    }

    public void ForwardButton()
    {
        InputAction s = playerInputActions.PlayerMovement.BasicMovement;
        keybindActions.StartRebindingComposit( s, forwardButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_FORWARD_INDEX );
    }

    public void BackwardsButton()
    {
        InputAction s = playerInputActions.PlayerMovement.BasicMovement;
        keybindActions.StartRebindingComposit( s, backwardsButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_BACKWARD_INDEX );
    }

    public void LeftButton()
    {
        InputAction s = playerInputActions.PlayerMovement.BasicMovement;
        keybindActions.StartRebindingComposit( s, leftButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_LEFT_INDEX );
    }

    public void RightButton()
    {
        InputAction s = playerInputActions.PlayerMovement.BasicMovement;
        keybindActions.StartRebindingComposit( s, rightButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_RIGHT_INDEX );
    }

    public void JumpButton()
    {

        InputAction s = playerInputActions.PlayerMovement.Jump;

        keybindActions.StartRebinding( s, jumpButtonText );

    }

    public void CameraInvertedButton()
    {
        keybindActions.ChangeCameraInverted( cameraInvertedButtonText );
    }

    public void PlayerRespawnButton()
    {
        InputAction s = playerInputActions.PlayerMovement.Respawn;

        keybindActions.StartRebinding( s, respawnButtonText );
    }

    public void PlayerRestartButton()
    {
        InputAction s = playerInputActions.PlayerMovement.Restart;

        keybindActions.StartRebinding( s, restartButtonText );
    }

    public void ExitButton()
    {
        controlsMenu.SetActive( false );
        pauseMenu.SetActive( true );
    }

    public void SaveKeybindsButton()
    {
        PlayerKeybindsStructure playerKeybindsStructure = new PlayerKeybindsStructure( playerInputManager );
        CommonDataMethods.SaveData( CommonGameObjectsVariables.PLAYER_KEYBINDS_PATH, CommonGameObjectsVariables.PLAYER_KEYBINDS_FILENAME, playerKeybindsStructure );
        ExitButton();
    }

}
