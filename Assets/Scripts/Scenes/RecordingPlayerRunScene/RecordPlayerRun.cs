using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RecordPlayerRun
{

    public static List<string> velocity = new List<string>();
    public static List<string> rotation = new List<string>();
    public static List<PlayerAnimator.CurrentState> animations = new List<PlayerAnimator.CurrentState>();
	public static List<int> checkpointStillAction = new List<int>();
    public static List<int> checkpointRunningAction = new List<int>();
    public static float time = 0.0f;
    
    public static int iterator = 0;
    
    public static bool record = false;
    public static bool replay = false;
    
    public static bool started = false;

    public static void SaveData(string level)
    {
        LevelRun.SaveLevelRun( level );
    }

    public static void ClearData()
    {
        velocity = new List<string>();
        rotation = new List<string>();
        animations = new List<PlayerAnimator.CurrentState>();
        checkpointStillAction = new List<int>();
        checkpointRunningAction = new List<int>();
        iterator = 0;
        time = 0.0f;
    }

    public static IEnumerator LoadData( string level )
    {

        PlayerAnimator playerAnimator = GameObject.FindObjectOfType<PlayerAnimator>();

        while(playerAnimator == null )
        {
            yield return null;
            playerAnimator = GameObject.FindObjectOfType<PlayerAnimator>();
        }

        LevelRunStructure levelRunStructure = LevelRun.LoadLevelRun( level );
        velocity.AddRange(levelRunStructure.velocity);
        rotation.AddRange(levelRunStructure.rotation);
        checkpointStillAction.AddRange(levelRunStructure.checkpointStillAction);
        checkpointRunningAction.AddRange(levelRunStructure.checkpointRunningAction);
        
        foreach (string animation in levelRunStructure.animations)
        {
            animations.Add( playerAnimator.StringToCurrentState( animation ) );
        }

        time = levelRunStructure.time;
    }
}

