using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputActionRebindingExtensions;

public class KeybindActions : MonoBehaviour
{

    private static PlayerInputActions playerInputActions;
    private static InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    private static bool startRebindObject;


    private void Start()
    {
        startRebindObject = false;
        playerInputActions = FindObjectOfType<PlayerInputManager>().getPlayerInputActions();
    }

    public static void StartRebindingComposit( InputAction action, TextMeshProUGUI textKeybind, int index )
    {
        if(!startRebindObject) {

            startRebindObject = true;

            PrepareRebind();

            rebindingOperation = action.PerformInteractiveRebinding()
                    .WithTargetBinding(index)
                    .WithControlsExcluding( "Mouse" )
                    .OnMatchWaitForAnother( 0.1f )
                    .OnComplete( operation => RebindCompleteComposit( action, textKeybind, index ) )
                    .Start();

        }
    }

    public static void StartRebinding(InputAction action, TextMeshProUGUI textKeybind )
    {
        if(!startRebindObject) {

            startRebindObject = true;
            
            PrepareRebind();

            rebindingOperation = action.PerformInteractiveRebinding()
                    .WithControlsExcluding( "Mouse" )
                    .OnMatchWaitForAnother( 0.1f )
                    .OnComplete( operation => RebindComplete( action, textKeybind ) )
                    .Start();

        }
    }

    private static void PrepareRebind()
    {
        playerInputActions.PlayerMovement.Disable();
        playerInputActions.RebindAuxiliarMenu.Enable();
    }

    private static void RebindComplete( InputAction action, TextMeshProUGUI textKeybind )
    {

        LoadKeybindsText(action, textKeybind);
        rebindingOperation.Dispose();
        playerInputActions.PlayerMovement.Enable();
        playerInputActions.RebindAuxiliarMenu.Disable();

        startRebindObject = false;

    }

    private static void RebindCompleteComposit( InputAction action, TextMeshProUGUI textKeybind, int index )
    {

        LoadKeybindsTextComposit( action, textKeybind, index );
        rebindingOperation.Dispose();
        playerInputActions.PlayerMovement.Enable();
        playerInputActions.RebindAuxiliarMenu.Disable();

        startRebindObject = false;

    }

    public static void LoadKeybindsText( InputAction action, TextMeshProUGUI textKeybind )
    {
        int bindingIndex = action.GetBindingIndexForControl( action.controls[0] );

        textKeybind.text = InputControlPath.ToHumanReadableString(
            action.bindings[bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice );
    }

    public static void LoadKeybindsTextComposit( InputAction action, TextMeshProUGUI textKeybind, int index )
    {

        textKeybind.text = InputControlPath.ToHumanReadableString(
            action.bindings[index].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice );
    }
}
