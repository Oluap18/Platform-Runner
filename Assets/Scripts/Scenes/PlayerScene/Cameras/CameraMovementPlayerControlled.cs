using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
using Cinemachine;

public class CameraMovementPlayerControlled : NetworkBehaviour {

    [Header( "Camera" )]
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [Header( "References" )]
    [SerializeField] private Transform playerObjectRotateAround;
    [SerializeField] private Transform playerBasicMovementObject;
    [SerializeField] private PlayerInputManager playerInputManager;
    [SerializeField] private CinemachineFreeLook freeLookCamera;


    //Saves the position of the follow object in reference to the player
    private Vector3 directionToTarget;
    private bool isCameraEnabled;


    private void Start()
    {
        if(!IsOwner) return;
        isCameraEnabled = false;
        directionToTarget = transform.position - playerObjectRotateAround.position;
        freeLookCamera.m_Priority++;
    }

    private void FixedUpdate()
    {
        if(!IsOwner) return;
        if(isCameraEnabled)
        {
            //Make sure that the position of the follow object in reference to the player
            //maintains the same despite the player movement
            transform.position = playerObjectRotateAround.position + directionToTarget;
            playerBasicMovementObject.position = this.transform.position;

            float mouseX = Mouse.current.delta.x.ReadValue() * Time.deltaTime * sensX * playerInputManager.GetCameraSensitivity();
            float mouseY = Mouse.current.delta.y.ReadValue() * Time.deltaTime * sensY * playerInputManager.GetInvertedCamera() * playerInputManager.GetCameraSensitivity();

            RotateHorizontally( mouseX );
            RotateVertically( mouseY );

            //Record the position of the follow object in reference to the player
            directionToTarget = transform.position - playerObjectRotateAround.position;
        
        }

    }

    private void RotateHorizontally( float mouseX )
    {

        this.transform.RotateAround( playerObjectRotateAround.position, Vector3.up, mouseX * Time.deltaTime );
        playerBasicMovementObject.RotateAround( playerBasicMovementObject.position, Vector3.up, mouseX * Time.deltaTime );

    }

    private void RotateVertically( float mouseY )
    {

        this.transform.Rotate( Vector3.right, mouseY * Time.deltaTime );

    }

    public void EnableCameraMovement()
    {
        isCameraEnabled = true;
    }

    public void DisableCameraMovement()
    {
        isCameraEnabled = false;
    }

}
