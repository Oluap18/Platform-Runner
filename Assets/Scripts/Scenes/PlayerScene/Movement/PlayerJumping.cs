using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
using System.Runtime.CompilerServices;

public class PlayerJumping : NetworkBehaviour {

    [Header( "Jumping" )]
    [SerializeField] private float jumpForce;
    [SerializeField] private int nbJumpsMax;

    [Header( "References" )]
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private Rigidbody parentRigidBody;
    [SerializeField] private PlayerInputManager playerInputManager;
    [SerializeField] private PlayerRPC playerRPC;

    private int nbJumpsCurrent;
    private PlayerInputActions playerInputActions;

    private List<PlayerAnimator.CurrentState> allowedJumpingStates;
    private bool jumpingEnabled;

    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        if(!IsOwner) return;
        jumpingEnabled = false;
        playerInputActions = playerInputManager.GetPlayerInputActions();

        nbJumpsCurrent = nbJumpsMax;

        allowedJumpingStates = new List<PlayerAnimator.CurrentState> {
            PlayerAnimator.CurrentState.Idle,
            PlayerAnimator.CurrentState.Running,
            PlayerAnimator.CurrentState.Falling
        };
    }

    public void Start()
    {
        if(!IsOwner) return;
        playerInputActions = playerInputManager.GetPlayerInputActions();
        playerInputActions.PlayerMovement.Jump.performed += Jump;
    }

    public override void OnNetworkDespawn()
    {
        if(!IsOwner) return;
        playerInputActions.PlayerMovement.Jump.performed -= Jump;
    }

    private void Jump( InputAction.CallbackContext obj)
    {

        JumpAction();
    }

    private void JumpAction()
    {

        if(allowedJumpingStates.Contains( playerAnimator.GetCurrentState() ) && jumpingEnabled)
        {

            if(nbJumpsCurrent > 0 && !playerAnimator.GetGoingToJump())
            {

                //So that gravity doesn't affect the second jump
                Vector3 velocity = parentRigidBody.velocity;
                if(velocity.y < 0) { velocity.y = 0; }
                parentRigidBody.velocity = velocity;

                playerAnimator.SetGoingToJump();
                playerRPC.AddPlayerForceServerRpc( Vector3.up * jumpForce );
                Debug.Log( "Jump Action" );
                //parentRigidBody.AddForce( Vector3.up * jumpForce, ForceMode.Impulse );
                DecreaseNBJumpsCurrent();

            }
        }
    }

    public void ResetJumpsAllowed()
    {

        nbJumpsCurrent = nbJumpsMax;

    }

    public void DecreaseNBJumpsCurrent()
    {
        nbJumpsCurrent--;
    }

    public void DisableJumpingAction()
    {
        jumpingEnabled = false;
    }

    public void EnableJumpingAction( )
    {
        jumpingEnabled = true;
    }
}