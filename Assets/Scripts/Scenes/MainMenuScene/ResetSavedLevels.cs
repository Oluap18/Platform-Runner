using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ResetSavedLevels : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string version = CommonDataMethods.LoadData( CommonGameObjectsVariables.NEW_VERSION_PATH, CommonGameObjectsVariables.PLAYER_KEYBINDS_FILENAME ) as string;
        if( version == null || version != "0.0.3")
        {
            CommonDataMethods.SaveData( CommonGameObjectsVariables.NEW_VERSION_PATH, CommonGameObjectsVariables.PLAYER_KEYBINDS_FILENAME, "0.0.3" );
            DeleteOldFiles();
        }
    }

    private void DeleteOldFiles()
    {
        string[] fileEntries = Directory.GetFiles( CommonGameObjectsVariables.LEVEL_DATA_PATH );

        for( int i = 0; i < fileEntries.Length; i++)
        {
            File.Delete( fileEntries[i] );
        }

        fileEntries = Directory.GetFiles( CommonGameObjectsVariables.LEVEL_RUN_PATH );

        for(int i = 0; i < fileEntries.Length; i++)
        {
            File.Delete( fileEntries[i] );
        }

    }
}
