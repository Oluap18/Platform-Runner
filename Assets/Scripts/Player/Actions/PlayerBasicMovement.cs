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
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float gravityScale = 1.0f;
    [SerializeField] private float maxSpeed;

    private const float GLOBALGRAVITY = -9.81f;
    //To reset after wall running, gliding, etc
    private float originalMoveSpeed;
    private float originalGravityScale;
    private Vector3 lastMovement;
    //Player Input
    private PlayerInputActions playerInputActions;

    private void Awake() {

        playerInputActions = new PlayerInputActions();
        playerInputActions.PlayerMovement.Enable();
        originalGravityScale = gravityScale;
        originalMoveSpeed = moveSpeed;

    }

    private void FixedUpdate() 
    {

        Vector3 finalMovement = CalculateMovementOnCamera();

        lastMovement = finalMovement;

        float speedLimit = maxSpeed;

        if(parentRigidBody.velocity.magnitude <= speedLimit) {

            parentRigidBody.AddForce( finalMovement );
        
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
        Debug.Log( "Não Entrei" );
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

}
