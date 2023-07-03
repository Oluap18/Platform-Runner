using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    [Header( "Jumping" )]
    [SerializeField] private PlayerJumping playerJumping;

    private void OnCollisionEnter( Collision collision ) 
    {
        // Debug-draw all contact points and normals

        if(collision.gameObject.tag.Equals( "Floor" )) {

            playerJumping.ResetJumpsAllowed();

        }
    }
}
