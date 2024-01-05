using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CheckPointManager : MonoBehaviour
{

    private List<Vector3> position;
    private List<Vector3> velocity;
    private List<int> checkPointID;
    private List<float> checkPointTimers;
    private GameObject player;

    private int checkPointsPassed = 0;

    private TimerController timerController;


    private void Start()
    {
        position = new List<Vector3>();
        velocity = new List<Vector3>();
        checkPointID = new List<int>();
        checkPointTimers = new List<float>();
        
    }

    public void TriggerCheckPoint( int checkPointName )
    {

        player = GameObject.Find( CommonGameObjectsName.PLAYER_OBJECT_NAME );
        timerController = FindObjectOfType<TimerController>();

        if(!checkPointID.Contains( checkPointName )) {
            position.Add( player.transform.position );
            velocity.Add( player.GetComponent<Rigidbody>().velocity );
            checkPointID.Add( checkPointName );
            checkPointTimers.Add( timerController.GetCurrentTime() );
            checkPointsPassed++;
        }
        
    }

    public void RespawnWithVelocity()
    {
        
        if(position.Count > 0) {
            player.transform.position = position[checkPointsPassed - 1];
            player.GetComponent<Rigidbody>().velocity = velocity[checkPointsPassed - 1];
        }

    }

    public void RespawnStill() 
    {

        if(position.Count > 0) {
            player.transform.position = position[checkPointsPassed - 1];
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

    }

    public void ResetCheckPoints()
    {
        position = new List<Vector3>();
        velocity = new List<Vector3>();
        checkPointID = new List<int>();
        checkPointTimers = new List<float>();
    }
}