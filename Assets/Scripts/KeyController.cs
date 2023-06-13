using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score;

    private void Awake()
    {
        score = 0;
        IncrementScore();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<PlayerController>() != null)
        {
            score += 10;
            IncrementScore();
            Destroy(gameObject);
        }
    }

    private void IncrementScore()
    {
        scoreText.text = "Score: " + score;
    }
}