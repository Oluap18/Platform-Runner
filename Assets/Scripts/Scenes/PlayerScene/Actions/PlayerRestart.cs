using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRestart : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private RestartManager restartManager;

    // Start is called before the first frame update
    void Start()
    {
        playerInputActions = FindObjectOfType<PlayerInputManager>().getPlayerInputActions();
        playerInputActions.PlayerMovement.Enable();
        playerInputActions.PlayerMovement.Restart.performed += Restart;

        restartManager = FindObjectOfType<RestartManager>();
    }

    private void Restart( InputAction.CallbackContext obj )
    {

        restartManager.Restart();

    }
}
