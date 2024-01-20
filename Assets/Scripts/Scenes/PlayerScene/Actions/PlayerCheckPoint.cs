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
        playerInputActions = FindObjectOfType<PlayerInputManager>().GetPlayerInputActions();
        playerInputActions.PlayerMovement.Respawn.performed += RespawnStill;
        playerInputActions.PlayerMovement.Respawn.started += RespawnWithVelocity;

        checkPointManager = FindObjectOfType<CheckPointManager>();
    }
    private void FixedUpdate()
    {
        if(RecordPlayerRun.replay)
        {
            if(RecordPlayerRun.checkpointStillAction.Count > 0 && RecordPlayerRun.checkpointStillAction[0] == RecordPlayerRun.iterator)
            {
                checkPointManager.RespawnStill();
                RecordPlayerRun.checkpointStillAction.RemoveAt( 0 );
            }
            if(RecordPlayerRun.checkpointRunningAction.Count > 0 && RecordPlayerRun.checkpointRunningAction[0] == RecordPlayerRun.iterator)
            {
                checkPointManager.RespawnWithVelocity();
                RecordPlayerRun.checkpointRunningAction.RemoveAt( 0 );
            }
        }
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
