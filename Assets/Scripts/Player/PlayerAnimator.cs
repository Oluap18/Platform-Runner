using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerBasicMovement;

public class PlayerAnimator : MonoBehaviour
{

    [Header( "References" )]
    [SerializeField] private PlayerGeneralFunctions playerGeneralFunctions;

    //Player State
    public enum CurrentState {
        Idle,
        Running,
        Jumping,
        WallRunningLeft,
        WallRunningRight,
        Falling
    }
    private CurrentState currentState;

    private Animator animator;
    private const string IS_RUNNING = "IsRunning";
    private const string IS_IDLE = "IsIdle";
    private const string IS_JUMPING = "IsJumping";
    private const string IS_WALLRUNNINGLEFT = "IsWallRunningLeft";
    private const string IS_WALLRUNNINGRIGHT = "IsWallRunningRight";
    private const string IS_FALLING = "IsFalling";

    // Animation Variable
    private Vector3 lastMovement;
    private bool goingToJump;
    private bool wallLeft;
    private bool wallRight;

    // Start is called before the first frame update
    private void Awake()
    {
        goingToJump = false;

        animator = GetComponentInParent<Animator>();
        animator.SetBool( IS_IDLE, true );
    }

    private void Start()
    {
        currentState = CurrentState.Idle;
    }

    public CurrentState GetCurrentState()
    {
        return currentState;
    }

    public void SetCurrentState( CurrentState current )
    {
        currentState = current;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState) {

            case CurrentState.Idle:
                ResetAnimator();
                animator.SetBool( IS_IDLE, true );
                break;
            case CurrentState.Running:
                ResetAnimator();
                animator.SetBool( IS_RUNNING, true );
                break;
            case CurrentState.Jumping:
                ResetAnimator();
                animator.SetBool( IS_JUMPING, true );
                break;
            case CurrentState.WallRunningLeft:
                ResetAnimator();
                animator.SetBool( IS_WALLRUNNINGLEFT, true );
                break;
            case CurrentState.WallRunningRight:
                ResetAnimator();
                animator.SetBool( IS_WALLRUNNINGRIGHT, true );
                break;
            case CurrentState.Falling:
                ResetAnimator();
                animator.SetBool( IS_FALLING, true );
                break;
        }

        UpdateCurrentState();

    }

    private void UpdateCurrentState()
    {
        if(goingToJump) {
            currentState = CurrentState.Jumping;
            goingToJump = false;
        }
        else {

            if(playerGeneralFunctions.AboveGround()) {

                if(wallLeft) {
                    currentState = CurrentState.WallRunningLeft;
                }
                else if(wallRight) {
                    currentState = CurrentState.WallRunningRight;
                }
                else {
                    currentState = CurrentState.Falling;
                }
          
            }
            else {

                if(lastMovement != Vector3.zero) {
                    currentState = CurrentState.Running;
                }
                if(lastMovement.Equals( Vector3.zero )) {
                    currentState = CurrentState.Idle;
                }

            }

        }
        
    }

    private void ResetAnimator()
    {
        animator.SetBool( IS_IDLE, false );
        animator.SetBool( IS_RUNNING, false );
        animator.SetBool( IS_JUMPING, false );
        animator.SetBool( IS_WALLRUNNINGLEFT, false );
        animator.SetBool( IS_WALLRUNNINGRIGHT, false );
        animator.SetBool( IS_FALLING, false );
    }

    public void SetLastMovement( Vector3 movement )
    {
        lastMovement = movement;
    }

    public void SetGoingToJump()
    {
        goingToJump = true;
    }

    public bool GetGoingToJump()
    {
        return goingToJump;
    }

    public void SetWallLeft()
    {
        wallLeft = true;
        wallRight = false;
    }

    public void SetWallRight()
    {
        wallRight = true;
        wallLeft = false;
    }

    public void SetNoWall()
    {
        wallLeft = false;
        wallRight = false;
    }
}
