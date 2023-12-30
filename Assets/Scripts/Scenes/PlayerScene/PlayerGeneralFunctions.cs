using UnityEngine;

public class PlayerGeneralFunctions : MonoBehaviour {

    [Header( "GeneralValues" )]
    [SerializeField] private float minJumpHeight;
    [SerializeField] private LayerMask whatIsGround;

    public bool AboveGround()
    {
        return !Physics.Raycast( transform.position, Vector3.down, minJumpHeight, whatIsGround );
    }
}
