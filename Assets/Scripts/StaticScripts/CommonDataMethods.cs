using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class CommonDataMethods
{

    public static void SaveData(string directoryPath, string fileName, object data)
    {
        System.IO.FileInfo file = new System.IO.FileInfo( directoryPath );
        file.Directory.Create();

        BinaryFormatter formatter = new BinaryFormatter();
        string path = directoryPath + fileName;
        FileStream stream = new FileStream( path, FileMode.Create );

        formatter.Serialize( stream, data );
        stream.Close();
    }

    public static object LoadData( string directoryPath, string fileName )
    {
        System.IO.FileInfo file = new System.IO.FileInfo( directoryPath );
        file.Directory.Create();

        string path = directoryPath + fileName;

        if(System.IO.File.Exists( path ))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream( path, FileMode.Open );

            object loadingObject = null;

            if(stream != null)
            {
                loadingObject = formatter.Deserialize( stream );
            }
            stream.Close();
            return loadingObject;
        }
        else
        {
            Debug.Log( "File: " + directoryPath + fileName + " does not exist" );
            return null;
        }
    }

    public static List<string> ListAnimationToListString( List<PlayerAnimator.CurrentState> animations )
    {
        PlayerAnimator playerAnimator = GameObject.FindObjectOfType<PlayerAnimator>();
        List<string> list = new List<string>();

        foreach(var animation in animations)
        {
            list.Add( playerAnimator.CurrentStateToString( animation ) );
        }
        return list;
    }
}
