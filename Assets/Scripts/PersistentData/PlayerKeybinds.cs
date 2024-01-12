using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.InputSystem;

public static class PlayerKeybinds {

    
    public static void SavePlayerKeybinds( PlayerInputManager playerInputManager )
    {
        PlayerKeybindsStructure playerKeybindsStructure = new PlayerKeybindsStructure( playerInputManager );
        System.IO.FileInfo file = new System.IO.FileInfo( CommonGameObjectsVariables.PLAYER_KEYBINDS_PATH );
        file.Directory.Create();

        BinaryFormatter formatter = new BinaryFormatter();
        string path = CommonGameObjectsVariables.PLAYER_KEYBINDS_PATH + CommonGameObjectsVariables.PLAYER_KEYBINDS_FILENAME;
        FileStream stream = new FileStream( path, FileMode.Create );

        formatter.Serialize( stream, playerKeybindsStructure );
        stream.Close();
    }

    public static void LoadPlayerKeybinds( PlayerInputManager playerInputManager )
    {
        System.IO.FileInfo file = new System.IO.FileInfo( CommonGameObjectsVariables.PLAYER_KEYBINDS_PATH );
        file.Directory.Create();

        string path = CommonGameObjectsVariables.PLAYER_KEYBINDS_PATH + CommonGameObjectsVariables.PLAYER_KEYBINDS_FILENAME;

        if (File.Exists( path ) ) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream( path, FileMode.Open );

            if(stream != null) {
                PlayerKeybindsStructure playerKeybindsStructure = formatter.Deserialize( stream ) as PlayerKeybindsStructure;
                playerInputManager.GetPlayerInputActions().LoadBindingOverridesFromJson( playerKeybindsStructure.playerInputActions );
                playerInputManager.SetCameraSensitivity( playerKeybindsStructure.cameraSensitivity );
                playerInputManager.SetInvertedCamera( playerKeybindsStructure.invertedCamera );
            }
            stream.Close();
            
        }
    }

}
