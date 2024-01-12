using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class LevelData {

    public static void SaveLevelData(string levelName, float time )
    {
        System.IO.FileInfo file = new System.IO.FileInfo( CommonGameObjectsVariables.LEVEL_DATA_PATH );
        file.Directory.Create();

        BinaryFormatter formatter = new BinaryFormatter();
        string path = CommonGameObjectsVariables.LEVEL_DATA_PATH + levelName;
        FileStream stream = new FileStream( path, FileMode.Create );

        LevelDataStructure levelData = new LevelDataStructure( levelName, time );
        formatter.Serialize( stream, levelData );
        stream.Close();
    }

    public static LevelDataStructure LoadLevelData(string levelName)
    {
        System.IO.FileInfo file = new System.IO.FileInfo( CommonGameObjectsVariables.LEVEL_DATA_PATH );
        file.Directory.Create();

        string path = CommonGameObjectsVariables.LEVEL_DATA_PATH + levelName;

        if (File.Exists( path ) ) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream( path, FileMode.Open );

            LevelDataStructure levelData = null;

            if(stream != null) {
                levelData = formatter.Deserialize( stream ) as LevelDataStructure;
            }
            stream.Close();
            return levelData;
        }
        else {
            return null;
        }
    }

}
