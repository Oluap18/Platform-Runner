using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerCheckPoint : MonoBehaviour
{

    private PlayerInputActions playerInputActions;
    private CheckPointManager checkPointManager;

    // Start is called before the first frame update
    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.PlayerMovement.Enable();
        playerInputActions.PlayerMovement.Respawn.performed += RespawnStill;
        playerInputActions.PlayerMovement.Respawn.started += RespawnWithVelocity;

        checkPointManager = FindObjectOfType<CheckPointManager>();
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
