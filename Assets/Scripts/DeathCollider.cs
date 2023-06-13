using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    public GameObject Player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Player Died!");
            Instantiate(Player, new Vector2(0f, 0f), Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
}
