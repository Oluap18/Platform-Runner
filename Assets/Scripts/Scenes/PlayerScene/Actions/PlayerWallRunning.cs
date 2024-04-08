using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWallRunning : MonoBehaviour {

    [Header( "Wall Running" )]
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private float wallRunForce;
    [SerializeField] private float maxWallRunTime;
    [SerializeField] private float wallRunSpeed;
    [SerializeField] private float wallRunGravityScale;
    [SerializeField] private float wallUpInitialForce;
    [SerializeField] private float wallRunAngle;
    [SerializeField] private float wallRunStickForce;
    private bool wallRunning = false;
    private float wallRunTimer;

    [Header( "Detection" )]
    [SerializeField] private float wallCheckDistance;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    private bool wallLeft;
    private bool wallRight;

    [Header( "References" )]
    [SerializeField] private Transform player;
    [SerializeField] private PlayerBasicMovement playerBasicMovement;
    [SerializeField] private PlayerJumping playerJumping;
    [SerializeField] private PlayerGeneralFunctions playerGeneralFunctions;
    [SerializeField] private Rigidbody parentRigidBody;

    [Header( "Jumping" )]
    [SerializeField] private float wallJumpUpForce;
    [SerializeField] private float wallJumpBackForce;
    [SerializeField] private float wallJumpForwardForce;

    //Player Input
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        wallRunTimer = maxWallRunTime;
        playerInputActions = FindObjectOfType<PlayerInputManager>().GetPlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.PlayerMovement.Jump.performed += WallRunJump;
    }

    private void OnDisable()
    {
        playerInputActions.PlayerMovement.Jump.performed -= WallRunJump;
    }


    // Update is called once per frame
    private void Update()
    {
        if(wallRunning && wallRunTimer > 0) wallRunTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        CheckForWall();
        StateMachine();
        if(wallRunning) WallRunningMovement();
    }

    private void StateMachine()
    {
        if(( wallLeft || wallRight ) && playerGeneralFunctions.AboveGround()) {

            if(!wallRunning && wallRunTimer > 0) StartWallRun();

            
            if(wallRunning && wallRunTimer <= 0) StopWallRun();

        }
        else {
            if(wallRunning) StopWallRun();
        }

    }

    private void CheckForWall()
    {
        wallRight = Physics.Raycast( player.position, player.right, out rightWallhit, wallCheckDistance, whatIsWall );
        wallLeft = Physics.Raycast( player.position, -player.right, out leftWallhit, wallCheckDistance, whatIsWall );
    }

    private void StartWallRun()
    {
        wallRunning = true;
        playerBasicMovement.SetMoveSpeed( wallRunSpeed );
        playerJumping.ResetJumpsAllowed();
        wallRunTimer = maxWallRunTime;
        if(parentRigidBody.velocity.y <= 0)
        {
            parentRigidBody.AddForce( transform.up * wallUpInitialForce, ForceMode.Impulse );
        }
    }

    private void WallRunningMovement()
    {

        playerBasicMovement.SetGravityScale( wallRunGravityScale );

        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;
        Vector3 wallForward = Vector3.Cross( wallNormal, transform.up );

        //Verify which direction the force should be applied
        if(( player.forward - wallForward ).magnitude > ( player.forward - -wallForward ).magnitude) {
            wallForward = -wallForward;
        }

        //Move player foward
        parentRigidBody.AddForce( wallForward * wallRunForce );

        //Move player against the wall
        if(!TryingToLeaveWall( wallNormal )) {
            parentRigidBody.AddForce( -wallNormal * wallRunStickForce, ForceMode.Force );
        }
        else {
            parentRigidBody.AddForce( wallNormal * 100 );
        }


    }

    private bool TryingToLeaveWall( Vector3 wallNormal )
    {

        if(Vector3.Angle( wallNormal, playerBasicMovement.GetLastMovement() ) < wallRunAngle) { return true; }
        else { return false; }
    }

    private void WallRunJump( InputAction.CallbackContext obj )
    {
        WallRunJumpAction();
        
    }

    private void WallRunJumpAction()
    {
        if((wallLeft || wallRight) && wallRunning)
        {

            //Get the vector to apply to get off the wall
            Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

            //Get the vector to apply to go forward
            Vector3 wallForward = Vector3.Cross( wallNormal, transform.up );

            //Get the angle in between both vectors to apply the force forward
            Vector3 forwardMomentum = new Vector3( (wallNormal.x + wallForward.x) / 2,
                                                    0,
                                                    (wallNormal.z + wallForward.z) / 2 );

            Vector3 forceToApply = player.up * wallJumpUpForce + wallNormal * wallJumpBackForce + forwardMomentum * wallJumpForwardForce;

            //Reset the Y velocity to apply upwards force better
            if(parentRigidBody.velocity.y < 0)
            {
                parentRigidBody.velocity = new Vector3( parentRigidBody.velocity.x, 0, parentRigidBody.velocity.z );
            }
            parentRigidBody.AddForce( forceToApply, ForceMode.Impulse );

            playerJumping.DecreaseNBJumpsCurrent();

        }
    }

    private void StopWallRun()
    {
        wallRunning = false;
        playerBasicMovement.ResetGravityScale();
        playerBasicMovement.ResetMoveSpeed();

    }

    public bool GetWallLeft()
    {
        return wallLeft;
    }

    public bool GetWallRight()
    {
        return wallRight;
    }

    public void ResetWallRunTimer()
    {
        wallRunTimer = maxWallRunTime;
    }

    public bool GetWallRunning()
    {
        return wallRunning;
    }
}