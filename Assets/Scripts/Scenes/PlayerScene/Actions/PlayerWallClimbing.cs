using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWallClimbing : MonoBehaviour
{

    [Header( "References" )]
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody parentRigidbody;
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private PlayerBasicMovement playerBasicMovement;

    [Header( "Climbing" )]
    [SerializeField] private float climbSpeed;
    [SerializeField] private float maxClimbTime;

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
    private string lastWall;
    private bool exitingWall;
    private float exitWallTimer;

    //Player Input
    private PlayerInputActions playerInputActions;

    private void Start()
    {
        climbTimer = maxClimbTime;
        playerInputActions = new PlayerInputActions();
        playerInputActions.PlayerMovement.Enable();
        playerInputActions.PlayerMovement.Jump.performed += ClimbJump;
    }

    private void Update()
    {
        lastMovement = playerBasicMovement.GetLastMovement();
        
        StateMachine(); 

    }

    private void FixedUpdate()
    {
        wallFront = Physics.SphereCast( player.position, sphereCastRadius, player.forward, out frontWallHit, detectionLength, whatIsWall );
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
            if(exitWallTimer < 0) exitingWall = false;
        }

        else {
            if(climbing) StopClimbing();
        }

    }

    private void WallCheck()
    {
        bool newWall = frontWallHit.collider.gameObject.name != lastWall;

        wallLookAngle = Vector3.Angle( player.forward, -frontWallHit.normal );

        if(wallFront && newWall) {
            climbTimer = maxClimbTime;
            climbJumpsLeft = climbJumps;
        }
    }

    private void StartClimbing()
    {
        climbing = true;
        lastWall = frontWallHit.collider.gameObject.name;
    }

    private void ClimbingMovement()
    {
        parentRigidbody.velocity = new Vector3( 0, climbSpeed, 0 );
    }

    private void StopClimbing()
    {
        climbing = false;
        playerBasicMovement.ResetMoveSpeed();

        if(playerAnimator.GetCurrentState().Equals( PlayerAnimator.CurrentState.WallClimbing )) {
            parentRigidbody.velocity = new Vector3( parentRigidbody.velocity.x, 0, parentRigidbody.velocity.z );
        }
    }

    public void ResetClimbTimer()
    {
        climbTimer = maxClimbTime;
        climbJumpsLeft = climbJumps;
    }

    public bool GetWallClimbing()
    {
        return climbing;
    }

    private void ClimbJump( InputAction.CallbackContext obj )
    {
        if(wallFront && climbJumpsLeft > 0 && playerAnimator.GetCurrentState() == PlayerAnimator.CurrentState.WallClimbing) {
            exitingWall = true;
            exitWallTimer = exitWallTime;

            Vector3 forceToApply = player.up * climbJumpUpForce + frontWallHit.normal * climbJumpBackForce;

            parentRigidbody.velocity = new Vector3( parentRigidbody.velocity.x, 0f, parentRigidbody.velocity.z );
            parentRigidbody.AddForce( forceToApply, ForceMode.Impulse );

            climbJumpsLeft--;

        }
    }

    public bool GetExitingWall()
    {
        return exitingWall;
    }
}
