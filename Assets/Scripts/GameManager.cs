using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager instance;

    private void Awake()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Bring up Start/Intro Screen
        CanvasManager.instance.ActivateIntroPanel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        // if game is unpaused
        if (GameManager.instance.gamePaused == false && time > 0)
            time -= Time.deltaTime;
    }


    // --------------------------------------------------
    // Player
    // --------------------------------------------------

    // Player Health
    public float playerHealth = 100;

    // Player Damage
    public float playerDamage = 10;

    // Player Speed
    public float playerSpeed = 5f;

    public void AdjustPlayerHealth(int amount) => playerHealth += amount;

    // --------------------------------------------------
    // Game Variables
    // --------------------------------------------------

    // points
    public int score;

    // time/clock
    public float time;
    public void AdjustTime(float amount) => time += amount;

    // Current Level
    public int currentLevel;

    // --------------------------------------------------
    // Game States and Settings
    // --------------------------------------------------


    // TODO: Add Game States
    public bool gamePaused = true;

    public void ToggleGamePaused() => gamePaused = !gamePaused;

    public void StartGame()
    {
        gamePaused = false;
        // TODO: Any clocks or timers should be addressed here
        // TODO: 3...2...1...GO! animation
        CanvasManager.instance.ActivateHUDPanel();
        StartCoroutine(CountdownOverlay());
        Debug.Log("Game Started");
    }

    IEnumerator CountdownOverlay()
    {
        CanvasManager.instance.SetFeedback("3", 1.0f);
        yield return new WaitForSeconds(1.0f);
        CanvasManager.instance.SetFeedback("2", 1.0f);
        yield return new WaitForSeconds(1.0f);
        CanvasManager.instance.SetFeedback("1", 1.0f);
        yield return new WaitForSeconds(1.0f);
        CanvasManager.instance.SetFeedback("GO!", 1.0f);
        yield return new WaitForSeconds(1.0f);

    }
    public void LoseGame()
    {
        gamePaused = true;
        Debug.Log("You Lose!");
        CanvasManager.instance.SetFeedback("You Lose!", 3.0f);
        CanvasManager.instance.ActivateLosePanel();
    }

    public void WinGame()
    {
        gamePaused = true;
        Debug.Log("You Win!");
        CanvasManager.instance.SetFeedback("You Win!", 3.0f);
        CanvasManager.instance.ActivateWinPanel();
    }

    public void RestartGame()
    {
        // TODO: Reset all the parameters and values, but maybe keep the score?
        gamePaused = true;
        Debug.Log("Restarting Game");
        CanvasManager.instance.ActivateIntroPanel();
    }

    public void ResetComplete() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);





}
