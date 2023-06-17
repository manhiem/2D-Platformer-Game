using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    //public LobbyManager lobbyManager;
    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
