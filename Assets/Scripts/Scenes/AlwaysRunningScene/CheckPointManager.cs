using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CheckPointManager : MonoBehaviour
{

    private List<Vector3> position;
    private List<Vector3> velocity;
    private List<Quaternion> rotation;
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
        rotation = new List<Quaternion>();


    }

    public void TriggerCheckPoint( int checkPointName )
    {

        player = GameObject.Find( CommonGameObjectsName.PLAYER_OBJECT_NAME );
        timerController = FindObjectOfType<TimerController>();

        if(!checkPointID.Contains( checkPointName )) {
            position.Add( player.transform.position );
            velocity.Add( player.GetComponent<Rigidbody>().velocity );
            rotation.Add( player.transform.rotation );
            checkPointID.Add( checkPointName );
            checkPointTimers.Add( timerController.GetCurrentTime() );
            checkPointsPassed++;
        }
        
    }

    public void RespawnWithVelocity()
    {
        RecordPlayerRun.checkpointRunningAction.Add( RecordPlayerRun.velocity.Count );
        if(position.Count > 0) {
            player.transform.position = position[checkPointsPassed - 1];
            player.GetComponent<Rigidbody>().velocity = velocity[checkPointsPassed - 1];
            player.transform.rotation = rotation[checkPointsPassed - 1];
        }

    }

    public void RespawnStill() 
    {
        RecordPlayerRun.checkpointStillAction.Add( RecordPlayerRun.velocity.Count );
        if(position.Count > 0) {
            player.transform.position = position[checkPointsPassed - 1];
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.transform.rotation = rotation[checkPointsPassed - 1];
        }

    }

    public void ResetCheckPoints()
    {
        position = new List<Vector3>();
        velocity = new List<Vector3>();
        checkPointID = new List<int>();
        checkPointTimers = new List<float>();
        rotation = new List<Quaternion>();
    }
}
