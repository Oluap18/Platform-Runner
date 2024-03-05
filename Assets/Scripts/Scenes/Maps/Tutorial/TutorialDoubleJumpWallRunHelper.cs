using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TutorialJumpWallRunHelper : MonoBehaviour
{
    private bool alreadyTriggered = false;
    private int id;

    private IEnumerator OnTriggerEnter( Collider other )
    {
        if(!alreadyTriggered)
        {
            if(other.tag == CommonGameObjectsTags.PLAYER_TAG)
            {
                DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
                yield return StartCoroutine( dialogueManager.StartDialogue( DialogueText.TUTORIAL_JUMP_WALRUN_HELPER ) );

                BotGenerator botGenerator = FindObjectOfType<BotGenerator>();
                botGenerator.PlayCustomRecording( CustomRecordingsNames.TUTORIAL_SCENE_TUTORIAL_JUMP_WALLRUN_HELPER, ResumePlay );
                id = other.transform.root.GetInstanceID();
                alreadyTriggered = true;
            }
        }
    }

    private void ResumePlay()
    {
        GeneralFunctions.EnableMovementOfPlayer(id);
    }
}
