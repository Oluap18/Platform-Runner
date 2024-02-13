using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
        List<BotObject> botObjects = GetAllBots();

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
            botObject.destroyOnEnd = false;
            botObject.isReplaying = true;
        }

        recordLevelRun.isRecording = true;
        playerBasicMovement.EnablePlayerMovement();
        foreach(BotObject child in botObjects)
        {
            child.isReplaying = true;
        }


        timerController.StartTimer();

        yield return new WaitForSeconds( 1f );

        SceneManager.UnloadSceneAsync( SceneName.START_COUNTDOWN_TIMER_UI_SCENE );
    
    }

    private List<BotObject> GetAllBots()
    {
        BotGenerator botGenerator = FindObjectOfType<BotGenerator>();
        int childCount = botGenerator.transform.childCount;
        List<BotObject> childObjects = new List<BotObject>();

        for( int i = 0; i < childCount; i++ )
        {
            BotObject child = botGenerator.transform.GetChild( i ).gameObject.GetComponent<BotObject>();
            childObjects.Add( child );
        }

        return childObjects;
    }
}
