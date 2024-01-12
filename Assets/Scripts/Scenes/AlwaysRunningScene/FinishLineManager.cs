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

        string bestTime = bestTimeController.ReturnBestTime();
        
        if(bestTime != null) {
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
