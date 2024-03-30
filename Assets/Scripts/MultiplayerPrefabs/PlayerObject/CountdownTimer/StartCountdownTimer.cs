using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCountdownTimer : NetworkBehaviour {

    [Header( "Countdown Timer" )]
    [SerializeField] private TextMeshProUGUI countdownTimer;
    [SerializeField] private GameObject countDownTimerCanvas;
    [SerializeField] private PlayerGeneralFunctions playerGeneralFunctions;

    private int countdownTime = 3;
    private bool initiateBestTimeReplay = true;

    [ClientRpc]
    public void StartCountDownClientRpc()
    {
        if(!IsOwner) return;
        Debug.Log( "Executed on: " + OwnerClientId );
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        countDownTimerCanvas.SetActive( true );

        yield return new WaitForSeconds( 1f );

        while(countdownTime > 0)
        {

            countdownTimer.text = countdownTime.ToString();

            yield return new WaitForSeconds( 1f );

            countdownTime--;
        }

        countdownTimer.text = "GO";
        countdownTimer.color = Color.green;

        //recordLevelRun.StartRecording();
        playerGeneralFunctions.EnablePlayerMovement();

        //StartBots();

        yield return new WaitForSeconds( 1f );

        countDownTimerCanvas.SetActive( false );
    }

    private void StartBots()
    {
        //Start the bots that race against you
        BotObject[] botObjects = FindObjectsByType<BotObject>( FindObjectsSortMode.None);

        for(int i = 0; i < botObjects.Length; i++)
        {
            botObjects[i].SetDestroyOnEnd();
            botObjects[i].StartBotReplay();
        }
    }

    private List<BotObject> GetAllBots()
    {
        BotGenerator botGenerator = FindObjectOfType<BotGenerator>();
        int childCount = botGenerator.transform.childCount;
        List<BotObject> childObjects = new List<BotObject>();

        for( int i = 0; i < childCount; i++ )
        {
            BotObject child = botGenerator.transform.GetChild( i ).gameObject.GetComponent<BotObject>();
            childObjects.Add( child );
        }

        return childObjects;
    }

    public void InitiateBestTimeReplay()
    {
        initiateBestTimeReplay = true;
    }

    public void DisableBestTimeReplay() 
    { 
        initiateBestTimeReplay = false; 
    }
}
