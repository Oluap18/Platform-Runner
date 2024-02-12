using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomRecordingStructure
{
    public string[] position;
    public string[] velocity;
    public string[] rotation;
    public string[] animations;
    public string initialPosition;
    public string initialRotation;
    public string initialVelocity;
    public string initialAnimation;

    public CustomRecordingStructure( string[] position, string[] velocity, string[] rotation, string[] animations, string initialPosition, string initialRotation, string initialVelocity, string initialAnimation )
    {
        this.position = position;
        this.velocity = velocity;
        this.rotation = rotation;
        this.animations = animations;
        this.initialPosition = initialPosition;
        this.initialRotation = initialRotation;
        this.initialVelocity = initialVelocity;
        this.initialAnimation = initialAnimation;
    }
}
