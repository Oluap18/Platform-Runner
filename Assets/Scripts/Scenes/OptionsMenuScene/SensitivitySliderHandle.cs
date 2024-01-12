using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SensitivitySliderHandle : MonoBehaviour
{

    [SerializeField] private Slider slider;
    [SerializeField] private TMP_InputField sensitivityInput;

    private float lastsensitivityInputValue;
    private PlayerInputManager playerInputManager;

    // Start is called before the first frame update
    void Start()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();

        float sensitivity = playerInputManager.GetCameraSensitivity();

        slider.value = sensitivity;
        sensitivityInput.text = sensitivity.ToString();

        lastsensitivityInputValue = sensitivity;

        OnValueChangedSlider();

        OnValueChangedTextInput();

    }

    private void OnValueChangedSlider()
    {
        slider.onValueChanged.AddListener( s => {

            lastsensitivityInputValue = System.MathF.Round( s, 2 );
            sensitivityInput.text = lastsensitivityInputValue.ToString();
            playerInputManager.SetCameraSensitivity( lastsensitivityInputValue );

        } );
    }
    private void OnValueChangedTextInput()
    {
        sensitivityInput.onEndEdit.AddListener( s => {
            try {

                float inputValue = System.MathF.Round( float.Parse( s ), 2 );

                if(inputValue > slider.maxValue) {
                    inputValue = System.MathF.Round( slider.maxValue, 2 );
                }

                slider.value = inputValue;
                sensitivityInput.text = inputValue.ToString();
                playerInputManager.SetCameraSensitivity( inputValue );
                lastsensitivityInputValue = inputValue;
            }
            catch(Exception e) {
                Debug.LogError( e.ToString() );
                sensitivityInput.text = lastsensitivityInputValue.ToString();
            }

        } );
    }
}
