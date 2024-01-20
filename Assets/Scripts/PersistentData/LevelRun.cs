using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class LevelRun
{
    public static void SaveLevelRun( string levelName )
    {
        System.IO.FileInfo file = new System.IO.FileInfo( CommonGameObjectsVariables.LEVEL_RUN_PATH );
        file.Directory.Create();

        BinaryFormatter formatter = new BinaryFormatter();
        string path = CommonGameObjectsVariables.LEVEL_RUN_PATH + levelName;
        FileStream stream = new FileStream( path, FileMode.Create );

        LevelRunStructure levelData = new LevelRunStructure(
            RecordPlayerRun.velocity.ToArray(),
            RecordPlayerRun.rotation.ToArray(),
            ListAnimationToListString(RecordPlayerRun.animations).ToArray(),
            RecordPlayerRun.checkpointStillAction.ToArray(),
            RecordPlayerRun.checkpointRunningAction.ToArray(),
            RecordPlayerRun.time
            );
        formatter.Serialize( stream, levelData );
        stream.Close();
    }

    public static LevelRunStructure LoadLevelRun( string levelName )
    {
        System.IO.FileInfo file = new System.IO.FileInfo( CommonGameObjectsVariables.LEVEL_RUN_PATH );
        file.Directory.Create();

        string path = CommonGameObjectsVariables.LEVEL_RUN_PATH + levelName;

        if(File.Exists( path ))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream( path, FileMode.Open );

            LevelRunStructure levelRun = null;

            if(stream != null)
            {
                levelRun = formatter.Deserialize( stream ) as LevelRunStructure;
            }
            stream.Close();
            return levelRun;
        }
        else
        {
            return null;
        }
    }

    private static List<string> ListAnimationToListString( List<PlayerAnimator.CurrentState> animations )
    {
        PlayerAnimator playerAnimator = GameObject.FindObjectOfType<PlayerAnimator>();
        List<string> list = new List<string>();

        foreach(var animation in animations)
        {
            list.Add( playerAnimator.CurrentStateToString( animation ) );
        }
        return list;
    }
}
