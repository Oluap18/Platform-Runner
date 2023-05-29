using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerWallRunning : MonoBehaviour
{

    [Header( "Wall Running" )]
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public float wallRunForce;
    public float maxWallRunTime;
    public float wallRunSpeed;
    public float wallRunGravityScale;


    [Header( "Input" )]
    private float horizontalInput;

    [Header( "Detection" )]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    private bool wallLeft;
    private bool wallRight;

    [Header( "References" )]
    public Transform orientation;
    //private PlayerMovementAdvanced pm;
    private Rigidbody rigidBody;
    [SerializeField] private PlayerBasicMovement playerBasicMovement;
    [SerializeField] private PlayerJumping playerJumping;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponentInParent<Rigidbody>();
        //pm = GetComponent<PlayerMovementAdvanced>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForWall();
        CanWallRun();
    }

    void FixedUpdate()
    {
        if(playerBasicMovement.GetCurrentState().Equals(PlayerBasicMovement.CurrentState.WallRunning)) {

            WallRunningMovement();

            
        }
    }

    private void CheckForWall() 
    {
        wallRight = Physics.Raycast( orientation.position, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall );
        wallLeft = Physics.Raycast( orientation.position, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall );

    }

    private bool AboveGround() 
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround );
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
        playerBasicMovement.SetCurrentState(PlayerBasicMovement.CurrentState.Idle);

    }

    private void CanWallRun()
    {

        Vector3 input = playerBasicMovement.CalculateMovementOnCamera();
        horizontalInput = input.z;

        if((wallLeft || wallRight) && AboveGround()) {
            playerBasicMovement.SetCurrentState( PlayerBasicMovement.CurrentState.WallRunning );
            StartWallRun();

        }
        
        if(playerBasicMovement.GetCurrentState().Equals( PlayerBasicMovement.CurrentState.WallRunning ) && !( ( wallLeft || wallRight ) && AboveGround() )) {
            StopWallRun();
        }

    }
}
