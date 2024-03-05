using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GeneralFunctions
{

    private static List<bool> statusOfCursor = new List<bool>();

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

    public static void DisableMovementOfPlayer()
    {
        statusOfCursor.Add( false );
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PlayerBasicMovement playerBasicMovement = GameObject.FindObjectOfType<PlayerBasicMovement>();
        playerBasicMovement.DisablePlayerMovement();
        PlayerJumping playerJumping = GameObject.FindObjectOfType<PlayerJumping>();
        playerJumping.DisableJumpingAction();
        CameraMovementPlayerControlled cameraMovementPlayerControlled = GameObject.FindObjectOfType<CameraMovementPlayerControlled>();
        cameraMovementPlayerControlled.DisableCameraMovement();
        TimerController timerController = GameObject.FindObjectOfType<TimerController>();
        timerController.StopTimer();
    }

    public static void DisableMovementOfPlayer( int id )
    {
        statusOfCursor.Add( false );
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PlayerBasicMovement playerBasicMovement = GetPlayerBasicMovementOfID( id );
        playerBasicMovement.DisablePlayerMovement();
        PlayerJumping playerJumping = GetPlayerJumpingOfID( id );
        playerJumping.DisableJumpingAction();
        CameraMovementPlayerControlled cameraMovementPlayerControlled = GetCameraMovementPlayerControlledOfID( id );
        cameraMovementPlayerControlled.DisableCameraMovement();
        TimerController timerController = GameObject.FindObjectOfType<TimerController>();
        timerController.StopTimer();
    }

    public static void EnableMovementOfPlayer()
    {
        if(statusOfCursor.Count != 0)
        {
            statusOfCursor.RemoveAt( statusOfCursor.Count - 1 );
        }
        if( statusOfCursor.Count == 0 || statusOfCursor[statusOfCursor.Count -1 ] )
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            PlayerBasicMovement playerBasicMovement = GameObject.FindObjectOfType<PlayerBasicMovement>();
            playerBasicMovement.EnablePlayerMovement();
            PlayerJumping playerJumping = GameObject.FindObjectOfType<PlayerJumping>();
            playerJumping.EnableJumpingAction();
            CameraMovementPlayerControlled cameraMovementPlayerControlled = GameObject.FindObjectOfType<CameraMovementPlayerControlled>();
            cameraMovementPlayerControlled.EnableCameraMovement();
            TimerController timerController = GameObject.FindObjectOfType<TimerController>();
            timerController.StartTimer();

            statusOfCursor.Add( true );
        }
        
    }

    public static void EnableMovementOfPlayer(int id)
    {
        if(statusOfCursor.Count != 0)
        {
            statusOfCursor.RemoveAt( statusOfCursor.Count - 1 );
        }
        if(statusOfCursor.Count == 0 || statusOfCursor[statusOfCursor.Count - 1])
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            PlayerBasicMovement playerBasicMovement = GetPlayerBasicMovementOfID( id );
            playerBasicMovement.EnablePlayerMovement();
            PlayerJumping playerJumping = GetPlayerJumpingOfID( id );
            playerJumping.EnableJumpingAction();
            CameraMovementPlayerControlled cameraMovementPlayerControlled = GetCameraMovementPlayerControlledOfID( id );
            cameraMovementPlayerControlled.EnableCameraMovement();
            TimerController timerController = GameObject.FindObjectOfType<TimerController>();
            timerController.StartTimer();

            statusOfCursor.Add( true );
        }

    }

    public static void RemoveCursor()
    {
        statusOfCursor.RemoveAt( statusOfCursor.Count - 1 );
        if(statusOfCursor.Count == 0 || statusOfCursor[statusOfCursor.Count - 1])
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private static PlayerBasicMovement GetPlayerBasicMovementOfID(int id)
    {
        PlayerBasicMovement[] playerBasicMovement = GameObject.FindObjectsOfType<PlayerBasicMovement>();
        for(int i = 0; i < playerBasicMovement.Length; i++)
        {
            if(playerBasicMovement[i].transform.root.GetInstanceID() == id)
            {
                return playerBasicMovement[i];
            }
        }
        return null;
    }
    private static PlayerJumping GetPlayerJumpingOfID( int id )
    {
        PlayerJumping[] playerJumping = GameObject.FindObjectsOfType<PlayerJumping>();
        for(int i = 0; i < playerJumping.Length; i++)
        {
            if(playerJumping[i].transform.root.GetInstanceID() == id)
            {
                return playerJumping[i];
            }
        }
        return null;
    }

    private static CameraMovementPlayerControlled GetCameraMovementPlayerControlledOfID( int id )
    {
        CameraMovementPlayerControlled[] cameraMovementPlayerControlled = GameObject.FindObjectsOfType<CameraMovementPlayerControlled>();
        for(int i = 0; i < cameraMovementPlayerControlled.Length; i++)
        {
            if(cameraMovementPlayerControlled[i].transform.root.GetInstanceID() == id)
            {
                return cameraMovementPlayerControlled[i];
            }
        }
        return null;
    }

    private static GameObject GetGameObjectOfID( GameObject[] gameobjects, int id )
    {
        for(int i = 0; i < gameobjects.Length; i++)
        {
            if(gameobjects[i].transform.root.GetInstanceID() == id)
            {
                return gameobjects[i];
            }
        }
        return null;
    }
}
