using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BestTimeController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestTimeText;

    void Start()
    {
        GameObject startPosition = GameObject.Find( CommonGameObjectsName.PLAYER_START_POSITION );
        SetupBestTime( startPosition.gameObject.scene.name );
    }

    private void SetupBestTime(string levelName)
    {
        LevelDataStructure levelDataStructure = CommonDataMethods.LoadData( CommonGameObjectsVariables.LEVEL_DATA_PATH, levelName ) as LevelDataStructure;
        if(levelDataStructure != null) {
            bestTimeText.text = "Best Time: " + GeneralFunctions.FormatTimer( levelDataStructure.time );
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
