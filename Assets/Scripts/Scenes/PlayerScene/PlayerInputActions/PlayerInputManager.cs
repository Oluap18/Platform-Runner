using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
using System.Runtime.CompilerServices;

public class PlayerInputManager : NetworkBehaviour
{

    [Header( "References" )]
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private PlayerGeneralFunctions playerGeneralFunctions;

    private PlayerInputActions playerInputActions;
    private int invertedCamera;
    private float cameraSensitivity;
    private bool optionsMenuOpen;

    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        if(!IsOwner) return;
        optionsMenuOpen = false;
        playerInputActions = new PlayerInputActions();
        playerInputActions.PlayerMovement.Enable();
        invertedCamera = 1;
        cameraSensitivity = 1;
        SetupInitialBindings();
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

    private void SetupInitialBindings()
    {
        PlayerKeybindsStructure playerKeybindsStructure = CommonDataMethods.LoadData( CommonGameObjectsVariables.PLAYER_KEYBINDS_PATH, CommonGameObjectsVariables.PLAYER_KEYBINDS_FILENAME ) as PlayerKeybindsStructure;
        playerInputActions.LoadBindingOverridesFromJson( playerKeybindsStructure.playerInputActions );
        SetCameraSensitivity( playerKeybindsStructure.cameraSensitivity );
        SetInvertedCamera( playerKeybindsStructure.invertedCamera );
    }

    private void Start()
    {
        if(!IsOwner) return;
        playerInputActions.PlayerMovement.OptionsMenu.performed += OpenOptionsMenu;
    }

    public override void OnNetworkDespawn()
    {
        if(!IsOwner) return;
        playerInputActions.PlayerMovement.OptionsMenu.performed -= OpenOptionsMenu;
    }

    private void OpenOptionsMenu( InputAction.CallbackContext obj )
    {
        if(!IsOwner) return;
        if(!optionsMenuOpen)
        {
            playerGeneralFunctions.DisableMovementOfPlayer();
            canvas.SetActive(true);
            pauseMenu.SetActive(true);
            controlsMenu.SetActive( false );
            optionsMenuOpen = true;
        }
    }

    public void CloseOptionsMenu()
    {
        if(!IsOwner) return;
        if(optionsMenuOpen)
        {
            pauseMenu.SetActive( false );
            controlsMenu.SetActive( false );
            canvas.SetActive( false );
            playerGeneralFunctions.EnableMovementOfPlayer();
            optionsMenuOpen = false;
        }
    }

    public void OpenControlsMenu()
    {
        if(optionsMenuOpen)
        {
            canvas.SetActive( true );
            pauseMenu.SetActive( false );
            controlsMenu.SetActive( true );
        }
    }
}
