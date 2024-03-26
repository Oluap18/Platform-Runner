using UnityEngine;

public class FinishLineScript : MonoBehaviour {

    //private FinishLineManager finishLineManager;

    private void OnTriggerEnter( Collider other )
    {

        if(LocalVariablesScript.replayOnly)
        {
            /*timerController.StopTimer();

            BotObject botObject = FindAnyObjectByType<BotObject>();
            if(RecordPlayerRun.replay)
            {
                timerController.SetCurrentTime( botObject.GetTime() );
            }
            if(botObject != null)
            {
                botObject.StopBotReplay();
            }*/
        }

        if(other.tag == CommonGameObjectsTags.PLAYER_TAG)
        {
            GeneralFunctions.TriggerPlayerFinishLineManager();
        }
        if(other.tag == CommonGameObjectsTags.BOT_TAG)
        {
            /*BotObject botObject = FindObjectOfType<BotObject>();
            if(botObject != null)
            {
                botObject.StopBotReplay();
            }*/
        }
    }


}
