using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BestTimeController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestTimeText;
    private LevelData levelData;

    void Start()
    {
        GameObject startPosition = GameObject.Find( CommonGameObjectsName.PLAYER_START_POSITION );
        SetupBestTime( startPosition.gameObject.scene.name );
    }

    private void SetupBestTime(string levelName)
    {
        levelData = FindObjectOfType<LevelData>();
        LevelDataStructure leveldata = levelData.LoadLevelData( levelName );
        if(leveldata != null) {
            bestTimeText.text = "Best Time: " + GeneralFunctions.FormatTimer( leveldata.time );
        }
        else {
            bestTimeText.text = null;
        }
    }

    public void SetupBestTime( float time )
    {
        bestTimeText.text = "Best Time: " + GeneralFunctions.FormatTimer( time );
    }

    public void RemoveBestTime()
    {
        bestTimeText.text = null;
    }

    public string ReturnBestTime()
    {
        return bestTimeText.text;
    }


}
