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

    private bool isRunning;

    // Start is called before the first frame update
    private void Start()
    {
        isRunning = false;
    }

    // Update is called once per frame
    private void Update()
    {

        Vector3 moveDir = playerInput.GetMovementVectorNormalized();

        transform.position += moveDir * moveSpeed * Time.deltaTime;

        if( moveDir != Vector3.zero ) {
            isRunning = true;
        }
        else {
            isRunning = false;
        }

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
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
