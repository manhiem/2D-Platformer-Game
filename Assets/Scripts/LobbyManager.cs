using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyManager : MonoBehaviour
{
    public string level;
    public static LobbyManager Instance { get; private set; }

    public GameObject levelPanel;
    public GameObject startPanel;

    //public TextMeshProUGUI restartBtn;
    //public TextMeshProUGUI quitBtn;
    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void setLevel(string levelName)
    {
        level = levelName;
        StartLevel(level);
    }

    public void RestartGame(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void StartLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void StartGame()
    {
        startPanel.SetActive(false);
        levelPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
