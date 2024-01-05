using UnityEngine;

public class FinishLineScript : MonoBehaviour {

    private FinishLineManager finishLineManager;

    private void OnTriggerEnter( Collider other )
    {
        finishLineManager = FindObjectOfType<FinishLineManager>();
        finishLineManager.TriggerFinishLine(this.gameObject.scene.name);
    }

}
