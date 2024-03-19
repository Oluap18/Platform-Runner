using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerCheckPoint : MonoBehaviour
{

    private PlayerInputActions playerInputActions;
    private CheckPointManager checkPointManager;


    void Awake()
    {
        playerInputActions = FindObjectOfType<PlayerInputManager>().GetPlayerInputActions();
        checkPointManager = FindObjectOfType<CheckPointManager>();
    }

    private void OnEnable()
    {
        playerInputActions.PlayerMovement.Respawn.performed += RespawnStill;
        playerInputActions.PlayerMovement.Respawn.started += RespawnWithVelocity;
    }

    private void OnDisable()
    {
        playerInputActions.PlayerMovement.Respawn.performed -= RespawnStill;
        playerInputActions.PlayerMovement.Respawn.started -= RespawnWithVelocity;
    }

    private void RespawnStill( InputAction.CallbackContext obj )
    {

        checkPointManager.RespawnStill();

    }

    private void RespawnWithVelocity( InputAction.CallbackContext obj )
    {

        checkPointManager.RespawnWithVelocity();

    }
}
