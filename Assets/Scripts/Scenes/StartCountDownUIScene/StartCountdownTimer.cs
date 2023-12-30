using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCountdownTimer : MonoBehaviour {
    [Header( "Countdown Timer" )]
    [SerializeField] private TextMeshProUGUI countdownTimer;

    private int countdownTime = 3;
    private PlayerBasicMovement playerBasicMovement;
    private TimerController timerController;

    IEnumerator Start()
    {
        playerBasicMovement = FindObjectOfType<PlayerBasicMovement>();
        timerController = FindObjectOfType<TimerController>();

        yield return new WaitForSeconds( 1f );

        while(countdownTime > 0) {

            countdownTimer.text = countdownTime.ToString();

            yield return new WaitForSeconds( 1f );

            countdownTime--;
        }

        countdownTimer.text = "GO";
        countdownTimer.color = Color.green;
        playerBasicMovement.EnablePlayerMovement();
        timerController.StartTimer();

        yield return new WaitForSeconds( 1f );

        SceneManager.UnloadSceneAsync( SceneName.START_COUNTDOWN_TIMER_UI_SCENE );
    }
}
