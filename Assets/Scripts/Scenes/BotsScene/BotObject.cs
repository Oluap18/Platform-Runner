using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

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

    // Start is called before the first frame update
    void Start()
    {
        ResetAllVariables();
        playerObject = this.GetComponent<Rigidbody>();
        playerAnimator = this.GetComponentInChildren<PlayerAnimator>();
        isReplaying = false;
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
            List<string> scenes = new List<string>();
            scenes.Add( this.gameObject.scene.name );
            StartCoroutine(GeneralFunctions.UnLoadScenes( scenes ));
            yield return null;
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
}
