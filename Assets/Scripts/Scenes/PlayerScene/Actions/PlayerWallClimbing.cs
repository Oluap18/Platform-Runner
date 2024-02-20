using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWallClimbing : MonoBehaviour {

    [Header( "References" )]
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody parentRigidbody;
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private PlayerBasicMovement playerBasicMovement;
    [SerializeField] private PlayerJumping playerJumping;

    [Header( "Climbing" )]
    [SerializeField] private float climbSpeed;
    [SerializeField] private float maxClimbTime;
    [SerializeField] private float ledgeBoost;

    [Header( "Detection" )]
    [SerializeField] private float detectionLength;
    [SerializeField] private float sphereCastRadius;
    [SerializeField] private float maxWallLookAngle;

    [Header( "Jumping" )]
    [SerializeField] private float climbJumpUpForce;
    [SerializeField] private float climbJumpBackForce;
    [SerializeField] private float minWallNormalAngleChange;
    [SerializeField] private float exitWallTime;
    [SerializeField] private int climbJumps;


    //WallClimbing
    private float climbTimer;
    private bool climbing;
    private float wallLookAngle;
    private RaycastHit frontWallHit;
    private bool wallFront;
    private Vector3 lastMovement;
    private int climbJumpsLeft;

    //WallClimbingJump
    private int lastWall;
    private bool exitingWall;
    private float exitWallTimer;

    //Player Input
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        climbTimer = maxClimbTime;
        playerInputActions = FindObjectOfType<PlayerInputManager>().GetPlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.PlayerMovement.Jump.performed += ClimbJump;
    }

    private void OnDisable()
    {
        playerInputActions.PlayerMovement.Jump.performed -= ClimbJump;
    }

    private void Update()
    {
        if(climbTimer > 0 && climbing) climbTimer -= Time.deltaTime;
        

    }

    private void FixedUpdate()
    {

        wallFront = Physics.SphereCast( player.position, sphereCastRadius, player.forward, out frontWallHit, detectionLength, whatIsWall );
        
        lastMovement = playerBasicMovement.GetLastMovement();

        StateMachine();

        if(wallFront) {
            WallCheck();
            
            if(climbing && !exitingWall) ClimbingMovement();
        }

    }

    private void StateMachine()
    {

        if(wallFront && lastMovement != Vector3.zero && wallLookAngle < maxWallLookAngle && !exitingWall) {

            if(!climbing && climbTimer > 0) StartClimbing();

            if(climbTimer > 0) climbTimer -= Time.deltaTime;
            else StopClimbing();
        }

        else if(exitingWall) {
            if(climbing) StopClimbing();
            if(exitWallTimer > 0) exitWallTimer -= Time.deltaTime;
            if(exitWallTimer <= 0) exitingWall = false;
        }

        else {
            if(climbing) StopClimbing();
        }

    }

    private void WallCheck()
    {
        bool newWall = frontWallHit.collider.GetInstanceID() != lastWall;

        wallLookAngle = Vector3.Angle( player.forward, -frontWallHit.normal );
        if(newWall) {
            lastWall = frontWallHit.collider.GetInstanceID();
            ResetClimbTimer();
        }
    }

    private void StartClimbing()
    {
        climbing = true;
    }

    private void ClimbingMovement()
    {
        parentRigidbody.velocity = new Vector3( 0, climbSpeed, 0 );
    }

    private void StopClimbing()
    {
        climbing = false;
        playerBasicMovement.ResetMoveSpeed();
    }

    public void ResetClimbTimer()
    {
        
        climbTimer = maxClimbTime;
        climbJumpsLeft = climbJumps;
        playerJumping.ResetJumpsAllowed();
        playerBasicMovement.ResetMoveSpeed();
    }

    public bool GetWallClimbing()
    {
        return climbing;
    }

    private void ClimbJump( InputAction.CallbackContext obj )
    {
        ClimbJumpAction();
        
    }

    private void ClimbJumpAction()
    {
        if(wallFront && climbJumpsLeft > 0 && playerAnimator.GetCurrentState() == PlayerAnimator.CurrentState.WallClimbing)
        {
            exitingWall = true;
            exitWallTimer = exitWallTime;

            Vector3 forceToApply = player.up * climbJumpUpForce + frontWallHit.normal * climbJumpBackForce;

            parentRigidbody.velocity = new Vector3( parentRigidbody.velocity.x, 0f, parentRigidbody.velocity.z );
            parentRigidbody.AddForce( forceToApply, ForceMode.Impulse );

            climbJumpsLeft--;

            playerJumping.DecreaseNBJumpsCurrent();

        }
    }

    public bool GetExitingWall()
    {
        return exitingWall;
    }

}
