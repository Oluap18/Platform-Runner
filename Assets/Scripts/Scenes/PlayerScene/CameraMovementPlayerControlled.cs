using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovementPlayerControlled : MonoBehaviour
{

    [Header( "Camera" )]
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [Header( "References" )]
    public Transform RotateAround;
    
    //Saves the position of the follow object in reference to the player
    private Vector3 directionToTarget;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        directionToTarget = transform.position - RotateAround.position;
    }

    private void Update()
    {
        
        //Make sure that the position of the follow object in reference to the player
        //maintains the same despite the player movement
        transform.position = RotateAround.position + directionToTarget;

        float mouseX = Mouse.current.delta.x.ReadValue() * Time.deltaTime * sensX;
        float mouseY = Mouse.current.delta.y.ReadValue() * Time.deltaTime * sensY;

        RotateHorizontally( mouseX );
        RotateVertically( mouseY );

        //Record the position of the follow object in reference to the player
        directionToTarget = transform.position - RotateAround.position;
    
    }

    private void RotateHorizontally (float mouseX) 
    {

        this.transform.RotateAround( RotateAround.position, Vector3.up, mouseX * Time.deltaTime );

    }

    private void RotateVertically(float mouseY )
    {
        
        this.transform.Rotate( Vector3.right, mouseY * Time.deltaTime );

    }

}
