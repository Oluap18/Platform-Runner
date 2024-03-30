using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class RestartManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInputManager playerInputManager;
    [SerializeField] private PlayerBasicMovement playerBasicMovement;
    [SerializeField] private GameObject player;
    [SerializeField] private CheckPointManager checkPointManager;
    [SerializeField] private TimerController timerController;
    [SerializeField] private RecordLevelRun recordLevelRun;

    private GameObject startPosition;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = playerInputManager.GetPlayerInputActions();
        startPosition = GameObject.Find( CommonGameObjectsName.PLAYER_START_POSITION );
    }

    public void Restart( InputAction.CallbackContext obj )
    {
        timerController.StopTimer();
        timerController.ResetTimer();
        checkPointManager.ResetCheckPoints();
        GeneralFunctions.DisableAllPlayersAndBotsMovement();
        recordLevelRun.ClearData();

        player.transform.position = startPosition.transform.position;
        player.GetComponent<Rigidbody>().MoveRotation( startPosition.transform.rotation );
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        /*List<string> scenesToLoad = new List<string>();
        scenesToLoad.Add( SceneName.START_COUNTDOWN_TIMER_UI_SCENE );

        StartCoroutine( GeneralFunctions.LoadScenes( scenesToLoad ));

        scenesToLoad.Clear();*/
    }

    private void OnEnable()
    {
        playerInputActions.PlayerMovement.Restart.performed += Restart;
    }

    private void OnDisable()
    {
        playerInputActions.PlayerMovement.Restart.performed -= Restart;
    }

}
