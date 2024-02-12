using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisuals : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] selectedVisualArray;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter )
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Show()
    {
        foreach (GameObject visual in selectedVisualArray)
        {
             visual.SetActive(true);
        }
       
    }
    private void Hide()
    {
        foreach (GameObject visual in selectedVisualArray)
        {
            visual.SetActive(false);
        }
        
    }
}
