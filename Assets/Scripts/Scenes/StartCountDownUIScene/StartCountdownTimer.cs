using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCountdownTimer : MonoBehaviour {
    [Header( "Countdown Timer" )]
    [SerializeField] private TextMeshProUGUI countdownTimer;

    private int countdownTime = 3;
    private PlayerBasicMovement playerBasicMovement;
    private TimerController timerController;
    private RecordLevelRun recordLevelRun;

    IEnumerator Start()
    {
        
        playerBasicMovement = FindObjectOfType<PlayerBasicMovement>();
        timerController = FindObjectOfType<TimerController>();
        recordLevelRun = FindObjectOfType<RecordLevelRun>();

        StartBotDataLoad();

        yield return new WaitForSeconds( 1f );

        while(countdownTime > 0) {

            countdownTimer.text = countdownTime.ToString();

            yield return new WaitForSeconds( 1f );

            countdownTime--;
        }

        countdownTimer.text = "GO";
        countdownTimer.color = Color.green;
        BotObject botObject = FindObjectOfType<BotObject>();

        if( botObject != null )
        {
            botObject.isReplaying = true;
        }

        recordLevelRun.isRecording = true;
        playerBasicMovement.EnablePlayerMovement();


        timerController.StartTimer();

        yield return new WaitForSeconds( 1f );

        SceneManager.UnloadSceneAsync( SceneName.START_COUNTDOWN_TIMER_UI_SCENE );
    
    }

    private void StartBotDataLoad()
    {
        BotObject botObject = FindObjectOfType<BotObject>();
        GameObject startPosition = GameObject.Find( CommonGameObjectsName.PLAYER_START_POSITION );
        StartCoroutine( botObject.LoadData( CommonGameObjectsVariables.LEVEL_RUN_PATH, startPosition.scene.name ) );
    }
}
