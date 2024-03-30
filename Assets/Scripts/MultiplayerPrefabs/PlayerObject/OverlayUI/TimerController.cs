using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour {

    private float currentTime = 0f;
    private bool startTimer = false;

    [Header( "Timer" )]
    [SerializeField] private TextMeshProUGUI timerText;

    // Update is called once per frame
    void Update()
    {
        timerText.text = "Time: " + GeneralFunctions.FormatTimer( currentTime );
        
        if(startTimer) {
            
            currentTime += Time.deltaTime;
            
        }

    }

    public void StartTimer()
    {
        startTimer = true;
    }

    public void StopTimer()
    {
        startTimer = false;
    }

    public void ResetTimer()
    {
        currentTime = 0f;
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }

    public void SetCurrentTime(float currentTime)
    {
        this.currentTime = currentTime;
    }
}
