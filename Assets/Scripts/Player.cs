using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Camera camera;

    private bool isRunning;

    // Start is called before the first frame update
    private void Start()
    {
        isRunning = false;
    }

    // Update is called once per frame
    private void Update()
    {

        //MovementInput
        Vector3 moveDir = playerInput.GetMovementVectorNormalized();

        //Camera Direction
        Vector3 cameraForward = camera.transform.forward;
        Vector3 cameraRight = camera.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        //Movement according to the camera
        Vector3 forwardMovement = moveDir.z * cameraForward;
        Vector3 rightMovement = moveDir.x * cameraRight;
        Vector3 finalMovement = forwardMovement + rightMovement;

        transform.position += finalMovement * moveSpeed * Time.deltaTime;

        if( moveDir != Vector3.zero ) {
            isRunning = true;
        }
        else {
            isRunning = false;
        }

        transform.forward = Vector3.Slerp(transform.forward, finalMovement, Time.deltaTime * rotateSpeed);
    }

    private void OnCollisionEnter( Collision collision ) {
        // Debug-draw all contact points and normals

        if( collision.gameObject.tag.Equals( "Floor" )) {

            playerInput.RestoreJumpsAllowed();

        }

    }

    public bool IsRunning() {
        return isRunning;
    }


}
