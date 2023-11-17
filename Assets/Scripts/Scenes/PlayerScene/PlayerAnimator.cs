using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerBasicMovement;

public class PlayerAnimator : MonoBehaviour
{

    [Header( "References" )]
    [SerializeField] private PlayerGeneralFunctions playerGeneralFunctions;
    [SerializeField] private PlayerWallRunning playerWallRunning;
    [SerializeField] private PlayerWallClimbing playerWallClimbing;
    [SerializeField] private PlayerBasicMovement playerBasicMovement;

    //Player State
    public enum CurrentState {
        Idle,
        Running,
        Jumping,
        WallRunningLeft,
        WallRunningRight,
        Falling,
        WallClimbing
    }
    private CurrentState currentState;

    private Animator animator;
    private const string IS_RUNNING = "IsRunning";
    private const string IS_IDLE = "IsIdle";
    private const string IS_JUMPING = "IsJumping";
    private const string IS_WALLRUNNINGLEFT = "IsWallRunningLeft";
    private const string IS_WALLRUNNINGRIGHT = "IsWallRunningRight";
    private const string IS_FALLING = "IsFalling";
    private const string IS_WALLCLIMBING = "IsWallClimbing";

    // Animation Variable
    private Vector3 lastMovement;
    private bool goingToJump;

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
        lastMovement = playerBasicMovement.GetLastMovement();

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
            case CurrentState.WallClimbing:
                ResetAnimator();
                animator.SetBool( IS_WALLCLIMBING, true );
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

            if(playerWallClimbing.GetWallClimbing()) {
                currentState = CurrentState.WallClimbing;
            }
            else {

                if(playerGeneralFunctions.AboveGround()) {

                    if(playerWallRunning.GetWallRunning()) {
                        
                        if(playerWallRunning.GetWallLeft()) {
                            currentState = CurrentState.WallRunningLeft;
                        }
                        else if(playerWallRunning.GetWallRight()) {
                            currentState = CurrentState.WallRunningRight;
                        }
                    
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
        
    }

    private void ResetAnimator()
    {
        animator.SetBool( IS_IDLE, false );
        animator.SetBool( IS_RUNNING, false );
        animator.SetBool( IS_JUMPING, false );
        animator.SetBool( IS_WALLRUNNINGLEFT, false );
        animator.SetBool( IS_WALLRUNNINGRIGHT, false );
        animator.SetBool( IS_FALLING, false );
        animator.SetBool( IS_WALLCLIMBING, false );
    }

    public void SetGoingToJump()
    {
        goingToJump = true;
    }

    public bool GetGoingToJump()
    {
        return goingToJump;
    }

    public bool IsGoingToLand()
    {
        if(currentState == CurrentState.Running || currentState == CurrentState.Idle) {
            return true;
        }
        else {
            return false;
        }
    }

    public bool AccelerationStatus()
    {
        List<CurrentState> accelarationList = new List<CurrentState>();
        accelarationList.Add( CurrentState.Idle );
        accelarationList.Add( CurrentState.Running );
        
        if(accelarationList.Contains(currentState)) {
            return true;
        }
        else {
            return false;
        }
    }
}
