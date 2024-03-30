using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupPlayerInitialPosition : MonoBehaviour
{
    void Start()
    {
        GameObject startPosition = GameObject.Find( CommonGameObjectsName.PLAYER_START_POSITION );
        this.transform.position = startPosition.transform.position;
        this.GetComponent<Rigidbody>().MoveRotation( startPosition.transform.rotation );
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
