using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    

    [SerializeField] private Player player;
    [SerializeField] private AuidoClipRefsSO auidoClipRefsSO;
    private float stepTimer;
    private float stepTimerMax = 0.2f;
    private float volume = 0.1f;
    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        stepTimer -= Time.deltaTime;
        if (stepTimer < 0)
        {
            stepTimer = stepTimerMax;

            if (player.IsWalking())
            {
                AudioManager.Instance.PlayFootSteps(Camera.main.transform.position, volume);
            }
        }
        
    }

}
