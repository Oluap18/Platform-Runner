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

    //Data to be stored
    public static List<string> position;
    public static List<string> velocity;
    public static List<string> rotation;
    public static List<PlayerAnimator.CurrentState> animations;
    public static string initialPosition;
    public static string initialRotation;
    public static string initialVelocity;
    public static string initialAnimation;

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
            position.Add( playerObject.position.ToString() );
            velocity.Add( playerObject.velocity.ToString() );
            rotation.Add( playerObject.rotation.ToString() );
            animations.Add( playerAnimator.GetCurrentState() );
        }
    }

    private void InitiateRecording( InputAction.CallbackContext obj )
    {
        ResetAllVariables();
        initialAnimation = playerAnimator.CurrentStateToString( playerAnimator.GetCurrentState() );
        initialPosition = playerObject.position.ToString();
        initialRotation = playerObject.rotation.ToString();
        initialVelocity = playerObject.velocity.ToString();
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
            initialPosition,
            initialRotation,
            initialVelocity,
            initialAnimation
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
        iterator = 0;
        initialPosition = string.Empty;
        initialRotation = string.Empty;
        initialVelocity = string.Empty;
        initialAnimation = "Idle";
    }

    private void ReplayLastRecording( InputAction.CallbackContext obj )
    {
        string[] fileEntries = Directory.GetFiles( CommonGameObjectsVariables.CUSTOM_RECORDING_PATH );
        List<string> fileSorted = new List<string>(fileEntries);
        fileSorted.Reverse();
        
        BotObject botObject = botGenerator.CreateBotObject();
        StartCoroutine(botObject.LoadData( CommonGameObjectsVariables.CUSTOM_RECORDING_PATH, Path.GetFileName(fileSorted[0]) ));
        botObject.isReplaying = true;
        
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
