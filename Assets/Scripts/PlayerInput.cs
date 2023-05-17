using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerInput : MonoBehaviour
{

    [SerializeField] private float jumpForce;
    [SerializeField] private int nbJumpsMax;

    private int nbJumpsCurrent;
    private PlayerInputActions playerInputActions;
    private Rigidbody parentRigidBody;

    private void Awake() {
        
        playerInputActions = new PlayerInputActions();
        playerInputActions.PlayerMovement.Enable();
        playerInputActions.PlayerMovement.Jump.performed += Jump;

        parentRigidBody = GetComponentInParent<Rigidbody>();

        nbJumpsCurrent = nbJumpsMax;
    
    }

    private void Jump( InputAction.CallbackContext obj ) {


        if(nbJumpsCurrent > 0) {

            parentRigidBody.AddForce( Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse );
            nbJumpsCurrent--;

        }
        
    }

    public Vector3 GetMovementVectorNormalized(){

        Vector3 inputVector = playerInputActions.PlayerMovement.BasicMovement.ReadValue<Vector3>();

        return inputVector.normalized;

    }

    public void RestoreJumpsAllowed() {

        nbJumpsCurrent = nbJumpsMax;
    
    }


}
