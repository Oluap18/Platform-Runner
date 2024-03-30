using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordLevelRun : MonoBehaviour
{
    [Header("References")]
    //Player Input
    [SerializeField] private Rigidbody playerObject;
    [SerializeField] private PlayerAnimator playerAnimator;

    //CameraObjects
    [SerializeField] private Rigidbody cameraLookObjectBot;
    [SerializeField] private Rigidbody playerBasicMovementObjectBot;

    //Data Structure
    private List<string> position;
    private List<string> velocity;
    private List<string> rotation;
    private List<PlayerAnimator.CurrentState> animations;

    private List<string> cameraLookObjectPosition;
    private List<string> cameraLookObjectRotation;

    private List<string> playerBasicMovementObjectPosition;
    private List<string> playerBasicMovementObjectRotation;

    private float time;
    private int iterator;

    private bool isRecording;

    // Start is called before the first frame update
    void Awake()
    {
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
    
    public float GetTime()
    {
        return time;
    }

    public void SetTime(float time )
    {
        this.time = time;
    }
}
