using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHelper : MonoBehaviour
{
    public IEnumerator StartLevel()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        yield return StartCoroutine( dialogueManager.StartDialogue( DialogueText.TUTORIAL_CHECKPOINT_INTRODUCTION ) );

        GameObject arrowTutorial = GameObject.Find( CommonGameObjectsName.TUTORIAL_ARROW );
        arrowTutorial.SetActive( false );

        List<string> scenesToLoad = new List<string>();
        scenesToLoad.Add( SceneName.START_COUNTDOWN_TIMER_UI_SCENE );
        StartCoroutine( GeneralFunctions.LoadScenes( scenesToLoad ) );
    }
}
