using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TutorialDoubleJumpWallRunHelper : MonoBehaviour
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
                yield return StartCoroutine( dialogueManager.StartDialogue( DialogueText.TUTORIAL_DOUBLE_JUMP_WALRUN_HELPER ) );

                BotGenerator botGenerator = FindObjectOfType<BotGenerator>();
                botGenerator.PlayCustomRecording( CustomRecordingsNames.TUTORIAL_SCENE_TUTORIAL_DOUBLE_JUMP_WALLRUN_HELPER, TriggerFailWallRun );
                id = other.transform.root.GetInstanceID();
                alreadyTriggered = true;
            }
        }
    }

    private void ResumePlay()
    {
        RecordLevelRun recordLevelRun = FindObjectOfType<RecordLevelRun>();
        recordLevelRun.StartRecording();
        GeneralFunctions.EnableAllPlayersAndBotsMovement();
    }

    private void TriggerFailWallRun()
    {
        StartCoroutine( FailWallRun() );
    }

    private IEnumerator FailWallRun()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        yield return StartCoroutine( dialogueManager.StartDialogue( DialogueText.TUTORIAL_DOUBLE_JUMP_WALRUN_FAIL_HELPER ) );

        BotGenerator botGenerator = FindObjectOfType<BotGenerator>();
        botGenerator.PlayCustomRecording( CustomRecordingsNames.TUTORIAL_SCENE_TUTORIAL_DOUBLE_JUMP_WALLRUN_FAIL_HELPER, ResumePlay );
        alreadyTriggered = true;
    }
}
