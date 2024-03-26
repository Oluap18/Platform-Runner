using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class PlayerCheckPoint : NetworkBehaviour
{

    private PlayerInputActions playerInputActions;
    private List<Vector3> position;
    private List<Vector3> velocity;
    private List<Quaternion> rotation;
    private List<int> checkPointID;
    private List<float> checkPointTimers;

    private int checkPointsPassed = 0;

    [Header( "References" )]
    [SerializeField] private PlayerInputManager playerInputManager;
    [SerializeField] private GameObject player;
    [SerializeField] private TimerController timerController;


    public override void OnNetworkSpawn()
    {
        if(!IsOwner) return;
        playerInputActions = playerInputManager.GetPlayerInputActions();
        position = new List<Vector3>();
        velocity = new List<Vector3>();
        checkPointID = new List<int>();
        checkPointTimers = new List<float>();
        rotation = new List<Quaternion>();
    }

    private void Start()
    {
        if(!IsOwner) return;
        playerInputActions = playerInputManager.GetPlayerInputActions();
        playerInputActions.PlayerMovement.Respawn.performed += RespawnStill;
        playerInputActions.PlayerMovement.Respawn.started += RespawnWithVelocity;
    }

    public override void OnNetworkDespawn()
    {
        if(!IsOwner) return;
        playerInputActions.PlayerMovement.Respawn.performed -= RespawnStill;
        playerInputActions.PlayerMovement.Respawn.started -= RespawnWithVelocity;
    }

    private void RespawnStill( InputAction.CallbackContext obj )
    {

        if(position.Count > 0)
        {
            player.transform.position = position[checkPointsPassed - 1];
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.transform.rotation = rotation[checkPointsPassed - 1];
        }

    }

    private void RespawnWithVelocity( InputAction.CallbackContext obj )
    {

        if(position.Count > 0)
        {
            player.transform.position = position[checkPointsPassed - 1];
            player.GetComponent<Rigidbody>().velocity = velocity[checkPointsPassed - 1];
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

    public void TriggerPlayerCheckPoint(int checkPointName )
    {
        if(!IsOwner) return;

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
}
