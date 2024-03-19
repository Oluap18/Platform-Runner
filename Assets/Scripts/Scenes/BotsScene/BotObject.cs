using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Cinemachine;

public class BotObject : MonoBehaviour
{

    //Player Input
    private Rigidbody playerObject;
    private PlayerAnimator playerAnimator;

    //CameraObjects
    private Rigidbody cameraLookObjectBot;
    private Rigidbody playerBasicMovementObjectBot;
    private CinemachineFreeLook freeLookCamera;

    //Data Structure
    private List<string> position;
    private List<string> velocity;
    private List<string> rotation;
    private List<PlayerAnimator.CurrentState> animations;

    public List<string> cameraLookObjectPosition;
    public List<string> cameraLookObjectRotation;

    public List<string> playerBasicMovementObjectPosition;
    public List<string> playerBasicMovementObjectRotation;

    private float time;
    private int iterator;

    private bool isReplaying;
    private bool destroyOnEnd;
    private Action actionAfterDestroy;
    private int originalPriority;

    // Start is called before the first frame update
    void Awake()
    {
        ResetAllVariables();
        playerObject = this.GetComponent<Rigidbody>();
        playerAnimator = this.GetComponentInChildren<PlayerAnimator>();
 
        cameraLookObjectBot = this.transform.Find( "CamerasBot/" + CommonGameObjectsName.CAMERA_LOOK_OBJECT + "Bot" ).GetComponent<Rigidbody>();
        playerBasicMovementObjectBot = this.transform.Find( "CamerasBot/" + CommonGameObjectsName.PLAYER_BASIC_MOVEMENT_OBJECT + "Bot" ).GetComponent<Rigidbody>();
        freeLookCamera = this.transform.Find( "CamerasBot/" + CommonGameObjectsName.PLAYER_FREE_LOOK_CAMERA + "Bot" ).GetComponent<CinemachineFreeLook>();

        isReplaying = false;
        SetDestroyOnEnd();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isReplaying && iterator < position.Count)
        {
            playerObject.position = GeneralFunctions.StringToVector3( position[iterator] );
            playerObject.velocity = GeneralFunctions.StringToVector3( velocity[iterator] );
            playerObject.rotation = GeneralFunctions.StringToQuaternion( rotation[iterator] );
            playerAnimator.SetCurrentState( animations[iterator] );

            cameraLookObjectBot.position = GeneralFunctions.StringToVector3( cameraLookObjectPosition[iterator] );
            cameraLookObjectBot.rotation = GeneralFunctions.StringToQuaternion( cameraLookObjectRotation[iterator] );

            playerBasicMovementObjectBot.position = GeneralFunctions.StringToVector3( playerBasicMovementObjectPosition[iterator] );
            playerBasicMovementObjectBot.rotation = GeneralFunctions.StringToQuaternion( playerBasicMovementObjectRotation[iterator] );

            iterator++;
        }
        //Reached the end of the replay
        if(isReplaying && iterator >= position.Count ) {
            playerObject.velocity = Vector3.zero;
            playerAnimator.SetCurrentState( playerAnimator.StringToCurrentState("Idle") );
            if(destroyOnEnd )
            {
                if(actionAfterDestroy != null)
                {
                    actionAfterDestroy();
                }
                Destroy( this.gameObject );
            }
        }
    }

    public IEnumerator LoadData( string directoryPath, string fileName )
    {
        originalPriority = freeLookCamera.m_Priority;
        freeLookCamera.m_Priority = 0;
        LevelRunStructure levelRunStructure = CommonDataMethods.LoadData( directoryPath, fileName ) as LevelRunStructure;

        if(levelRunStructure != null)
        {
            position.AddRange( levelRunStructure.position );
            velocity.AddRange( levelRunStructure.velocity );
            rotation.AddRange( levelRunStructure.rotation );

            cameraLookObjectPosition.AddRange( levelRunStructure.cameraLookObjectPosition );
            cameraLookObjectRotation.AddRange( levelRunStructure.cameraLookObjectRotation );

            playerBasicMovementObjectPosition.AddRange( levelRunStructure.playerBasicMovementObjectPosition );
            playerBasicMovementObjectRotation.AddRange( levelRunStructure.playerBasicMovementObjectRotation );

            time = levelRunStructure.time;


            foreach(string animation in levelRunStructure.animations)
            {
                animations.Add( playerAnimator.StringToCurrentState( animation ) );
            }
            SetupInitialPosition();
            yield return null;
        }
        else
        {
            DestroyBot();
        }
    }

    private void SetupInitialPosition()
    {
        playerObject.position = GeneralFunctions.StringToVector3( position[0] );
        playerObject.velocity = GeneralFunctions.StringToVector3( velocity[0] );
        playerObject.rotation = GeneralFunctions.StringToQuaternion( rotation[0] );
        playerAnimator.SetCurrentState( animations[0] );

        cameraLookObjectBot.position = GeneralFunctions.StringToVector3( cameraLookObjectPosition[0] );
        cameraLookObjectBot.rotation = GeneralFunctions.StringToQuaternion( cameraLookObjectRotation[0] );

        playerBasicMovementObjectBot.position = GeneralFunctions.StringToVector3( playerBasicMovementObjectPosition[0] );
        playerBasicMovementObjectBot.rotation = GeneralFunctions.StringToQuaternion( playerBasicMovementObjectRotation[0] );
        
    }

    private void ResetAllVariables()
    {
        position = new List<string>();
        velocity = new List<string>();
        rotation = new List<string>();
        animations = new List<PlayerAnimator.CurrentState>();

        cameraLookObjectPosition = new List<string>();
        cameraLookObjectRotation = new List<string>();

        playerBasicMovementObjectPosition = new List<string>();
        playerBasicMovementObjectRotation = new List<string>();

        iterator = 0;
        time = 0.0f;
    }

    private void ResetCameraPriority()
    {
        freeLookCamera.m_Priority = originalPriority;
    }

    public float GetTime()
    {
        return time;
    }

    public void DestroyBot()
    {
        Destroy( this.gameObject );
    }

    public void StartBotReplay()
    {
        isReplaying = true;
    }

    public IEnumerator StartCustomReplay( Action action )
    {
        yield return new WaitForSeconds( 0.5f );
        ResetCameraPriority();
        yield return new WaitForSeconds( 1.0f );
        actionAfterDestroy = action;
        isReplaying = true;
    }
    public IEnumerator StartCustomReplay()
    {
        yield return new WaitForSeconds( 0.5f );
        ResetCameraPriority();
        yield return new WaitForSeconds( 1.0f );
        isReplaying = true;
    }

    public void StopBotReplay()
    {
        isReplaying = false;
    }

    public void SetDestroyOnEnd()
    {
        destroyOnEnd = true;
    }

    public void DisableDestroyOnEnd()
    {
        destroyOnEnd = false;
    }
}
