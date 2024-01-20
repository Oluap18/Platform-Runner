using UnityEngine;

public class FinishLineManager : MonoBehaviour {
    
    private TimerController timerController;
    private PlayerBasicMovement playerBasicMovement;
    private BestTimeController bestTimeController;

    public void TriggerFinishLine(string levelName)
    {
        playerBasicMovement = FindObjectOfType<PlayerBasicMovement>();
        timerController = FindObjectOfType<TimerController>();
        bestTimeController = FindObjectOfType<BestTimeController>();

        timerController.StopTimer();
        playerBasicMovement.DisablePlayerMovement();
        RecordPlayerRun.time = timerController.GetCurrentTime();
        if(RecordPlayerRun.record)
        {
            RecordPlayerRun.record = false;
            RecordPlayerRun.SaveData( levelName );
        }
        if(RecordPlayerRun.replay) 
        {
            RecordPlayerRun.started = false;
        }

        string bestTime = bestTimeController.ReturnBestTime();
        if(RecordPlayerRun.replay)
        {
            timerController.SetCurrentTime( RecordPlayerRun.time );
        }

        if(bestTime != null && !RecordPlayerRun.replay) {
            bestTime = bestTime.Replace( "Best Time: ", "" );
            if(GeneralFunctions.TimerStringToFloat( bestTime ) > timerController.GetCurrentTime()) {
                bestTimeController.SetupBestTime( timerController.GetCurrentTime() );
                LevelData.SaveLevelData( levelName, timerController.GetCurrentTime() );
            }
        }
        else {
            bestTimeController.SetupBestTime( timerController.GetCurrentTime() );
            LevelData.SaveLevelData( levelName, timerController.GetCurrentTime() );
        }

        
    }
}
