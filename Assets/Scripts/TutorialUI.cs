using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MoveUp;
    [SerializeField] private TextMeshProUGUI MoveDown;
    [SerializeField] private TextMeshProUGUI MoveRight;
    [SerializeField] private TextMeshProUGUI MoveLeft;
    [SerializeField] private TextMeshProUGUI Interact;
    [SerializeField] private TextMeshProUGUI InteracatAlt;
    [SerializeField] private TextMeshProUGUI Pause;

    private void Start()
    {
        Show();
        UpdateVisual();
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountDownStart())
        {
            Hide();
        }
    }

    private void GameInput_OnBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        MoveUp.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        MoveDown.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        MoveRight.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        MoveLeft.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        Interact.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        InteracatAlt.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlt);
        Pause.text = (GameInput.Instance.GetBindingText(GameInput.Binding.Pause).Length > 3) ? GameInput.Instance.GetBindingText(GameInput.Binding.Pause).Substring(0, 3) : GameInput.Instance.GetBindingText(GameInput.Binding.Pause); ;
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
