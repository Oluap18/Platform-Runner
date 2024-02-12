using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordLevelRun : MonoBehaviour
{
    //Player Input
    private Rigidbody playerObject;
    private PlayerAnimator playerAnimator;

    //Data Structure
    public List<string> position;
    public List<string> velocity;
    public List<string> rotation;
    public List<PlayerAnimator.CurrentState> animations;
    public string initialPosition;
    public string initialRotation;
    public string initialVelocity;
    public string initialAnimation;
    public float time;
    public int iterator;

    public bool isRecording;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find( CommonGameObjectsName.PLAYER_OBJECT_NAME ).GetComponent<Rigidbody>();
        playerAnimator = FindObjectOfType<PlayerAnimator>();
        initialPosition = playerObject.position.ToString();
        initialRotation = playerObject.rotation.ToString();
        initialVelocity = playerObject.velocity.ToString();
        initialAnimation = playerAnimator.CurrentStateToString( playerAnimator.GetCurrentState() );
        ClearData();
        isRecording = false;
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

    public void SaveData( string level )
    {
        LevelRunStructure levelRun = new LevelRunStructure(
            position.ToArray(),
            velocity.ToArray(),
            rotation.ToArray(),
            CommonDataMethods.ListAnimationToListString( animations ).ToArray(),
            initialPosition,
            initialRotation,
            initialVelocity,
            initialAnimation,
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
        initialPosition = playerObject.position.ToString();
        initialRotation = playerObject.rotation.ToString();
        initialVelocity = playerObject.velocity.ToString();
        initialAnimation = playerAnimator.CurrentStateToString( playerAnimator.GetCurrentState() );
        iterator = 0;
        time = 0.0f;
    }

    
}
