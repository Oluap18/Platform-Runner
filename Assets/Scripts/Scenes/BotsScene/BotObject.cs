using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BotObject : MonoBehaviour
{

    //Player Input
    private Rigidbody playerObject;
    private PlayerAnimator playerAnimator;

    //Data Structure
    private List<string> position;
    private List<string> velocity;
    private List<string> rotation;
    private List<PlayerAnimator.CurrentState> animations;
    private float time;
    private int iterator;

    public bool isReplaying;
    public bool destroyOnEnd;

    // Start is called before the first frame update
    void Awake()
    {
        ResetAllVariables();
        playerObject = this.GetComponent<Rigidbody>();
        playerAnimator = this.GetComponentInChildren<PlayerAnimator>();
        isReplaying = false;
        destroyOnEnd = true;
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
            iterator++;
        }
        if(isReplaying && iterator >= position.Count ) {
            playerObject.velocity = Vector3.zero;
            playerAnimator.SetCurrentState( playerAnimator.StringToCurrentState("Idle") );
            if(destroyOnEnd )
            {
                Destroy( this.gameObject );
            }
        }
    }

    public IEnumerator LoadData( string directoryPath, string fileName )
    {
        LevelRunStructure levelRunStructure = CommonDataMethods.LoadData( directoryPath, fileName ) as LevelRunStructure;
        if(levelRunStructure != null)
        {
            position.AddRange( levelRunStructure.position );
            velocity.AddRange( levelRunStructure.velocity );
            rotation.AddRange( levelRunStructure.rotation );

            time = levelRunStructure.time;

            this.transform.position = GeneralFunctions.StringToVector3( levelRunStructure.initialPosition );
            this.GetComponent<Rigidbody>().MoveRotation( GeneralFunctions.StringToQuaternion( levelRunStructure.initialRotation ) );
            this.GetComponent<Rigidbody>().velocity = GeneralFunctions.StringToVector3( levelRunStructure.initialVelocity );
            playerAnimator.SetCurrentState( playerAnimator.StringToCurrentState( levelRunStructure.initialAnimation ) );

            foreach(string animation in levelRunStructure.animations)
            {
                animations.Add( playerAnimator.StringToCurrentState( animation ) );
            }

            yield return null;
        }
        else
        {
            DestroyBot();
        }
    }

    private void ResetAllVariables()
    {
        position = new List<string>();
        velocity = new List<string>();
        rotation = new List<string>();
        animations = new List<PlayerAnimator.CurrentState>();
        iterator = 0;
        time = 0.0f;
    }

    public float GetTime()
    {
        return time;
    }

    public void DestroyBot()
    {
        Destroy( this.gameObject );
    }
}
