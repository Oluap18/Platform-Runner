using UnityEngine;
using Unity.Netcode;

public class PlayerBasicMovement : NetworkBehaviour {

    [Header( "References" )]
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private PlayerInputManager playerInputManager;
    [SerializeField] private Rigidbody parentRigidBody;
    [SerializeField] private PlayerWallClimbing playerWallClimbing;
    [SerializeField] private PlayerGeneralFunctions playerGeneralFunctions;
    [SerializeField] private PlayerRPC playerRPC;

    [Header( "Player Movement" )]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float airSpeed;
    [SerializeField] private float accelarationPower;
    [SerializeField] private float accelarationThreshold;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject movementRotationObject;
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
    private bool playerMovementEnabled = false;

    private void Start()
    {
        if(!IsOwner) return;
        originalGravityScale = gravityScale;
        originalMoveSpeed = moveSpeed;

    }

    private void FixedUpdate()
    {
        if(!IsOwner) return;
        BasicMovement();

    }

    private void BasicMovement()
    {
        MaintainVelocityWhenLanding();

        Vector3 finalMovement;

        //To enable/disable movement
        if(playerMovementEnabled)
        {
            finalMovement = CalculateMovementOnCamera();
        }
        else
        {
            finalMovement = Vector3.zero;
        }
        lastMovement = finalMovement;
        Debug.Log( finalMovement );


        //When the player is falling, the movement sideways movement is limited
        if(playerAnimator.GetCurrentState() != PlayerAnimator.CurrentState.Falling)
        {

            MovementWhileNotFallingServerRpc( finalMovement );

        }
        else
        {

            if(playerAnimator.GetCurrentState() == PlayerAnimator.CurrentState.Running)
            {
                maxSpeed = originalMoveSpeed;
            }
            MovementWhileFalling( finalMovement );

        }

        Vector3 rotationVector = Vector3.Slerp( transform.forward, finalMovement, Time.deltaTime * rotateSpeed );
        parentRigidBody.MoveRotation( Quaternion.LookRotation( rotationVector ) );
        AddGravityForce( );
    }

    private void MovementWhileFalling( Vector3 finalMovement )
    {
        //Update speedlimit to take into account the speed diferences between air, wall, run, etc
        float speedLimit = maxSpeed;
        float currentVelocity = parentRigidBody.velocity.magnitude;

        //To be able to move a little bit in the air
        playerRPC.AddPlayerForceServerRpc( finalMovement * airSpeed );
        Debug.Log( "MovementWhileFalling" );
        //parentRigidBody.AddForce( finalMovement * airSpeed );

        //Air speed is always 10% slower then whatever the current max speed is
        if(currentVelocity >= ( speedLimit * 0.90 )) {

            //Slow down 1% of the last speed
            float slowdownFactor = 0.99f;
            parentRigidBody.velocity = new Vector3( parentRigidBody.velocity.x * slowdownFactor, parentRigidBody.velocity.y, parentRigidBody.velocity.z * slowdownFactor );

        }

        Vector3 newVelocity = parentRigidBody.velocity;
        //So that the player doesn't loose velocity on landing due to friction
        lastVelocity = newVelocity;
        lastVelocity.y = 0;
        goingToLand = true;
    }

    [ServerRpc]
    private void MovementWhileNotFallingServerRpc( Vector3 finalMovement )
    {
        Debug.Log( "MovementWhileNotFallingServerRpc " + finalMovement.ToString() );
        //Update speedlimit to take into account the speed diferences between air, wall, run, etc
        float speedLimit = maxSpeed;
        float currentVelocity = parentRigidBody.velocity.magnitude;

        if(currentVelocity <= speedLimit) {

            //To simulate the accelaration when starting from an almost idle position
            if(parentRigidBody.velocity.magnitude < accelarationThreshold && playerAnimator.AccelerationStatus())
            {
                parentRigidBody.AddForce( finalMovement * accelarationPower );
                //playerRPC.AddPlayerForceServerRpc( finalMovement * accelarationPower );
                Debug.Log( "MovementWhileNotFalling" );
                //parentRigidBody.AddForce( finalMovement * accelarationPower );
            }
            else
            {
                parentRigidBody.AddForce( finalMovement * accelarationPower );
                //playerRPC.AddPlayerForceServerRpc( finalMovement );
                Debug.Log( "MovementWhileNotFalling" );
                //parentRigidBody.AddForce( finalMovement );
            }
        }
    }

    public Vector3 CalculateMovementOnCamera()
    {

        if(!playerWallClimbing.GetExitingWall()) {
            //MovementInput

            Vector3 moveDir = GetMovementVectorNormalized();

            //Camera Direction
            Vector3 cameraForward = movementRotationObject.transform.forward;
            Vector3 cameraRight = movementRotationObject.transform.right;

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

    public void AddGravityForce( )
    {

        Vector3 gravity = GLOBALGRAVITY * gravityScale * Vector3.up;
        playerRPC.AddPlayerForceServerRpc( gravity );
        Debug.Log( "Gravity" );
        //parentRigidBody.AddForce( gravity, ForceMode.Acceleration );

    }

    public Vector3 GetMovementVectorNormalized()
    {

        Vector3 inputVector = playerInputManager.GetPlayerInputActions().PlayerMovement.BasicMovement.ReadValue<Vector3>();
        return inputVector.normalized;

    }

    public void SetGravityScale( float gravity )
    {
        gravityScale = gravity;
    }

    public void ResetGravityScale()
    {
        gravityScale = originalGravityScale;
    }

    public void SetMoveSpeed( float speed )
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

    private void MaintainVelocityWhenLanding()
    {
        //So that the player doesn't loose velocity when landing
        if(goingToLand == true && playerAnimator.IsGoingToLand()) {
            goingToLand = false;
            parentRigidBody.velocity = lastVelocity;
        }
    }

}
