using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;

public class CountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountDownStart())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Update()
    {
        countDownText.text = GameManager.Instance.GetCountDownTimer().ToString("#");
        
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
