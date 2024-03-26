using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Unity.Netcode;

public class LoaderCallback : NetworkBehaviour {

    private static List<string> scenesToLoad = new List<string>();
    private static List<string> scenesToUnload = new List<string>();
    private static List<AsyncOperation> loadOperations = new List<AsyncOperation>();

    [Header( "Slider" )]
    [SerializeField] private Slider loadingBar;
    [SerializeField] private TextMeshProUGUI loadingPercentage;

    IEnumerator Start()
    {
        scenesToLoad.ForEach( i => {
            loadOperations.Add( SceneManager.LoadSceneAsync( i, LoadSceneMode.Additive ) );
        } );

        while(loadOperations.Count > 0) {

            float progressValue = 0f;
            float count = loadOperations.Count;

            loadOperations.ForEach( i => {
                progressValue += Mathf.Clamp01( ( i.progress / 0.9f ) / count );
            } );

            loadingBar.value = progressValue;
            loadingPercentage.text = progressValue.ToString() + "%";

            yield return null;

            foreach(AsyncOperation operation in loadOperations.ToList()) {

                if(operation.isDone) loadOperations.Remove( operation );

            }

        }

        StartCoroutine(GeneralFunctions.UnLoadScenes( scenesToUnload ));

        ResetScenes();

        
        SceneManager.UnloadSceneAsync( SceneName.LOADING_SCENE );


    }

    public static void SetScenesToLoad( List<string> scenes )
    {
        scenesToLoad.AddRange( scenes );
    }

    public static void SetScenesToUnload( List<string> scenes )
    {
        scenesToUnload.AddRange( scenes );
    }

    private void ResetScenes()
    {
        scenesToLoad.Clear();
        scenesToUnload.Clear();
        loadOperations.Clear();
    }
}
