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
    private RecordLevelRun recordLevelRun;
    private bool initiateBestTimeReplay = true;

    IEnumerator Start()
    {
        CameraMovementPlayerControlled cameraMovementPlayerControlled = GameObject.FindObjectOfType<CameraMovementPlayerControlled>();
        cameraMovementPlayerControlled.EnableCameraMovement();

        recordLevelRun = FindObjectOfType<RecordLevelRun>();

        yield return new WaitForSeconds( 1f );

        while(countdownTime > 0) {

            countdownTimer.text = countdownTime.ToString();

            yield return new WaitForSeconds( 1f );

            countdownTime--;
        }

        countdownTimer.text = "GO";
        countdownTimer.color = Color.green;

        if(initiateBestTimeReplay)
        {
            BotGenerator botGenerator = FindObjectOfType<BotGenerator>();
            botGenerator.InitiateBestTimeReplay();
        }

        recordLevelRun.StartRecording();
        GeneralFunctions.EnableMovementOfPlayer();
        
        StartBots();

        yield return new WaitForSeconds( 1f );

        SceneManager.UnloadSceneAsync( SceneName.START_COUNTDOWN_TIMER_UI_SCENE );
    
    }

    private void StartBots()
    {
        //Start the bots that race against you
        BotObject[] botObjects = FindObjectsByType<BotObject>( FindObjectsSortMode.None);

        for(int i = 0; i < botObjects.Length; i++)
        {
            botObjects[i].SetDestroyOnEnd();
            botObjects[i].StartBotReplay();
        }
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

    public void InitiateBestTimeReplay()
    {
        initiateBestTimeReplay = true;
    }

    public void DisableBestTimeReplay() 
    { 
        initiateBestTimeReplay = false; 
    }
}
