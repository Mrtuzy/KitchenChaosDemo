using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterAudio : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
       
    }
    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool soundPlay = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;

        if (soundPlay)
        {
            audioSource.Play();
            
        }
        else
        {
            audioSource.Pause();
         
        }
    }
}
