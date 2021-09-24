using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    // Singleton
    public static CanvasManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        UpdateHealthBar();
        UpdateScore();
        UpdateTime();
    }

    // --------------------------------------------------
    // HUD - UI Text Readouts
    // --------------------------------------------------


    public TextMeshProUGUI pointsText;

    // Update points in HUD
    public void UpdateScore() => pointsText.text = "Score: " + GameManager.instance.score;


    // time
    public TextMeshProUGUI timeText;

    // Update time in HUD
    public void UpdateTime() => timeText.text = "Time: " + GameManager.instance.time;

    // feedback
    public TextMeshProUGUI feedbackText;

    public void AddPoints(int points) => GameManager.instance.score += points;

    public void SetFeedback(string feedback, float duration)
    {
        feedbackText.text = feedback;
        StartCoroutine(ClearFeedback(duration));
    }

    IEnumerator ClearFeedback(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        feedbackText.text = "";
    }

    // Health Bar
    public Image healthBar;

    // Update Health Bar in HUD
    public void UpdateHealthBar()
    {
        // Get Player health
        float health = GameManager.instance.playerHealth / 100;

        // if health is less than 0, set health to 0
        if (health < 0)
        {
            health = 0;
        }

        // if health is greater than 1, set health to 1
        if (health > 1)
        {
            health = 1;
        }

        // Set health bar to health
        healthBar.transform.localScale = new Vector3(health, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

    // --------------------------------------------------
    // HUD - UI Buttons
    // --------------------------------------------------
    // TODO: Navigation buttons, start, exit, etc.
    // pause button
    public Button pauseButton;

    // Update pause button in HUD
    public void UpdatePauseButton()
    {
        if (GameManager.instance.gamePaused)
        {
            pauseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Resume";
        }
        else
        {
            pauseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Pause";
        }
    }


    // --------------------------------------------------
    // HUD - UI Panels
    // --------------------------------------------------

    public GameObject[] panels;

    // Intro Panel
    public GameObject introPanel;

    // Lose Panel
    public GameObject losePanel;

    // Win Panel
    public GameObject winPanel;

    // HUD Panel
    public GameObject hudPanel;

    // Pause/Instructions Panel
    public GameObject pausePanel;

    // Deactivate all panels
    // TODO: Load all panels into array, even specific panels above
    public void DeactivateAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }

    // Activate Intro Panel
    public void ActivateIntroPanel()
    {
        DeactivateAllPanels();
        introPanel.SetActive(true);
    }

    // Activate Lose Panel
    public void ActivateLosePanel()
    {
        DeactivateAllPanels();
        losePanel.SetActive(true);
    }

    // Activate Win Panel
    public void ActivateWinPanel()
    {
        DeactivateAllPanels();
        winPanel.SetActive(true);
    }

    // Activate HUD Panel
    public void ActivateHUDPanel()
    {
        DeactivateAllPanels();
        hudPanel.SetActive(true);
    }

    // Activate Pause Panel
    public void ActivatePausePanel() => pausePanel.SetActive(true);

    // Deactivate Pause Panel
    public void DeactivatePausePanel() => pausePanel.SetActive(false);

}
