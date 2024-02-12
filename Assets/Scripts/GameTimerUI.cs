using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerUI : MonoBehaviour
{

    [SerializeField] private Image timerImage;
    [SerializeField] private Transform gameTimerUI;

    private void Update()
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            timerImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();
        }
        else if (GameManager.Instance.IsGameOver())
        {
            gameTimerUI.gameObject.SetActive(false);
        }
    }
}
