using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionDetectionPlayer : MonoBehaviour
{

    [SerializeField] private PlayerInput playerInput;

    private void OnCollisionStay( Collision collision ) {
        // Debug-draw all contact points and normals

        if(collision.gameObject.tag.Equals( "Floor" )) {

            playerInput.RestoreJumpsAllowed();

        }
    }
}
