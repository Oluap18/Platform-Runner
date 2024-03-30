using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiplayerMenu : MonoBehaviour
{
    public void ServerButton()
    {
        NetworkManager.Singleton.StartServer();
        SceneManager.UnloadSceneAsync( SceneName.NETCODE_FOR_GAMEOBJECTS_SCENE );
    }

    public void HostButton()
    {
        NetworkManager.Singleton.StartHost();
        SceneManager.UnloadSceneAsync( SceneName.NETCODE_FOR_GAMEOBJECTS_SCENE );
    }

    public void ClientButton()
    {
        NetworkManager.Singleton.StartClient();
        SceneManager.UnloadSceneAsync( SceneName.NETCODE_FOR_GAMEOBJECTS_SCENE );
    }
}
