using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePause;
    public event EventHandler OnGameUnpaused;

    private float countDownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 120f;
    private State state;
    private bool isGamePause = false;
    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }
    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
       
    }
    private void Start()
    {
        GameInput.Instance.OnPauseAction += Instance_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (state == State.WaitingToStart)
        {
            state = State.CountDownToStart;
            OnStateChanged?.Invoke(this, new EventArgs());
        }
    }

    private void Instance_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                
                break;
            case State.CountDownToStart:
                countDownToStartTimer -= Time.deltaTime;
                if (countDownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
        
    }
    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
    public bool IsCountDownStart()
    {
        return state == State.CountDownToStart;
    }
    public float GetCountDownTimer()
    {
        return countDownToStartTimer;
    }
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }
    public float GetGamePlayingTimerNormalized()
    {
        return (gamePlayingTimer / gamePlayingTimerMax);
    }
    public void TogglePauseGame()
    {
        
        if (!isGamePause)
        {
            Time.timeScale = 0f;
            OnGamePause?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
        isGamePause = !isGamePause;
    }
}
