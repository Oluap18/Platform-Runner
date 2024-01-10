using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsMenu : MonoBehaviour
{

    [Header( "PauseMenuReferences" )]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject controlsMenu;

    [Header( "Text Keybindings" )]
    [SerializeField] private TextMeshProUGUI forwardButtonText;
    [SerializeField] private TextMeshProUGUI backwardsButtonText;
    [SerializeField] private TextMeshProUGUI rightButtonText;
    [SerializeField] private TextMeshProUGUI leftButtonText;
    [SerializeField] private TextMeshProUGUI jumpButtonText;
    [SerializeField] private TextMeshProUGUI cameraButtonText;
    [SerializeField] private TextMeshProUGUI CameraSensitivityButtonText;

    private PlayerInputActions playerInputActions;


    // Start is called before the first frame update
    void Start()
    {
        
        playerInputActions = FindObjectOfType<PlayerInputManager>().getPlayerInputActions();

        KeybindActions.LoadKeybindsText( playerInputActions.PlayerMovement.Jump, jumpButtonText );

        InputAction basicMovement = playerInputActions.PlayerMovement.BasicMovement;

        KeybindActions.LoadKeybindsTextComposit( basicMovement, forwardButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_FORWARD_INDEX );
        KeybindActions.LoadKeybindsTextComposit( basicMovement, backwardsButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_BACKWARD_INDEX );
        KeybindActions.LoadKeybindsTextComposit( basicMovement, leftButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_LEFT_INDEX );
        KeybindActions.LoadKeybindsTextComposit( basicMovement, rightButtonText, CommonGameObjectsVariables.PLAYER_INPUT_ACTIONS_RIGHT_INDEX );

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

    public void CameraButton()
    {

    }

    public void CameraSensitivityButton()
    {

    }

    public void ExitButton()
    {

        controlsMenu.SetActive( false );
        pauseMenu.SetActive( true );

    }

}
