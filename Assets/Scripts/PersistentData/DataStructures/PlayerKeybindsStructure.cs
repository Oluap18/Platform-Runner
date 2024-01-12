using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class PlayerKeybindsStructure
{

    public string playerInputActions;
    public int invertedCamera;
    public float cameraSensitivity;

    public PlayerKeybindsStructure( PlayerInputManager playerInputManager )
    {
        this.playerInputActions = playerInputManager.GetPlayerInputActions().SaveBindingOverridesAsJson();
        this.invertedCamera = playerInputManager.GetInvertedCamera();
        this.cameraSensitivity = playerInputManager.GetCameraSensitivity();
    }
}
