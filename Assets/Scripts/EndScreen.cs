using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    //public LobbyManager lobbyManager;
    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        LobbyManager.Instance.StartLevel(scene.buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
