using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerWallRunning : MonoBehaviour
{

    [Header( "Wall Running" )]
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private float wallRunForce;
    [SerializeField] private float maxWallRunTime;
    [SerializeField] private float wallRunSpeed;
    [SerializeField] private float wallRunGravityScale;


    [Header( "Input" )]
    private float horizontalInput;

    [Header( "Detection" )]
    [SerializeField] private float wallCheckDistance;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    private bool wallLeft;
    private bool wallRight;

    [Header( "References" )]
    [SerializeField] private Transform orientation;
    [SerializeField] private PlayerBasicMovement playerBasicMovement;
    [SerializeField] private PlayerJumping playerJumping;
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private PlayerGeneralFunctions playerGeneralFunctions;

    private Rigidbody rigidBody;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForWall();
        CanWallRun();
    }

    void FixedUpdate()
    {
        if(playerAnimator.GetCurrentState().Equals( PlayerAnimator.CurrentState.WallRunningLeft)
            || playerAnimator.GetCurrentState().Equals( PlayerAnimator.CurrentState.WallRunningRight )) {

            WallRunningMovement();

            
        }
    }

    private void CheckForWall() 
    {
        wallRight = Physics.Raycast( orientation.position, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall );
        wallLeft = Physics.Raycast( orientation.position, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall );
        if(wallLeft) {
            playerAnimator.SetWallLeft();
        }
        else{
            if(wallRight) {
                playerAnimator.SetWallRight();
            }
            else {
                playerAnimator.SetNoWall();
            }
        }

    }

    private void StartWallRun()
    {
        playerBasicMovement.SetMoveSpeed( wallRunSpeed );
        playerJumping.ResetJumpsAllowed();
    }

    private void WallRunningMovement()
    {

        playerBasicMovement.SetGravityScale( wallRunGravityScale );
        rigidBody.velocity = new Vector3( rigidBody.velocity.x, rigidBody.velocity.y, rigidBody.velocity.z );

        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;
        Vector3 wallForward = Vector3.Cross( wallNormal, transform.up );

        //Verify which direction the force should be applied
        if(( orientation.forward - wallForward ).magnitude > ( orientation.forward - -wallForward ).magnitude) {
            wallForward = -wallForward;
        }

        //Move player foward
        rigidBody.AddForce( wallForward * wallRunForce, ForceMode.Force );

        //Move player against the wall
        if(!(wallLeft && horizontalInput > 0) && !(wallRight && horizontalInput < 0))
            rigidBody.AddForce( -wallNormal * 100, ForceMode.Force );

    }

    public void StopWallRun()
    {
        
        playerBasicMovement.ResetMoveSpeed();
        playerBasicMovement.ResetGravityScale();

    }

    private void CanWallRun()
    {

        Vector3 input = playerBasicMovement.CalculateMovementOnCamera();
        horizontalInput = input.z;

        if((wallLeft || wallRight) && playerGeneralFunctions.AboveGround()) {
            
            StartWallRun();

        }
        else {

            StopWallRun();
        
        }

    }
}
