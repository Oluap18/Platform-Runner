using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelDataStructure
{
    public string levelName;
    public float time;

    public LevelDataStructure( string levelName, float time )
    {
        this.levelName = levelName;
        this.time = time;
    }
}
