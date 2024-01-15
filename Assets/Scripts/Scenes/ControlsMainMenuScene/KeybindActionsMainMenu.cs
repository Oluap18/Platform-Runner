using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputActionRebindingExtensions;

public class KeybindActionsMainMenu : MonoBehaviour
{

    private static PlayerInputManager playerInputManager;
    private static InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    private static bool startRebindObject;


    private void Start()
    {
        startRebindObject = false;
        playerInputManager = FindObjectOfType<PlayerInputManager>();
    }

    public static void StartRebindingComposit( InputAction action, TextMeshProUGUI textKeybind, int index )
    {
        if(!startRebindObject) {

            startRebindObject = true;

            PrepareRebind();
            textKeybind.gameObject.SetActive( false );

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
            textKeybind.gameObject.SetActive( false );

            rebindingOperation = action.PerformInteractiveRebinding()
                    .WithControlsExcluding( "Mouse" )
                    .OnMatchWaitForAnother( 0.1f )
                    .OnComplete( operation => RebindComplete( action, textKeybind ) )
                    .Start();

        }
    }

    private static void PrepareRebind()
    {
        playerInputManager.GetPlayerInputActions().PlayerMovement.Disable();
        playerInputManager.GetPlayerInputActions().RebindAuxiliarMenu.Enable();
    }

    private static void RebindComplete( InputAction action, TextMeshProUGUI textKeybind )
    {

        LoadKeybindsText(action, textKeybind);
        rebindingOperation.Dispose();
        playerInputManager.GetPlayerInputActions().PlayerMovement.Enable();
        playerInputManager.GetPlayerInputActions().RebindAuxiliarMenu.Disable();
        startRebindObject = false;
        textKeybind.gameObject.SetActive( true );

    }

    private static void RebindCompleteComposit( InputAction action, TextMeshProUGUI textKeybind, int index )
    {

        LoadKeybindsTextComposit( action, textKeybind, index );
        rebindingOperation.Dispose();
        playerInputManager.GetPlayerInputActions().PlayerMovement.Enable();
        playerInputManager.GetPlayerInputActions().RebindAuxiliarMenu.Disable();
        startRebindObject = false;
        textKeybind.gameObject.SetActive( true );

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

    public static void LoadKeybindsCameraInverted( TextMeshProUGUI textKeybind )
    {
        if(playerInputManager.GetInvertedCamera() == 1) {
            textKeybind.text = "Default";
        }
        if(playerInputManager.GetInvertedCamera() == -1) {
            textKeybind.text = "Inverted";
        }
    }

    public static void LoadKeybindsCameraInverted( TextMeshProUGUI textKeybind, int inverted )
    {
        if(inverted == 1) {
            textKeybind.text = "Default";
        }
        if(inverted == -1) {
            textKeybind.text = "Inverted";
        }
    }

    public static void ChangeCameraInverted( TextMeshProUGUI textKeybind )
    {
        playerInputManager.SetInvertedCamera( playerInputManager.GetInvertedCamera() * -1 );
        LoadKeybindsCameraInverted( textKeybind );
    }
}
