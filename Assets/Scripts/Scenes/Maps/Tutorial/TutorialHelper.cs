using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHelper : MonoBehaviour
{

    [SerializeField] private GameObject tutorialCanvas;
    [SerializeField] private GameObject tutorialTriggers;

    public void StartTutorial()
    {
        tutorialCanvas.SetActive(false);
        StartCoroutine( StartTutorialRoutine() );
    }

    private IEnumerator StartTutorialRoutine()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        yield return StartCoroutine( dialogueManager.StartDialogue( DialogueText.TUTORIAL_CHECKPOINT_INTRODUCTION ) );

        GameObject arrowTutorial = GameObject.Find( CommonGameObjectsName.TUTORIAL_ARROW );
        arrowTutorial.SetActive( false );

        /*List<string> scenesToLoad = new List<string>();
        scenesToLoad.Add( SceneName.START_COUNTDOWN_TIMER_UI_SCENE );
        yield return StartCoroutine( GeneralFunctions.LoadScenes( scenesToLoad ) );
        
        StartCountdownTimer startCountdownTimer = FindObjectOfType<StartCountdownTimer>();
        startCountdownTimer.DisableBestTimeReplay();*/
    }

    public void StartTutorialLevel()
    {
        tutorialCanvas.SetActive( false );
        tutorialTriggers.SetActive( false );
        GameObject arrowTutorial = GameObject.Find( CommonGameObjectsName.TUTORIAL_ARROW );
        arrowTutorial.SetActive( false );

        /*List<string> scenesToLoad = new List<string>();
        scenesToLoad.Add( SceneName.START_COUNTDOWN_TIMER_UI_SCENE );
        StartCoroutine( GeneralFunctions.LoadScenes( scenesToLoad ) );*/
    }
}
