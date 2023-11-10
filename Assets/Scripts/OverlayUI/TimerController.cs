using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{

    private float currentTime = 0f;
    private bool startTimer = false;

    [Header ("Timer")]
    [SerializeField] private TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer) {

            currentTime += Time.deltaTime;
            timerText.text = FormatTimer(currentTime);

        }
        
    }

    private string FormatTimer (float timer )
    {
        int minutes = Mathf.FloorToInt( timer / 60F );
        int seconds = Mathf.FloorToInt( timer - minutes * 60 );
        int miliseconds = Mathf.FloorToInt( (timer - minutes * 60 - seconds) * 100 );

        string niceTime = string.Format( "{0:00}:{1:00}:{2:00}", minutes, seconds, miliseconds );

        return niceTime;
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
}
