using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    public static GamePauseUI Instance { get; private set; }
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    private void Awake()
    {
        Instance = this;
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.LoadScene(Loader.Scene.MainMenuScene);
        });

        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePauseGame();
        });
        optionsButton.onClick.AddListener(() =>
        {
            OptionsUI.Instance.Show();
            Hide();
        });
    }
    private void Start()
    {
        GameManager.Instance.OnGamePause += GameManager_OnGamePause;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        Hide();
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGamePause(object sender, System.EventArgs e)
    {
        Show();
        
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
