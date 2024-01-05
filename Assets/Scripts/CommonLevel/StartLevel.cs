using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        List<string> loadScenes = new List<string> ();
        loadScenes.Add( SceneName.OVERLAY_UI_SCENE );

        StartCoroutine(GeneralFunctions.LoadScenes( loadScenes ));

        loadScenes.Clear();
        loadScenes.Add( SceneName.START_COUNTDOWN_TIMER_UI_SCENE );

        StartCoroutine(GeneralFunctions.LoadScenes( loadScenes ));
    }
}
