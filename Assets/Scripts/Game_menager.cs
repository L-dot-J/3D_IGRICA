using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_menager : MonoBehaviour
{

    public static Game_menager Instance { get; private set; }

    public event EventHandler OnStateChange;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private enum State
    { 
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float countdownToStartTimer = 3f;
    private float gamePlayingToStartTimer;
    private float gamePlayingToStartTimerMax = 20f;
    private bool isGamePaused = false;



    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }
    private void Start()
    {
        GameInput.Instance.OnPuaseAction += GameInput_OnPuaseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (state == State.WaitingToStart)
        { 
            state = State.CountdownToStart;
            OnStateChange?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPuaseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (state)
        { 
            case State.WaitingToStart:
               
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingToStartTimer = gamePlayingToStartTimerMax;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingToStartTimer -= Time.deltaTime;
                if (gamePlayingToStartTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }

        //UnityEngine.Debug.Log(state);
    }
    public bool IsGamePlaying()
    { 
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }
    public float GetCountdownToStartTimer()
    { 
        return countdownToStartTimer;
    }
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }
    public float GetPlayTimerNormalized()
    {
        return 1 - (gamePlayingToStartTimer / gamePlayingToStartTimerMax);
    }
    public void TogglePauseGame()
    { 
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
        
    }
}
