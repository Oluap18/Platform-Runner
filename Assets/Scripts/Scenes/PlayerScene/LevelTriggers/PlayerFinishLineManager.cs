using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class PlayerFinishLineManager : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private TimerController timerController;
    [SerializeField] private BestTimeController bestTimeController;
    [SerializeField] private PlayerGeneralFunctions playerGeneralFunctions;
    private RecordLevelRun recordLevelRun;
    private GameObject startPosition;
    private string levelName;

    private void Start()
    {
        if(!IsOwner) return;
        recordLevelRun = FindObjectOfType<RecordLevelRun>();
        startPosition = GameObject.Find( CommonGameObjectsName.PLAYER_START_POSITION );
        levelName = startPosition.scene.name;
    }

    public void TriggerFinishLine()
    {
        timerController.StopTimer();

        string bestTime = bestTimeController.ReturnBestTime();
        playerGeneralFunctions.DisableMovementOfPlayer();

        recordLevelRun.SetTime(timerController.GetCurrentTime());

        if(recordLevelRun.GetRecordingStatus())
        {
            recordLevelRun.StopRecording();
        }

        if(bestTime != null)
        {
            bestTime = bestTime.Replace( "Best Time: ", "" );
            if(GeneralFunctions.TimerStringToFloat( bestTime ) > timerController.GetCurrentTime())
            {
                bestTimeController.SetupBestTime( timerController.GetCurrentTime() );
                SaveBestTime( levelName );
                recordLevelRun.SaveData( levelName );
            }
        }
        else
        {
            bestTimeController.SetupBestTime( timerController.GetCurrentTime() );
            SaveBestTime( levelName );
            recordLevelRun.SaveData( levelName );
        }
    }

    private void SaveBestTime( string levelName )
    {
        LevelDataStructure levelDataStructure = new LevelDataStructure( levelName, timerController.GetCurrentTime() );
        CommonDataMethods.SaveData( CommonGameObjectsVariables.LEVEL_DATA_PATH, levelName, levelDataStructure );
    }

    public void TriggerFinishLineBot()
    {
        BotObject botObject = FindObjectOfType<BotObject>();
        if(botObject != null)
        {
            botObject.StopBotReplay();
        }
    }

    public void TriggerFinishLineReplay()
    {
        timerController.StopTimer();

        BotObject botObject = FindAnyObjectByType<BotObject>();
        if(LocalVariablesScript.replayOnly)
        {
            timerController.SetCurrentTime( botObject.GetTime() );
        }
        if(botObject != null)
        {
            botObject.StopBotReplay();
        }
    }
}