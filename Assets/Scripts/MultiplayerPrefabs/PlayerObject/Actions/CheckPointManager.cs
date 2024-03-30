using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckPointManager : MonoBehaviour
{
    [Header( "References" )]
    [SerializeField] private PlayerInputManager playerInputManager;
    [SerializeField] private TimerController timerController;
    [SerializeField] private GameObject player;

    private List<Vector3> position;
    private List<Vector3> velocity;
    private List<Quaternion> rotation;
    private List<int> checkPointID;
    private List<float> checkPointTimers;

    private int checkPointsPassed = 0;
    private PlayerInputActions playerInputActions;


    private void Awake()
    {
        playerInputActions = playerInputManager.GetPlayerInputActions();
        position = new List<Vector3>();
        velocity = new List<Vector3>();
        checkPointID = new List<int>();
        checkPointTimers = new List<float>();
        rotation = new List<Quaternion>();


    }

    private void OnEnable()
    {
        playerInputActions.PlayerMovement.Respawn.performed += RespawnStill;
        playerInputActions.PlayerMovement.Respawn.started += RespawnWithVelocity;
    }

    private void OnDisable()
    {
        playerInputActions.PlayerMovement.Respawn.performed -= RespawnStill;
        playerInputActions.PlayerMovement.Respawn.started -= RespawnWithVelocity;
    }

    public void TriggerCheckPoint( int checkPointName )
    {

        if(!checkPointID.Contains( checkPointName ))
        {
            position.Add( player.transform.position );
            velocity.Add( player.GetComponent<Rigidbody>().velocity );
            rotation.Add( player.transform.rotation );
            checkPointID.Add( checkPointName );
            checkPointTimers.Add( timerController.GetCurrentTime() );
            checkPointsPassed++;
        }
        
    }

    public void RespawnWithVelocity( InputAction.CallbackContext obj )
    {
        if(position.Count > 0) {
            player.transform.position = position[checkPointsPassed - 1];
            player.GetComponent<Rigidbody>().velocity = velocity[checkPointsPassed - 1];
            player.transform.rotation = rotation[checkPointsPassed - 1];
        }
    }

    public void RespawnStill( InputAction.CallbackContext obj ) 
    {
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
        checkPointsPassed = 0;
    }
}
