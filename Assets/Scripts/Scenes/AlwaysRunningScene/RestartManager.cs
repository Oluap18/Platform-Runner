using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class RestartManager : MonoBehaviour
{

    private GameObject player;
    private GameObject startPosition;
    private PlayerBasicMovement playerBasicMovement;
    private TimerController timerController;
    private CheckPointManager checkPointManager;

    public void Restart()
    {


        playerBasicMovement = FindObjectOfType<PlayerBasicMovement>();
        timerController = FindObjectOfType<TimerController>();
        player = GameObject.Find( CommonGameObjectsName.PLAYER_OBJECT_NAME );
        startPosition = GameObject.Find( CommonGameObjectsName.PLAYER_START_POSITION );
        checkPointManager = FindObjectOfType<CheckPointManager>();

        timerController.StopTimer();
        timerController.ResetTimer();
        checkPointManager.ResetCheckPoints();
        playerBasicMovement.DisablePlayerMovement();
        player.transform.position = startPosition.transform.position;
        player.GetComponent<Rigidbody>().MoveRotation( startPosition.transform.rotation );
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        List<string> scenesToLoad = new List<string>();
        scenesToLoad.Add( SceneName.START_COUNTDOWN_TIMER_UI_SCENE );

        StartCoroutine( GeneralFunctions.LoadScenes( scenesToLoad ));

        scenesToLoad.Clear();


    }

}
