using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public LobbyManager lobbyManager;

    private void Start()
    {
        lobbyManager = FindObjectOfType<LobbyManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<PlayerController>() != null)
        {
            lobbyManager.SetLevelUnlocked();
        }
    }
}
