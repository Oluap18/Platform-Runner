using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{

    private PlayerInputActions playerInputActions;
    private int invertedCamera;
    private float cameraSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        playerInputActions = new PlayerInputActions();
        invertedCamera = 1;
        cameraSensitivity = 1;
    }

    public PlayerInputActions GetPlayerInputActions()
    {
        return playerInputActions;
    }
    public void SetPlayerInputActions( PlayerInputActions playerInputActions )
    {
        this.playerInputActions = playerInputActions;
    }

    public int GetInvertedCamera()
    {
        return invertedCamera;
    }

    public float GetCameraSensitivity()
    {
        return cameraSensitivity;
    }

    public void SetInvertedCamera( int invertedCamera )
    {
        this.invertedCamera = invertedCamera;
    }

    public void SetCameraSensitivity( float cameraSensitivity )
    {
        this.cameraSensitivity = cameraSensitivity;
    }
}
