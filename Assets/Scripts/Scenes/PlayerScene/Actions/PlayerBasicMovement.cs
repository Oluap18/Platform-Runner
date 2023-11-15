using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBasicMovement : MonoBehaviour {

    [Header( "References" )]
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private Rigidbody parentRigidBody;
    [SerializeField] private PlayerWallClimbing playerWallClimbing;

    [Header( "Player Movement" )]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float airSpeed;
    [SerializeField] private float accelarationPower;
    [SerializeField] private float accelarationThreshold;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float gravityScale = 1.0f;
    [SerializeField] private float maxSpeed;

    private const float GLOBALGRAVITY = -9.81f;
    //To reset after wall running, gliding, etc
    private float originalMoveSpeed;
    private float originalGravityScale;
    private Vector3 lastMovement;
    private Vector3 lastVelocity;
    private bool goingToLand = false;
    //Player Input
    private PlayerInputActions playerInputActions;
    private bool playerMovementEnabled = false;


    private void Awake() {

        playerInputActions = new PlayerInputActions();
        playerInputActions.PlayerMovement.Enable();
        originalGravityScale = gravityScale;
        originalMoveSpeed = moveSpeed;

    }

    private void FixedUpdate() 
    {
        //So that the player doesn't loose velocity when landing
        if( goingToLand == true && (playerAnimator.GetCurrentState() == PlayerAnimator.CurrentState.Running || playerAnimator.GetCurrentState() == PlayerAnimator.CurrentState.Idle) ) { 
            goingToLand = false;
            parentRigidBody.velocity = lastVelocity;
        }

        Vector3 finalMovement;
        
        if(playerMovementEnabled) {
            finalMovement = CalculateMovementOnCamera();
        }
        else {
            finalMovement = Vector3.zero;
        }

        lastMovement = finalMovement;

        float speedLimit = maxSpeed;

        if(parentRigidBody.velocity.magnitude <= speedLimit && playerAnimator.GetCurrentState() != PlayerAnimator.CurrentState.Falling) {

            if(parentRigidBody.velocity.magnitude < accelarationThreshold) {
                parentRigidBody.AddForce( finalMovement * accelarationPower );
            }
            else
            {
                parentRigidBody.AddForce( finalMovement );
            }
            

        }
        else {
            
            if(playerAnimator.GetCurrentState() == PlayerAnimator.CurrentState.Falling) {
                //To be able to move a little bit in the air
                parentRigidBody.AddForce( finalMovement * airSpeed );
                float airStrengthCorrect = 1;
                if(parentRigidBody.velocity.magnitude != 0) {
                    airStrengthCorrect = parentRigidBody.velocity.magnitude / speedLimit;
                }
                Vector3 newVelocity = new Vector3( parentRigidBody.velocity.x / airStrengthCorrect, parentRigidBody.velocity.y, parentRigidBody.velocity.z / airStrengthCorrect );
                parentRigidBody.velocity = newVelocity;                
            

                //So that the player doesn't loose velocity on landing due to friction
                lastVelocity = parentRigidBody.velocity;
                lastVelocity.y = 0;
                goingToLand = true;
            }
        }

        Vector3 rotationVector = Vector3.Slerp( transform.forward, finalMovement, Time.deltaTime * rotateSpeed );
        parentRigidBody.MoveRotation( Quaternion.LookRotation( rotationVector ) );


        AddGravityForce( parentRigidBody );
    
    }

    public Vector3 CalculateMovementOnCamera() 
    {

        if(!playerWallClimbing.GetExitingWall()) {
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
        return Vector3.zero;
    }

    private void AddGravityForce( Rigidbody rigidBody ) 
    {

        Vector3 gravity = GLOBALGRAVITY * gravityScale * Vector3.up;
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

    public Vector3 GetLastMovement()
    {
        return lastMovement;
    }

    public void EnablePlayerMovement()
    {
        playerMovementEnabled = true;
    }

    public void DisablePlayerMovement()
    {
        playerMovementEnabled = false;
    }

}
