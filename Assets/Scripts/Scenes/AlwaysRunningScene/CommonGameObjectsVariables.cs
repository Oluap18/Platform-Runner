using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonGameObjectsVariables
{
    public static string LEVEL_DATA_PATH = Application.persistentDataPath + "/LevelData/";
    public static string PLAYER_KEYBINDS_PATH = Application.persistentDataPath + "/PlayerKeybinds/";
    public static string PLAYER_KEYBINDS_FILENAME = "PlayerKeybinds";
    public static int PLAYER_INPUT_ACTIONS_FORWARD_INDEX = 3;
    public static int PLAYER_INPUT_ACTIONS_BACKWARD_INDEX = 4;
    public static int PLAYER_INPUT_ACTIONS_RIGHT_INDEX = 2;
    public static int PLAYER_INPUT_ACTIONS_LEFT_INDEX = 1;
}
