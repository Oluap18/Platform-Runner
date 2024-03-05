using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelRunStructure
{
    //Player
    public string[] position;
    public string[] velocity;
    public string[] rotation;
    public string[] animations;

    //Camera Look Object
    public string[] cameraLookObjectPosition;
    public string[] cameraLookObjectRotation;

    //Player Basic Movement Object
    public string[] playerBasicMovementObjectPosition;
    public string[] playerBasicMovementObjectRotation;
    public float time;

    public LevelRunStructure( string[] position, string[] velocity, string[] rotation, string[] animations, string[] cameraLookObjectPosition, string[] cameraLookObjectRotation, string[] playerBasicMovementObjectPosition, string[] playerBasicMovementObjectRotation, float time )
    {
        this.position = position;
        this.velocity = velocity;
        this.rotation = rotation;
        this.animations = animations;
        this.cameraLookObjectPosition = cameraLookObjectPosition;
        this.cameraLookObjectRotation = cameraLookObjectRotation;
        this.playerBasicMovementObjectPosition = playerBasicMovementObjectPosition;
        this.playerBasicMovementObjectRotation = playerBasicMovementObjectRotation;
        this.time = time;
    }

    public LevelRunStructure( string[] position, string[] velocity, string[] rotation, string[] animations, string[] cameraLookObjectPosition, string[] cameraLookObjectRotation, string[] playerBasicMovementObjectPosition, string[] playerBasicMovementObjectRotation )
    {
        this.position = position;
        this.velocity = velocity;
        this.rotation = rotation;
        this.animations = animations;
        this.cameraLookObjectPosition = cameraLookObjectPosition;
        this.cameraLookObjectRotation = cameraLookObjectRotation;
        this.playerBasicMovementObjectPosition = playerBasicMovementObjectPosition;
        this.playerBasicMovementObjectRotation = playerBasicMovementObjectRotation;
        this.time = 0.0f;
    }
}
