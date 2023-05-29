using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    [SerializeField] private PlayerBasicMovement player;

    private Animator animator;
    private const string IS_RUNNING = "IsRunning";
    private const string IS_IDLE = "IsIdle";
    private const string IS_JUMPING = "IsJumping";
    private const string IS_WALLRUNNING = "IsWallRunning";

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool( IS_IDLE, true );
    }

    // Update is called once per frame
    void Update()
    {
        switch(player.GetCurrentState()) {

            case PlayerBasicMovement.CurrentState.Idle:
                ResetAnimator();
                animator.SetBool( IS_IDLE, true );
                break;
            case PlayerBasicMovement.CurrentState.Running:
                ResetAnimator();
                animator.SetBool( IS_RUNNING, true );
                break;
            case PlayerBasicMovement.CurrentState.Jumping:
                ResetAnimator();
                animator.SetBool( IS_JUMPING, true );
                break;
            case PlayerBasicMovement.CurrentState.WallRunning:
                ResetAnimator();
                animator.SetBool( IS_WALLRUNNING, true );
                break;
        
        }
        
    }

    private void ResetAnimator()
    {
        animator.SetBool( IS_IDLE, false );
        animator.SetBool( IS_RUNNING, false );
        animator.SetBool( IS_JUMPING, false );
        animator.SetBool( IS_WALLRUNNING, false );
    }
}
