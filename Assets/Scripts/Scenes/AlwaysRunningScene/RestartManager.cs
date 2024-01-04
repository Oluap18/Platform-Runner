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

    public void Restart()
    {
        playerBasicMovement = FindObjectOfType<PlayerBasicMovement>();
        timerController = FindObjectOfType<TimerController>();

        timerController.StopTimer();
        timerController.ResetTimer();
        playerBasicMovement.DisablePlayerMovement();

        player = GameObject.Find( CommonGameObjectsName.PLAYER_OBJECT_NAME );
        startPosition = GameObject.Find( CommonGameObjectsName.PLAYER_START_POSITION );
        player.transform.position = startPosition.transform.position;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        SceneManager.LoadSceneAsync( SceneName.START_COUNTDOWN_TIMER_UI_SCENE, LoadSceneMode.Additive );
        
    }

}
