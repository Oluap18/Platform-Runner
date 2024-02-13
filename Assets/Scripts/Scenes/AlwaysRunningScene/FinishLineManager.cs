using UnityEngine;

public class FinishLineManager : MonoBehaviour {
    
    private TimerController timerController;
    private PlayerBasicMovement playerBasicMovement;
    private BestTimeController bestTimeController;
    private RecordLevelRun recordLevelRun;

    public void TriggerFinishLine(string levelName)
    {
        bestTimeController = FindObjectOfType<BestTimeController>();
        timerController = FindObjectOfType<TimerController>();
        recordLevelRun = FindObjectOfType<RecordLevelRun>();

        timerController.StopTimer();

        string bestTime = bestTimeController.ReturnBestTime();
        playerBasicMovement = FindObjectOfType<PlayerBasicMovement>();
        playerBasicMovement.DisablePlayerMovement();

        recordLevelRun.time = timerController.GetCurrentTime();

        if(recordLevelRun.isRecording)
        {
            recordLevelRun.isRecording = false;
        }

        if(bestTime != null) {
            bestTime = bestTime.Replace( "Best Time: ", "" );
            if(GeneralFunctions.TimerStringToFloat( bestTime ) > timerController.GetCurrentTime()) {
                bestTimeController.SetupBestTime( timerController.GetCurrentTime() );
                LevelDataStructure levelDataStructure = new LevelDataStructure( levelName, timerController.GetCurrentTime() );
                CommonDataMethods.SaveData( CommonGameObjectsVariables.LEVEL_DATA_PATH, levelName, levelDataStructure );
            }
        }
        else {
            bestTimeController.SetupBestTime( timerController.GetCurrentTime() );
            LevelDataStructure levelDataStructure = new LevelDataStructure( levelName, timerController.GetCurrentTime() );
            CommonDataMethods.SaveData( CommonGameObjectsVariables.LEVEL_DATA_PATH, levelName, levelDataStructure );
        }

        
    }

    public void TriggerFinishLineBot()
    {
        BotObject botObject = FindObjectOfType<BotObject>();
        if(botObject != null )
        {
            botObject.isReplaying = false;
        }
    }

    public void TriggerFinishLineReplay()
    {
        timerController.StopTimer();

        BotObject botObject = FindAnyObjectByType<BotObject>();
        if(RecordPlayerRun.replay)
        {
            timerController.SetCurrentTime( botObject.GetTime() );
        }
        if(botObject != null)
        {
            botObject.isReplaying = false;
        }
    }
}
