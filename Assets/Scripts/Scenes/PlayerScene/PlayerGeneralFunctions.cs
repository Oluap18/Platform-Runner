using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerGeneralFunctions : NetworkBehaviour {

    [Header( "References" )]
    [SerializeField] private PlayerBasicMovement playerBasicMovement;
    [SerializeField] private PlayerJumping playerJumping;
    [SerializeField] private CameraMovementPlayerControlled cameraMovementPlayerControlled;
    [SerializeField] private TimerController timerController;

    [Header( "GeneralValues" )]
    [SerializeField] private float minJumpHeight;
    [SerializeField] private LayerMask whatIsGround;

    private static List<bool> statusOfCursor = new List<bool>();

    public bool AboveGround()
    {
        return !Physics.Raycast( transform.position, Vector3.down, minJumpHeight, whatIsGround );
    }

    public void DisableMovementOfPlayer()
    {
        if(!IsOwner) return;
        statusOfCursor.Add( false );
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerBasicMovement.DisablePlayerMovement();
        playerJumping.DisableJumpingAction();
        cameraMovementPlayerControlled.DisableCameraMovement();
        timerController.StopTimer();
    }

    public void EnableMovementOfPlayer()
    {
        if(!IsOwner) return;
        
        if(statusOfCursor.Count != 0)
        {
            statusOfCursor.RemoveAt( statusOfCursor.Count - 1 );
        }
        if(statusOfCursor.Count == 0 || statusOfCursor[statusOfCursor.Count - 1])
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playerBasicMovement.EnablePlayerMovement();
            playerJumping.EnableJumpingAction();
            cameraMovementPlayerControlled.EnableCameraMovement();
            timerController.StartTimer();
        }

    }

    public void RemoveCursor()
    {
        if(!IsOwner) return;
        statusOfCursor.RemoveAt( statusOfCursor.Count - 1 );
        if(statusOfCursor.Count == 0 || statusOfCursor[statusOfCursor.Count - 1])
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
