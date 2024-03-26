using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{

    [Header( "References" )]
    [SerializeField] private GameObject networkCanvas;

    public void ServerButton()
    {
        NetworkManager.Singleton.StartServer();
    }

    public void HostButton()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void ClientButton()
    {
        NetworkManager.Singleton.StartClient();
    }

    public void SinglePlayerMode()
    {
        networkCanvas.SetActive( false );
    }

    public void MultiPlayerMode()
    {
        networkCanvas.SetActive( true );
    }


}
