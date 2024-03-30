using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GeneralFunctions
{

    public static string FormatTimer( float timer )
    {
        int minutes = Mathf.FloorToInt( timer / 60F );
        int seconds = Mathf.FloorToInt( timer - minutes * 60 );
        int miliseconds = Mathf.FloorToInt( ( timer - minutes * 60 - seconds ) * 100 );

        string niceTime = string.Format( "{0:00}:{1:00}:{2:00}", minutes, seconds, miliseconds );

        return niceTime;
    }

    public static float TimerStringToFloat(string timer)
    {
        float time = 0;
        
        string[] splitTimer = timer.Split( ":" );
        time += int.Parse(splitTimer[0]) * 60F;
        time += int.Parse( splitTimer[1] );
        time += int.Parse( splitTimer[2] ) / 100F;

        return time;
    }

    public static IEnumerator LoadScenes( List<string> scenesToLoad )
    {
        List<AsyncOperation> loadOperations = new List<AsyncOperation>();

        scenesToLoad.ForEach( i => {
            loadOperations.Add( SceneManager.LoadSceneAsync( i, LoadSceneMode.Additive ) );
        } );

        while(loadOperations.Count > 0) {

            yield return null;

            foreach(AsyncOperation operation in loadOperations.ToList()) {

                if(operation.isDone) loadOperations.Remove( operation );

            }

        }
    }

    public static IEnumerator UnLoadScenes( List<string> scenesToUnLoad )
    {
        scenesToUnLoad.ForEach( i => {
            SceneManager.UnloadSceneAsync( i );
        } );

        yield return null;
    }

    public static Vector3 StringToVector3( string sVector )
    {
        // Remove the parentheses
        if(sVector.StartsWith( "(" ) && sVector.EndsWith( ")" ))
        {
            sVector = sVector.Substring( 1, sVector.Length - 2 );
        }

        // split the items
        string[] sArray = sVector.Split( ',' );

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse( sArray[0] ),
            float.Parse( sArray[1] ),
            float.Parse( sArray[2] ) );

        return result;
    }

    public static Quaternion StringToQuaternion( string sVector )
    {
        // Remove the parentheses
        if(sVector.StartsWith( "(" ) && sVector.EndsWith( ")" ))
        {
            sVector = sVector.Substring( 1, sVector.Length - 2 );
        }

        // split the items
        string[] sArray = sVector.Split( ',' );

        // store as a Vector3
        Quaternion result = new Quaternion(
            float.Parse( sArray[0] ),
            float.Parse( sArray[1] ),
            float.Parse( sArray[2] ),
            float.Parse( sArray[3] ) );

        return result;
    }

    public static void DisableAllPlayersAndBotsMovement()
    {
        PlayerGeneralFunctions[] playerGeneralFunctions = GameObject.FindObjectsByType<PlayerGeneralFunctions>( FindObjectsSortMode.None );
        for( int i = 0; i < playerGeneralFunctions.Length; i++ )
        {
            playerGeneralFunctions[i].DisableMovementOfPlayer();
        }
    }

    public static void EnableAllPlayersAndBotsMovement()
    {
        PlayerGeneralFunctions[] playerGeneralFunctions = GameObject.FindObjectsByType<PlayerGeneralFunctions>( FindObjectsSortMode.None );
        for(int i = 0; i < playerGeneralFunctions.Length; i++)
        {
            playerGeneralFunctions[i].EnablePlayerMovement();
        }
    }

    public static void RemoveCursorAllPlayers()
    {
        PlayerGeneralFunctions[] playerGeneralFunctions = GameObject.FindObjectsByType<PlayerGeneralFunctions>( FindObjectsSortMode.None );
        for(int i = 0; i < playerGeneralFunctions.Length; i++)
        {
            playerGeneralFunctions[i].RemoveCursor();
        }
    }
}
