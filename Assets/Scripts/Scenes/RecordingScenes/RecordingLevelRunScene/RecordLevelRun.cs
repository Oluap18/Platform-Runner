using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordLevelRun : MonoBehaviour
{
    //Player Input
    private Rigidbody playerObject;
    private PlayerAnimator playerAnimator;

    //CameraObjects
    private Rigidbody cameraLookObjectBot;
    private Rigidbody playerBasicMovementObjectBot;

    //Data Structure
    public List<string> position;
    public List<string> velocity;
    public List<string> rotation;
    public List<PlayerAnimator.CurrentState> animations;

    public List<string> cameraLookObjectPosition;
    public List<string> cameraLookObjectRotation;

    public List<string> playerBasicMovementObjectPosition;
    public List<string> playerBasicMovementObjectRotation;

    public float time;
    public int iterator;

    private bool isRecording;

    // Start is called before the first frame update
    void Awake()
    {
        playerObject = GameObject.Find( CommonGameObjectsName.PLAYER_OBJECT_NAME ).GetComponent<Rigidbody>();
        playerAnimator = FindObjectOfType<PlayerAnimator>();
        cameraLookObjectBot = GameObject.Find( CommonGameObjectsName.CAMERA_LOOK_OBJECT ).GetComponent<Rigidbody>();
        playerBasicMovementObjectBot = GameObject.Find( CommonGameObjectsName.PLAYER_BASIC_MOVEMENT_OBJECT ).GetComponent<Rigidbody>();
        ClearData();
        isRecording = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isRecording)
        {

            //Player
            position.Add( playerObject.position.ToString() );
            velocity.Add( playerObject.velocity.ToString() );
            rotation.Add( playerObject.rotation.ToString() );
            animations.Add( playerAnimator.GetCurrentState() );

            //Camera Look Object
            cameraLookObjectPosition.Add( cameraLookObjectBot.position.ToString() );
            cameraLookObjectRotation.Add( cameraLookObjectBot.rotation.ToString() );

            //Player Basic Movement Object
            playerBasicMovementObjectPosition.Add( playerBasicMovementObjectBot.position.ToString() );
            playerBasicMovementObjectRotation.Add( playerBasicMovementObjectBot.rotation.ToString() );
        }
    }

    public void SaveData( string level )
    {
        LevelRunStructure levelRun = new LevelRunStructure(
            position.ToArray(),
            velocity.ToArray(),
            rotation.ToArray(),
            CommonDataMethods.ListAnimationToListString( animations ).ToArray(),
            cameraLookObjectPosition.ToArray(),
            cameraLookObjectRotation.ToArray(),
            playerBasicMovementObjectPosition.ToArray(),
            playerBasicMovementObjectRotation.ToArray(),
            time
            );

        CommonDataMethods.SaveData( CommonGameObjectsVariables.LEVEL_RUN_PATH, level, levelRun );
    }

    public void ClearData()
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

    public void StartRecording()
    {
        isRecording = true;
    }

    public void StopRecording()
    {
        isRecording = false;
    }

    public bool GetRecordingStatus()
    {
        return isRecording;
    }
    
}
