using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class PlayerWallRunning : MonoBehaviour
{

    [Header( "Wall Running" )]
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private float wallRunForce;
    [SerializeField] private float maxWallRunTime;
    [SerializeField] private float wallRunSpeed;
    [SerializeField] private float wallRunGravityScale;
    private bool wallRunning = false;
    private float wallRunTimer; 

    [Header( "Input" )]
    private float horizontalInput;

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
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private PlayerGeneralFunctions playerGeneralFunctions;
    [SerializeField] private Rigidbody parentRigidBody;

    private void Start()
    {
        wallRunTimer = maxWallRunTime;
    }

    // Update is called once per frame
    private void Update()
    { 
        StateMachine();
    }

    private void FixedUpdate()
    {
        CheckForWall();
        if(wallRunning) WallRunningMovement();
    }

    private void StateMachine()
    {
        if(( wallLeft || wallRight ) && playerGeneralFunctions.AboveGround()) {

            if(!wallRunning && wallRunTimer > 0) StartWallRun();

            if(wallRunning && wallRunTimer > 0) wallRunTimer -= Time.deltaTime;
            else StopWallRun();

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
        if(!(wallLeft && horizontalInput > 0) && !(wallRight && horizontalInput < 0 )) {
            parentRigidBody.AddForce( -wallNormal * 100 );
        }
            

    }

    private void StopWallRun()
    {
        wallRunning = false;
        //playerBasicMovement.ResetMoveSpeed();
        playerBasicMovement.ResetGravityScale();

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