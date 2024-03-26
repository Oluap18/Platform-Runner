using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SetupPlayerInitialPosition : NetworkBehaviour
{
    void Start()
    {
        if(!IsOwner) return;
        GameObject startPosition = GameObject.Find( CommonGameObjectsName.PLAYER_START_POSITION );
        this.transform.position = startPosition.transform.position;
        this.GetComponent<Rigidbody>().MoveRotation( startPosition.transform.rotation );
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
