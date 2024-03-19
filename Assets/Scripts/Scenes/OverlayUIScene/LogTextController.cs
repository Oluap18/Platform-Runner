using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class LogTextController : MonoBehaviour
{

    public void AddTextToLog(string text )
    {
        TextMeshProUGUI logTextController = GameObject.Find( CommonGameObjectsName.LOG_TEXT_OVERLAYUI_SCENE ).GetComponent<TextMeshProUGUI>();
        string logTextString = logTextController.text + text + "\n";
        string newstring = logTextString;
        if(logTextString.Split( '\n' ).Length > 10)
        {
            newstring = "";
            string[] splitstrings = logTextString.Split( '\n' );
            int iterator = 1;
            
            while( iterator < 10)
            {
                newstring += splitstrings[iterator++] + " . Iterator: " + iterator + "\n";
            }
        }

        logTextController.text = newstring;
    }

}
