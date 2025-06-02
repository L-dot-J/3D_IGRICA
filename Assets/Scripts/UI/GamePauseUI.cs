using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() => { 
            Game_menager.Instance.TogglePauseGame();
        });
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }
    private void Start()
    {
        Game_menager.Instance.OnGamePaused += GameMenager_OnGamePaused;
        Game_menager.Instance.OnGameUnpaused += GameMenager_OnGameUnpaused;

        Hide();
    }

    private void GameMenager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameMenager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    { 
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
