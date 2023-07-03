using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumping : MonoBehaviour
{

    [Header( "Jumping" )]
    [SerializeField] private float jumpForce;
    [SerializeField] private int nbJumpsMax;

    [Header( "References" )]
    [SerializeField] private PlayerBasicMovement playerBasicMovement;
    [SerializeField] private PlayerAnimator playerAnimator;

    private int nbJumpsCurrent;
    private PlayerInputActions playerInputActions;
    private Rigidbody parentRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.PlayerMovement.Enable();
        playerInputActions.PlayerMovement.Jump.performed += Jump;

        parentRigidBody = GetComponentInParent<Rigidbody>();

        nbJumpsCurrent = nbJumpsMax;
    }

    private void Jump( InputAction.CallbackContext obj ) 
    {

        if(nbJumpsCurrent > 0 && !playerAnimator.GetGoingToJump()) {

            playerAnimator.SetGoingToJump();
            playerAnimator.SetCurrentState( PlayerAnimator.CurrentState.Jumping );
            parentRigidBody.AddForce( Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse );
            nbJumpsCurrent--;

        }
    }

    public void ResetJumpsAllowed() 
    {

        nbJumpsCurrent = nbJumpsMax;

    }
}
