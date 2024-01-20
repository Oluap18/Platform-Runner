using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

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

    public CurrentState StringToCurrentState( string str )
    {
        switch(str)
        {
            case IS_RUNNING:
                return CurrentState.Running;
            case IS_IDLE:
                return CurrentState.Idle;
            case IS_JUMPING:
                return CurrentState.Jumping;
            case IS_WALLRUNNINGLEFT:
                return CurrentState.WallRunningLeft;
            case IS_WALLRUNNINGRIGHT:
                return CurrentState.WallRunningRight;
            case IS_FALLING:
                return CurrentState.Falling;
            case IS_WALLCLIMBING:
                return CurrentState.WallClimbing;
            default:
                return CurrentState.Idle;
        }
    }

    public string CurrentStateToString( CurrentState animator )
    {
        switch(animator)
        {
            case CurrentState.Running:
                return IS_RUNNING;
            case CurrentState.Idle:
                return IS_IDLE;
            case CurrentState.Jumping:
                return IS_JUMPING;
            case CurrentState.WallRunningLeft:
                return IS_WALLRUNNINGLEFT;
            case CurrentState.WallRunningRight:
                return IS_WALLRUNNINGRIGHT;
            case CurrentState.Falling:
                return IS_FALLING;
            case CurrentState.WallClimbing:
                return IS_WALLCLIMBING;
            default:
                return IS_IDLE;
        }
    }

    public void SetCurrentState( CurrentState current )
    {
        currentState = current;
    }

    // Update is called once per frame
    void FixedUpdate()
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

        if(accelarationList.Contains( currentState )) {
            return true;
        }
        else {
            return false;
        }
    }
}
