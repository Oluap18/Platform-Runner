using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BotGenerator : MonoBehaviour
{
    public BotObject prefab;

    /* Creates a new instance for your prefab. */
    public BotObject CreateBotObject()
    {

        BotObject go = Instantiate( prefab, this.transform );
        return go;
    }

    public void InitiateBestTimeReplay()
    {
        GameObject startPosition = GameObject.Find( CommonGameObjectsName.PLAYER_START_POSITION );
        if(System.IO.File.Exists( CommonGameObjectsVariables.LEVEL_RUN_PATH + startPosition.scene.name ))
        {
            BotObject botObject = CreateBotObject();
            StartCoroutine(botObject.LoadData( CommonGameObjectsVariables.LEVEL_RUN_PATH, startPosition.scene.name ));
        }
    }

    public void PlayCustomRecording(string filename, Action action)
    {
        BotObject botObject = CreateBotObject();
        StartCoroutine( botObject.LoadData( CommonGameObjectsVariables.CUSTOM_RECORDING_PATH, filename ) );
        StartCoroutine( botObject.StartCustomReplay( action ) );
    }

    public void PlayCustomRecording( string filename )
    {
        BotObject botObject = CreateBotObject();
        StartCoroutine( botObject.LoadData( CommonGameObjectsVariables.CUSTOM_RECORDING_PATH, filename ) );
        StartCoroutine( botObject.StartCustomReplay( ) );
    }
}
