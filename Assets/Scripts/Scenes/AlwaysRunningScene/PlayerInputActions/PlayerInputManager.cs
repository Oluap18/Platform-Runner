using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{

    private PlayerInputActions playerInputActions;

    // Start is called before the first frame update
    void Start()
    {
        playerInputActions = new PlayerInputActions();
    }

    public PlayerInputActions getPlayerInputActions()
    {
        return playerInputActions;
    }
}
