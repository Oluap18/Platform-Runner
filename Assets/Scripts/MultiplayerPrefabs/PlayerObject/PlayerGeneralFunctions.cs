using System.Collections.Generic;
using UnityEngine;

public class PlayerGeneralFunctions : MonoBehaviour {

    [Header( "References" )]
    [SerializeField] private PlayerBasicMovement playerBasicMovement;
    [SerializeField] private PlayerJumping playerJumping;
    [SerializeField] private CameraMovementPlayerControlled cameraMovementPlayerControlled;
    [SerializeField] private TimerController timerController;
    [SerializeField] private StartCountdownTimer countdownTimer;


    [Header( "GeneralValues" )]
    [SerializeField] private float minJumpHeight;
    [SerializeField] private LayerMask whatIsGround;

    private List<bool> statusOfCursor = new List<bool>();

    public bool AboveGround()
    {
        return !Physics.Raycast( transform.position, Vector3.down, minJumpHeight, whatIsGround );
    }

    public void EnablePlayerMovement()
    {
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

            statusOfCursor.Add( true );
        }
    }

    public void DisableMovementOfPlayer()
    {
        statusOfCursor.Add( false );
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerBasicMovement.DisablePlayerMovement();
        playerJumping.DisableJumpingAction();
        cameraMovementPlayerControlled.DisableCameraMovement();
        timerController.StopTimer();
    }

    public void RemoveCursor()
    {
        statusOfCursor.RemoveAt( statusOfCursor.Count - 1 );
        if(statusOfCursor.Count == 0 || statusOfCursor[statusOfCursor.Count - 1])
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
