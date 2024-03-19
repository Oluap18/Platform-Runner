using UnityEngine;

public class FinishLineScript : MonoBehaviour {

    private FinishLineManager finishLineManager;

    private void OnTriggerEnter( Collider other )
    {
        finishLineManager = FindObjectOfType<FinishLineManager>();

        if(RecordPlayerRun.replay)
        {
            finishLineManager.TriggerFinishLineReplay();
        }

        if(other.tag == CommonGameObjectsTags.PLAYER_TAG)
        {
            finishLineManager.TriggerFinishLine( this.gameObject.scene.name );
        }
        if(other.tag == CommonGameObjectsTags.BOT_TAG)
        {
            finishLineManager.TriggerFinishLineBot();
        }
    }


}
