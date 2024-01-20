using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelRunStructure
{

    public string[] velocity;
    public string[] rotation;
    public string[] animations;
    public int[] checkpointStillAction;
    public int[] checkpointRunningAction;
    public float time;

    public LevelRunStructure( string[] velocity, string[] rotation, string[] animations, int[] checkpointStillAction, int[] checkpointRunningAction, float time )
    {
        this.velocity = velocity;
        this.rotation = rotation;
        this.animations = animations;
        this.checkpointStillAction = checkpointStillAction;
        this.checkpointRunningAction = checkpointRunningAction;
        this.time = time;
    }
}
