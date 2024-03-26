using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class PlayerRestart : NetworkBehaviour
{
    private GameObject player;
    private PlayerInputActions playerInputActions;
    private GameObject startPosition;
    private RecordLevelRun recordLevelRun;

    [Header( "References" )]
    [SerializeField] private PlayerInputManager playerInputManager;
    [SerializeField] private PlayerBasicMovement playerBasicMovement;
    [SerializeField] private PlayerCheckPoint playerCheckPoint;
    [SerializeField] private PlayerGeneralFunctions playerGeneralFunctions;
    [SerializeField] private TimerController timerController;

    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        if(!IsOwner) return;
        playerInputActions = playerInputManager.GetPlayerInputActions();
    }

    private void Start()
    {
        if(!IsOwner) return;
        playerInputActions = playerInputManager.GetPlayerInputActions();
        playerInputActions.PlayerMovement.Restart.performed += Restart;
    }

    public override void OnNetworkDespawn()
    {
        if(!IsOwner) return;
        playerInputActions.PlayerMovement.Restart.performed -= Restart;
    }

    private void Restart( InputAction.CallbackContext obj )
    {
        player = GameObject.Find( CommonGameObjectsName.PLAYER_OBJECT_NAME );
        startPosition = GameObject.Find( CommonGameObjectsName.PLAYER_START_POSITION );
        recordLevelRun = FindObjectOfType<RecordLevelRun>();

        timerController.StopTimer();
        timerController.ResetTimer();
        playerCheckPoint.ResetCheckPoints();
        playerGeneralFunctions.DisableMovementOfPlayer();
        recordLevelRun.ClearData();

        player.transform.position = startPosition.transform.position;
        player.GetComponent<Rigidbody>().MoveRotation( startPosition.transform.rotation );
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        List<string> scenesToLoad = new List<string>();
        scenesToLoad.Add( SceneName.START_COUNTDOWN_TIMER_UI_SCENE );

        StartCoroutine( GeneralFunctions.LoadScenes( scenesToLoad ) );

        scenesToLoad.Clear();

    }
}
