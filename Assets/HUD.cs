using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    private static HUD _instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI livesText;
    public float feedbackDuration = 1f;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateHealth(int health)
    {
        healthText.text = "Health: " + health;
    }

    private void UpdateTime(float time)
    {
        timeText.text = "Time: " + time;
    }

    public void UpdateLives(int lives)
    {
        livesText.text = "Lives: " + lives;
    }

    public void PostFeedback(string feedback)
    {
        feedbackText.text = feedback;
        StartCoroutine(ClearFeedback());
    }

    private IEnumerator ClearFeedback()
    {
        yield return new WaitForSeconds(feedbackDuration);
        feedbackText.text = "";
    }

}
