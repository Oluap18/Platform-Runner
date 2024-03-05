using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    private CheckPointManager checkpointManager;

    private void OnTriggerEnter( Collider other )
    {
        if(other.tag == CommonGameObjectsTags.PLAYER_TAG)
        {
            checkpointManager = FindObjectOfType<CheckPointManager>();
            checkpointManager.TriggerCheckPoint( this.gameObject.GetInstanceID() );
        }
    }
}
