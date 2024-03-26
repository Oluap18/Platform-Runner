using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    private void OnTriggerEnter( Collider other )
    {
        if(other.tag == CommonGameObjectsTags.PLAYER_TAG)
        {
            GeneralFunctions.TriggerAllPlayerCheckPoint( this.gameObject.GetInstanceID() );
        }
    }
}
