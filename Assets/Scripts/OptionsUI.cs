using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }
    [SerializeField] private Button soundEffectLevelButton;
    [SerializeField] private Button musicLevelButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI soundEffectLevelText;
    [SerializeField] private TextMeshProUGUI musicLevelText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private Transform pressKeyWindow;
    private void Awake()
    {
       
        Instance = this;
        soundEffectLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicLevelButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        exitButton.onClick.AddListener(() =>
        {
            Hide();
            GamePauseUI.Instance.Show();
        });
        moveUpButton.onClick.AddListener(() =>
        {
            RebindBindings(GameInput.Binding.Move_Up);
        });
        moveDownButton.onClick.AddListener(() =>
        {
            RebindBindings(GameInput.Binding.Move_Down);
        });
        moveRightButton.onClick.AddListener(() =>
        {
            RebindBindings(GameInput.Binding.Move_Right);
        });
        moveLeftButton.onClick.AddListener(() =>
        {
            RebindBindings(GameInput.Binding.Move_Left);
        });
        interactButton.onClick.AddListener(() =>
        {
            RebindBindings(GameInput.Binding.Interact);
        });
        interactAltButton.onClick.AddListener(() =>
        {
            RebindBindings(GameInput.Binding.InteractAlt);
        });
        pauseButton.onClick.AddListener(() =>
        {
            RebindBindings(GameInput.Binding.Pause);
        });
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        UpdateVisual();
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectLevelText.text = $"Sound Effect Level: {Math.Round(AudioManager.Instance.GetVolume()*10)}";
        musicLevelText.text = $"Music Level: {Math.Round(MusicManager.Instance.GetVolume()*10)}";
        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlt);
        pauseText.text = (GameInput.Instance.GetBindingText(GameInput.Binding.Pause).Length > 3) ? GameInput.Instance.GetBindingText(GameInput.Binding.Pause).Substring(0,3) : GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ShowPressKeyWindow()
    {
        pressKeyWindow.gameObject.SetActive(true);
    }
    public void HidePressKeyWindow()
    {
        pressKeyWindow.gameObject.SetActive(false);
    }
    private void RebindBindings(GameInput.Binding binding)
    {
        ShowPressKeyWindow();
        GameInput.Instance.RebindBinding(binding, () =>
        {
            HidePressKeyWindow();
            UpdateVisual();
        });
    }
}
