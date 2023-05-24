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
    private float wallRunTimer;
    [SerializeField] private float rotateSpeed;


    [Header( "Input" )]
    private float horizontalInput;
    private float verticalInput;

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
    }

    private void WallRunningMovement()
    {

        playerBasicMovement.SetGravityScale( 0 );
        rigidBody.velocity = new Vector3( rigidBody.velocity.x, 0f, rigidBody.velocity.z );

        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;
        Vector3 wallForward = Vector3.Cross( wallNormal, transform.up );
        rigidBody.AddForce( wallForward * wallRunForce, ForceMode.Force );

        //Try and rotate the body forward
        Vector3 rotationVector = Vector3.Slerp( transform.forward, wallForward, Time.deltaTime * rotateSpeed );
        rigidBody.MoveRotation( Quaternion.LookRotation( rotationVector ) );
    }

    public void StopWallRun()
    {
        
        playerBasicMovement.ResetMoveSpeed();
        playerBasicMovement.ResetGravityScale();
        playerBasicMovement.SetCurrentState(PlayerBasicMovement.CurrentState.Idle);

    }

    private void CanWallRun()
    {

        if((wallLeft || wallRight) && AboveGround()) {

            playerBasicMovement.SetCurrentState( PlayerBasicMovement.CurrentState.WallRunning );
            StartWallRun();

        }
        
        if(playerBasicMovement.GetCurrentState().Equals( PlayerBasicMovement.CurrentState.WallRunning ) && !( ( wallLeft || wallRight ) && AboveGround() )) {
            StopWallRun();
        }

    }
}
