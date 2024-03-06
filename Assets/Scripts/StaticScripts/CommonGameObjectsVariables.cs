using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonGameObjectsVariables
{
    public static string LEVEL_DATA_PATH = Application.persistentDataPath + "/LevelData/";
    public static string PLAYER_KEYBINDS_PATH = Application.persistentDataPath + "/PlayerKeybinds/";
    public static string LEVEL_RUN_PATH = Application.persistentDataPath + "/PlayerRuns/";
    public static string CUSTOM_RECORDING_PATH = Application.persistentDataPath + "/CustomRecordings/";
    public static string TUTORIAL_RECORDING_PATH = Application.dataPath + "/TutorialRecordings/";
    public static string PLAYER_KEYBINDS_FILENAME = "PlayerKeybinds";
    public static string NEW_VERSION_PATH = Application.persistentDataPath + "/NewVersion/";
    public static string NEW_VERSION_FILENAME = "NewVersion";
    public static int PLAYER_INPUT_ACTIONS_FORWARD_INDEX = 3;
    public static int PLAYER_INPUT_ACTIONS_BACKWARD_INDEX = 4;
    public static int PLAYER_INPUT_ACTIONS_RIGHT_INDEX = 2;
    public static int PLAYER_INPUT_ACTIONS_LEFT_INDEX = 1;
}
