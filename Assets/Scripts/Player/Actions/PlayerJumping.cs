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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Jump( InputAction.CallbackContext obj ) 
    {

        if(nbJumpsCurrent > 0) {


            if(playerBasicMovement.GetCurrentState().Equals( PlayerBasicMovement.CurrentState.Idle )) {
                playerBasicMovement.currentState = PlayerBasicMovement.CurrentState.Jumping;
            }
            else {
                playerBasicMovement.currentState = PlayerBasicMovement.CurrentState.Jumping;
            }
            playerBasicMovement.currentState = PlayerBasicMovement.CurrentState.Jumping;
            parentRigidBody.AddForce( Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse );
            nbJumpsCurrent--;

        }

    }

    public void ResetJumpsAllowed() 
    {

        nbJumpsCurrent = nbJumpsMax;

    }
}
