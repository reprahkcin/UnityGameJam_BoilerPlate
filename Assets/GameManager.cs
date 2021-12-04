using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private GameClock _gameClock;
    private CanvasManager _canvasManager;
    private AudioManager _audioManager;
    private HUD _hud;

    private int _score;
    [SerializeField] private int playerLives;
    [SerializeField] private int playerHealth;
    [SerializeField] private int playerMaxHealth;

    public bool hasWonGame;

    #region Unity Methods
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _gameClock = GetComponent<GameClock>();
        _canvasManager = GetComponent<CanvasManager>();
        _audioManager = GetComponent<AudioManager>();
        _hud = FindObjectOfType<HUD>();
    }

    private void Update()
    {
        // if (_playerHealth > 0) return;
        // if (_playerLives > 0)
        // {
        //     _playerLives--;
        //     _playerHealth = playerMaxHealth;
        //     _hud.UpdateLives(_playerLives);
        //     _hud.UpdateHealth(_playerHealth);
        // }
        // else
        // {
        //     GameOver(false);
        // }
    }
    #endregion

    public void AddToScore(int score)
    {
        _score += score;
        _hud.UpdateScore(_score);
    }

    private void ResetScore()
    {
        _score = 0;
        _hud.UpdateScore(_score);
    }

    public void AdjustHealth(int adjustment)
    {
        playerHealth += adjustment;
        if (playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }
        else if (playerHealth < 0)
        {
            playerHealth = 0;
        }
        _hud.UpdateHealth(playerHealth);
    }

    public void StartGame()
    {
        _canvasManager.TurnOnHUD();
        _gameClock.StartClock();
        _hud.UpdateHealth(playerHealth);
        _hud.UpdateLives(playerLives);
        ResetScore();
    }

    private void GameOver(bool hasWon)
    {
        _canvasManager.ShowCanvas(3, hasWon);
        _gameClock.StopClock();
    }

    public void RestartGame()
    {
        _canvasManager.TurnOnStartPage();
        _gameClock.ResetTime();
        ResetScore();
    }
}
