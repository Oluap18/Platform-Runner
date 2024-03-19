using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Animator animator;

    private Queue<string> sentences;
    private bool nextMessage;

    private const string IS_OPEN = "IsOpen";

    private void Awake()
    {
        animator.SetBool( IS_OPEN, false );
        sentences = new Queue<string>();
        nextMessage = false;
    }

    public IEnumerator StartDialogue( string[] dialogue )
    {
        animator.SetBool( IS_OPEN, true );
        sentences.Clear();
        GeneralFunctions.DisableMovementOfPlayer();
        
        foreach( string sentence in dialogue )
        {
            sentences.Enqueue( sentence );
        }
        nextMessage = true;
        
        while( sentences.Count > 0)
        {
            yield return StartCoroutine( DisplayNextSentence() );
        }

        //Wait for the last "Continue"
        while(!nextMessage){
            yield return null;
        }
        EndDialogue();
    }

    private IEnumerator DisplayNextSentence()
    {
        //Wait for the "Continue" to display the message
        if(nextMessage)
        {
            nextMessage = false;
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            yield return StartCoroutine( TypeSentence( sentence ) );
        }
    }

    private IEnumerator TypeSentence ( string sentence )
    {
        dialogueText.text = "";
        foreach( char letter in sentence.ToCharArray() ) {
            dialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        //Wait for the final "Continue" of the dialogue
        if(nextMessage){
            animator.SetBool( IS_OPEN, false );
            GeneralFunctions.RemoveCursor();
        }
    }

    public void TriggerNextMessage()
    {
        nextMessage = true;
    }
}
