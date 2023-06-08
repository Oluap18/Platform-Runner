using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBasicMovement : MonoBehaviour {

    //Player Input
    private PlayerInputActions playerInputActions;

    [Header( "References" )]
    [SerializeField] private PlayerAnimator playerAnimator;

    [Header( "Player Movement" )]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float minJumpHeight;

    private float originalMoveSpeed;

    //Animation variables
    private bool isRunning;
    
    //Gravity Scale editable on the inspector
    //providing a gravity scale per object
    public float gravityScale = 1.0f;
    public static float globalGravity = -9.81f;
    private float originalGravityScale;

    //Player movement
    private Rigidbody rigidBody;

    private void Awake() {

        playerInputActions = new PlayerInputActions();
        playerInputActions.PlayerMovement.Enable();
        originalGravityScale = gravityScale;
        originalMoveSpeed = moveSpeed;

    }

    private void Start() 
    {

        rigidBody = GetComponentInParent<Rigidbody>();

    }

    private void FixedUpdate() 
    {

        Vector3 finalMovement = CalculateMovementOnCamera();

        playerAnimator.SetLastMovement( finalMovement );

        rigidBody.MovePosition( transform.position + finalMovement );

        Vector3 rotationVector = Vector3.Slerp( transform.forward, finalMovement, Time.deltaTime * rotateSpeed );
        rigidBody.MoveRotation( Quaternion.LookRotation( rotationVector ) );

        AddGravityForce( rigidBody );
    }

    public Vector3 CalculateMovementOnCamera() 
    {
        
        //MovementInput
        Vector3 moveDir = GetMovementVectorNormalized();

        //Camera Direction
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        //Movement according to the camera
        Vector3 forwardMovement = moveDir.z * cameraForward;
        Vector3 rightMovement = moveDir.x * cameraRight;
        Vector3 finalMovement = ( forwardMovement + rightMovement ) * moveSpeed;

        return finalMovement;
    }

    private void AddGravityForce( Rigidbody rigidBody ) 
    {

        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rigidBody.AddForce( gravity, ForceMode.Acceleration );

    }

    public Vector3 GetMovementVectorNormalized() 
    {

        Vector3 inputVector = playerInputActions.PlayerMovement.BasicMovement.ReadValue<Vector3>();

        return inputVector.normalized;

    }

    public void SetGravityScale(float gravity)
    {
        gravityScale = gravity;
    }

    public void ResetGravityScale()
    {
        gravityScale = originalGravityScale;
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void ResetMoveSpeed()
    {
        moveSpeed = originalMoveSpeed;
    }

    

}
