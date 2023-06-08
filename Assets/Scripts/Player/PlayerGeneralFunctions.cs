using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGeneralFunctions : MonoBehaviour
{

    public float minJumpHeight;
    public LayerMask whatIsGround;

    public bool AboveGround()
    {
        return !Physics.Raycast( transform.position, Vector3.down, minJumpHeight, whatIsGround );
    }
}
