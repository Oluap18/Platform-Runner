using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TutorialMultipleWallRunHelper : MonoBehaviour
{
    private bool alreadyTriggered = false;
    private int id;

    private IEnumerator OnTriggerEnter( Collider other )
    {
        if(!alreadyTriggered)
        {
            if(other.tag == CommonGameObjectsTags.PLAYER_TAG)
            {
                RecordLevelRun recordLevelRun = FindObjectOfType<RecordLevelRun>();
                recordLevelRun.StopRecording();

                DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
                yield return StartCoroutine( dialogueManager.StartDialogue( DialogueText.TUTORIAL_MULTIPLE_WALLRUN_HELPER ) );

                BotGenerator botGenerator = FindObjectOfType<BotGenerator>();
                botGenerator.PlayCustomRecording( CustomRecordingsNames.TUTORIAL_SCENE_TUTORIAL_MULTIPLE_WALLRUN_HELPER, ResumePlay );
                id = other.transform.root.GetInstanceID();
                alreadyTriggered = true;
            }
        }
    }

    private void ResumePlay()
    {
        RecordLevelRun recordLevelRun = FindObjectOfType<RecordLevelRun>();
        recordLevelRun.StartRecording();
        GeneralFunctions.EnableAllPlayersMovement();
    }
}