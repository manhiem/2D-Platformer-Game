using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score;

    public Animator keyAnimator;

    private void Awake()
    {
        keyAnimator = GetComponent<Animator>();
        score = 0;
        IncrementScore();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<PlayerController>() != null)
        {
            score += 10;
            IncrementScore();
            StartCoroutine(KeyDestroy());
        }
    }

    private void IncrementScore()
    {
        scoreText.text = "Score: " + score;
    }

    IEnumerator KeyDestroy()
    {
        keyAnimator.Play("FadeOut");
        yield return new WaitForSeconds(0.800f);
        Destroy(gameObject);
    }
}