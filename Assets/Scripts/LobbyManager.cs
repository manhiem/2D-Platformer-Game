using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    //public string level;
    public static LobbyManager Instance { get; private set; }

    public GameObject levelPanel;
    public GameObject startPanel;

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

    private void Start()
    {
        //Set all levels as Locked and Level1 as Unlocked
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("UnLockedLevels", 1);

        //Debug.Log(SceneManager.sceneCountInBuildSettings);
    }

    public void CheckLevelMode(int level)
    {
        int mode = PlayerPrefs.GetInt("UnLockedLevels");
        Debug.Log(level);
        Debug.Log(mode);
        if(mode == level)
        {
            StartLevel(level);
        }
        else
        {
            Debug.Log("Please Complete the earlier levels to unlock this!!");
        }
    }

    public void SetLevelUnlocked()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        PlayerPrefs.SetInt("UnLockedLevels", nextLevelIndex);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetInt("UnLockedLevels"));
        CheckLevelMode(nextLevelIndex);
    }


    public void StartLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void ShowLevelPanel()
    {
        startPanel.SetActive(false);
        levelPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
