using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

    [Header("Player Input")]
    [SerializeField] private PlayerInput playerInput;

    [Header("Player Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Camera playerCamera;

    //Animation variables
    private bool isRunning;
    
    // Gravity Scale editable on the inspector
    // providing a gravity scale per object
    public float gravityScale = 1.0f;
    public static float globalGravity = -9.81f;

    //Player movement
    private Rigidbody rigidBody;

    private void Start() {
        
        isRunning = false;
        rigidBody = GetComponent<Rigidbody>();
    
    }

    // Update is called once per frame
    private void Update() {

    }

    private void FixedUpdate() {

        Vector3 finalMovement = CalculateMovementOnCamera();

        if(finalMovement != Vector3.zero) {
            isRunning = true;
        }
        else {
            isRunning = false;
        }

        rigidBody.MovePosition( transform.position + finalMovement );

        Vector3 rotationVector = Vector3.Slerp( transform.forward, finalMovement, Time.deltaTime * rotateSpeed );
        rigidBody.MoveRotation( Quaternion.LookRotation( rotationVector ) );

        addGravityForce( rigidBody );
    }

    private Vector3 CalculateMovementOnCamera() {
        
        //MovementInput
        Vector3 moveDir = playerInput.GetMovementVectorNormalized();

        //Camera Direction
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        //Movement according to the camera
        Vector3 forwardMovement = moveDir.z * cameraForward;
        Vector3 rightMovement = moveDir.x * cameraRight;
        Vector3 finalMovement = ( forwardMovement + rightMovement ) * moveSpeed;

        return finalMovement;
    }

    public bool IsRunning() {
        return isRunning;
    }

    private void addGravityForce( Rigidbody rigidBody ) {

        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rigidBody.AddForce( gravity, ForceMode.Acceleration );

    }

}
