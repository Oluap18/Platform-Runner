using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RecordOnCommand : MonoBehaviour
{
    //Player Input
    private PlayerInputActions playerInputActions;
    private bool isRecording;
    private Rigidbody playerObject;
    private PlayerAnimator playerAnimator;

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

    // Start is called before the first frame update
    void Start()
    {
        if(!RecordPlayerRun.replay)
        {
            ResetAllVariables();
            isRecording = false;
            playerInputActions = FindObjectOfType<PlayerInputManager>().GetPlayerInputActions();
            playerInputActions.RecordOnCommand.Enable();
            playerInputActions.RecordOnCommand.InitiateRecording.performed += InitiateRecording;
            playerInputActions.RecordOnCommand.StopRecording.performed += StopRecording;
            playerObject = GameObject.Find( CommonGameObjectsName.PLAYER_OBJECT_NAME ).GetComponent<Rigidbody>();
            playerAnimator = FindObjectOfType<PlayerAnimator>();
        }
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
        Debug.Log( "Stop" );
        isRecording = false;
        CustomRecordingStructure customRecordingStructure = new CustomRecordingStructure(
            position.ToArray(),
            velocity.ToArray(),
            rotation.ToArray(),
            CommonDataMethods.ListAnimationToListString( animations ).ToArray(),
            initialPosition,
            initialRotation,
            initialVelocity,
            initialAnimation
            );
        CommonDataMethods.SaveData( CommonGameObjectsVariables.CUSTOM_RECORDING_PATH, System.DateTime.Now.ToString(), customRecordingStructure );
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
}
