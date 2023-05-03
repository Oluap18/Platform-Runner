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

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 moveDir = playerInput.GetMovementVectorNormalized();

        transform.position += moveDir * moveSpeed * Time.deltaTime;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    void OnCollisionEnter( Collision collision ) {
        // Debug-draw all contact points and normals

        if( collision.gameObject.tag == "Floor") {

            playerInput.RestoreJumpsAllowed();

        }

    }


}
