using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRestart : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private RestartManager restartManager;

    // Start is called before the first frame update
    void Awake()
    {
        playerInputActions = FindObjectOfType<PlayerInputManager>().GetPlayerInputActions();

        restartManager = FindObjectOfType<RestartManager>();
    }

    private void OnEnable()
    {
        playerInputActions.PlayerMovement.Restart.performed += Restart;
    }

    private void OnDisable()
    {
        playerInputActions.PlayerMovement.Restart.performed -= Restart;
    }

    private void Restart( InputAction.CallbackContext obj )
    {
        restartManager.Restart();

    }
}
