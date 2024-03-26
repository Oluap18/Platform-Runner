using UnityEngine;
using Unity.Netcode;

public class PlayerCollision : NetworkBehaviour {

    [Header( "Jumping" )]
    [SerializeField] private PlayerJumping playerJumping;
    [SerializeField] private PlayerWallClimbing playerWallClimbing;
    [SerializeField] private PlayerWallRunning playerWallRunning;
    [SerializeField] private PlayerBasicMovement playerBasicMovement;

    private void OnCollisionEnter( Collision collision )
    {
        if(!IsOwner) return;
        // Debug-draw all contact points and normals

        if (collision.gameObject.tag.Equals( "Floor" )) {

            playerJumping.ResetJumpsAllowed();
            playerWallClimbing.ResetClimbTimer();
            playerWallRunning.ResetWallRunTimer();
            playerBasicMovement.ResetMoveSpeed();

        }
    }
}
