using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovementPlayerControlled : MonoBehaviour {

    [Header( "Camera" )]
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [Header( "References" )]
    [SerializeField] private Transform RotateAround;
    [SerializeField] private Transform cameraObjectMovement;


    //Saves the position of the follow object in reference to the player
    private Vector3 directionToTarget;
    private PlayerInputManager playerInputManager;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        directionToTarget = transform.position - RotateAround.position;
        playerInputManager = FindObjectOfType<PlayerInputManager>();
    }

    private void FixedUpdate()
    {

        //Make sure that the position of the follow object in reference to the player
        //maintains the same despite the player movement
        transform.position = RotateAround.position + directionToTarget;

        float mouseX = Mouse.current.delta.x.ReadValue() * Time.deltaTime * sensX * playerInputManager.GetCameraSensitivity();
        float mouseY = Mouse.current.delta.y.ReadValue() * Time.deltaTime * sensY * playerInputManager.GetInvertedCamera() * playerInputManager.GetCameraSensitivity();

        RotateHorizontally( mouseX );
        RotateVertically( mouseY );

        cameraObjectMovement.position = transform.position;
        cameraObjectMovement.rotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, transform.rotation.w);

        //Record the position of the follow object in reference to the player
        directionToTarget = transform.position - RotateAround.position;

    }

    private void RotateHorizontally( float mouseX )
    {

        this.transform.RotateAround( RotateAround.position, Vector3.up, mouseX * Time.deltaTime );

    }

    private void RotateVertically( float mouseY )
    {

        this.transform.Rotate( Vector3.right, mouseY * Time.deltaTime );

    }

}
