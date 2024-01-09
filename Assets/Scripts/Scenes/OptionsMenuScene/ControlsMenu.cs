using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenu : MonoBehaviour
{

    [Header( "PauseMenuReferences" )]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject controlsMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ForwardButton()
    {

    }

    public void BackwardsButton()
    {

    }

    public void RightButton()
    {

    }

    public void LeftButton()
    {

    }

    public void JumpButton()
    {

    }

    public void CameraButton()
    {

    }

    public void ExitButton()
    {

        controlsMenu.SetActive( false );
        pauseMenu.SetActive( true );

    }
}
