using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class CorrectWrongUI : MonoBehaviour
{
    [SerializeField] private Image correctImage;
    [SerializeField] private Image wrongImage;

    private float correctTimer ;
    private float correctTimerMax = 0.5f;
    private float wrongTimer;
    private float wrongTimerMax = 0.5f;
    private bool showCorrect = false;
    private bool showWrong = false;

    private void Start()
    {
        DeliveryManager.Instance.OnDeliveryCorrect += DeliveryManager_OnDeliveryCorrect;
        DeliveryManager.Instance.OnDeliveryWrong += DeliveryManager_OnDeliveryWrong;
        Hide(correctImage);
        Hide(wrongImage);
        correctTimer = correctTimerMax;
        wrongTimer = wrongTimerMax;
    }

    private void DeliveryManager_OnDeliveryWrong(object sender, System.EventArgs e)
    {
        Show(wrongImage);
        showWrong = true;
    }

    private void DeliveryManager_OnDeliveryCorrect(object sender, System.EventArgs e)
    {
        Show(correctImage);
        showCorrect = true;
       
    }
    private void Update()
    {
        if (showCorrect)
        {
            correctTimer -= Time.deltaTime;
            if (correctTimer < 0)
            {
                correctTimer = correctTimerMax;
                Hide(correctImage);
                showCorrect = false;
               
            }
        }
        if (showWrong)
        {
            wrongTimer -= Time.deltaTime;
            if (wrongTimer < 0)
            {
                wrongTimer = wrongTimerMax;
                Hide(wrongImage);
                showWrong = false;

            }
        }
    }
    private void Show(Image ýmage)
    {
        ýmage.gameObject.SetActive(true);
    }
    private void Hide(Image ýmage)
    {
        ýmage.gameObject.SetActive(false);
    }
}
