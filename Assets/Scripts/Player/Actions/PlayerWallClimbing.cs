using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    private float climbTimer;
    private bool climbing;
    private float wallLookAngle;
    private RaycastHit frontWallHit;
    private bool wallFront;
    private Vector3 lastMovement;

    private void Start()
    {
        climbTimer = maxClimbTime;
    }

    private void Update()
    {
        lastMovement = playerBasicMovement.GetLastMovement();
        
        StateMachine(); 

    }

    private void FixedUpdate()
    {
        WallCheck();
        if(climbing) ClimbingMovement();
    }

    private void StateMachine()
    {

        if(wallFront && lastMovement != Vector3.zero && wallLookAngle < maxWallLookAngle) {

            if(!climbing && climbTimer > 0) StartClimbing();

            if(climbTimer > 0) climbTimer -= Time.deltaTime;
            else StopClimbing();
        }

        else {
            if(climbing) StopClimbing();
        }
        Debug.Log( "CLimbing? " + climbing + ". WallFront: " + wallFront + ". Last Movement: " + lastMovement + ". wallLookAngle: " + wallLookAngle + ". maxWallLookAngle: " + maxWallLookAngle );

    }

    private void WallCheck()
    {
        wallFront = Physics.SphereCast( player.position, sphereCastRadius, player.forward, out frontWallHit, detectionLength, whatIsWall );
        wallLookAngle = Vector3.Angle( player.forward, -frontWallHit.normal );
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

        if(playerAnimator.GetCurrentState().Equals( PlayerAnimator.CurrentState.WallClimbing )) {
            parentRigidbody.velocity = new Vector3( parentRigidbody.velocity.x, 0, parentRigidbody.velocity.z );
        }
    }

    public void ResetClimbTimer()
    {
        climbTimer = maxClimbTime;
    }

    public bool GetWallClimbing()
    {
        return climbing;
    }
}
