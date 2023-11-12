using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineManager : MonoBehaviour
{
    private TimerController timerController;
    private PlayerBasicMovement playerBasicMovement;

    public void TriggerFinishLine()
    {
        playerBasicMovement = FindObjectOfType<PlayerBasicMovement>();
        timerController = FindObjectOfType<TimerController>();
        timerController.StopTimer();
        playerBasicMovement.DisablePlayerMovement();
    }
}
