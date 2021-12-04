using System;
using UnityEngine;
using TMPro;
using System.Collections;

public class GameClock : MonoBehaviour
{
    [Header("Set the Game Clock")]
    private int _time;
    public bool isRunning;
    [Range(10, 240)]
    public int levelTimeInSeconds;
    public TextMeshProUGUI timeText;

    private string TimeString()
    {
        int minutes = _time / 60;
        int seconds = _time % 60;
        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    private void Start()
    {
        _time = levelTimeInSeconds;
    }

    public void StartClock()
    {
        isRunning = true;
        CountdownClock();
    }

    public void StopClock()
    {
        isRunning = false;
    }

    public void CountdownClock()
    {
        if (_time > 0)
        {
            _time--;
            StartCoroutine(SecondDelay());
        }
        timeText.text = TimeString();
    }

    IEnumerator SecondDelay()
    {
        yield return new WaitForSeconds(1);
        CountdownClock();
    }

    public void SetClock(int time)
    {
        this._time = time;
    }

    public int GetTimeInSeconds()
    {
        return _time;
    }

    public void ResetTime()
    {
        _time = levelTimeInSeconds;
    }


}
