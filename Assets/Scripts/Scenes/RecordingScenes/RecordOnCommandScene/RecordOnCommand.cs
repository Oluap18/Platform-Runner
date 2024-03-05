using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
using System;

public class RecordOnCommand : MonoBehaviour
{
    //Player Input
    private PlayerInputActions playerInputActions;
    private bool isRecording;
    private Rigidbody playerObject;
    private PlayerAnimator playerAnimator;
    private GameObject startPosition;
    private BotGenerator botGenerator;

    //CameraObjects
    private Rigidbody cameraLookObjectBot;
    private Rigidbody playerBasicMovementObjectBot;

    //Data to be stored
    public static List<string> position;
    public static List<string> velocity;
    public static List<string> rotation;
    public static List<PlayerAnimator.CurrentState> animations;

    public static List<string> cameraLookObjectPosition;
    public static List<string> cameraLookObjectRotation;

    public static List<string> playerBasicMovementObjectPosition;
    public static List<string> playerBasicMovementObjectRotation;

    public static int iterator = 0;

    void Awake()
    {
        if(!RecordPlayerRun.replay)
        {
            ResetAllVariables();
            isRecording = false;
            playerInputActions = FindObjectOfType<PlayerInputManager>().GetPlayerInputActions();
            playerObject = GameObject.Find( CommonGameObjectsName.PLAYER_OBJECT_NAME ).GetComponent<Rigidbody>();
            playerAnimator = FindObjectOfType<PlayerAnimator>();
            startPosition = GameObject.Find( CommonGameObjectsName.PLAYER_START_POSITION );
            cameraLookObjectBot = GameObject.Find( CommonGameObjectsName.CAMERA_LOOK_OBJECT ).GetComponent<Rigidbody>();
            playerBasicMovementObjectBot = GameObject.Find( CommonGameObjectsName.PLAYER_BASIC_MOVEMENT_OBJECT ).GetComponent<Rigidbody>();
            botGenerator = FindObjectOfType<BotGenerator>();
        }
    }

    void OnEnable()
    {
        playerInputActions.RecordOnCommand.InitiateRecording.performed += InitiateRecording;
        playerInputActions.RecordOnCommand.StopRecording.performed += StopRecording;
        playerInputActions.RecordOnCommand.ReplayLastRecording.performed += ReplayLastRecording;
        playerInputActions.RecordOnCommand.Enable();
    }

    void OnDisable()
    {
        playerInputActions.RecordOnCommand.InitiateRecording.performed -= InitiateRecording;
        playerInputActions.RecordOnCommand.StopRecording.performed -= StopRecording;
        playerInputActions.RecordOnCommand.ReplayLastRecording.performed -= ReplayLastRecording;
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

    private void InitiateRecording( InputAction.CallbackContext obj )
    {
        ResetAllVariables();
        isRecording = true;
    }

    private void StopRecording( InputAction.CallbackContext obj )
    {
        isRecording = false;
        LevelRunStructure levelRunStructure = new LevelRunStructure(
            position.ToArray(),
            velocity.ToArray(),
            rotation.ToArray(),
            CommonDataMethods.ListAnimationToListString( animations ).ToArray(),
            cameraLookObjectPosition.ToArray(),
            cameraLookObjectRotation.ToArray(),
            playerBasicMovementObjectPosition.ToArray(),
            playerBasicMovementObjectRotation.ToArray()
            );

        string fileName = DateToString() + "_" + startPosition.scene.name;
        CommonDataMethods.SaveData( CommonGameObjectsVariables.CUSTOM_RECORDING_PATH, fileName, levelRunStructure );
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
    }

    private void ReplayLastRecording( InputAction.CallbackContext obj )
    {
        string[] fileEntries = Directory.GetFiles( CommonGameObjectsVariables.CUSTOM_RECORDING_PATH );
        List<string> fileSorted = new List<string>(fileEntries);
        fileSorted.Reverse();
        
        BotObject botObject = botGenerator.CreateBotObject();
        StartCoroutine(botObject.LoadData( CommonGameObjectsVariables.CUSTOM_RECORDING_PATH, Path.GetFileName(fileSorted[0]) ));
        StartCoroutine( botObject.StartCustomReplay() );
        
    }

    public string DateToString()
    {
        string date = null;
        date += DateTime.UtcNow.Year + "-";
        date += DateTime.UtcNow.Month + "-";
        date += DateTime.UtcNow.Day + "-";
        date += DateTime.UtcNow.Hour + ".";
        date += DateTime.UtcNow.Minute + ".";
        date += DateTime.UtcNow.Second + ".";
        date += DateTime.UtcNow.Millisecond;
        return date;
    }
}
