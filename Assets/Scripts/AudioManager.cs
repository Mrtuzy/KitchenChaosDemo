using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_LEVEL_VOLUME = "SoundLevelVolume";
    public static AudioManager Instance { get; set; }
    [SerializeField] private AuidoClipRefsSO auidoClipRefsSO;
    private float volumeLevel = 0.1f;
    private void Awake()
    {
        Instance = this;
        volumeLevel = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_LEVEL_VOLUME, 0.1f);
    }
    private void Start()
    {
        DeliveryManager.Instance.OnDeliveryCorrect += DeliveryManager_OnDeliveryCorrect;
        DeliveryManager.Instance.OnDeliveryWrong += DeliveryManager_OnDeliveryWrong;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.OnPickSomething += Player_OnPickSomething;
        BaseCounter.OnAnyDropSomthing += BaseCounter_OnDropSomthing;
        TrashCounter.OnAnyTrashed += TrashCounter_OnTrashed;
    }

    private void TrashCounter_OnTrashed(object sender, System.EventArgs e)
    {
        PlayAuidoClip(auidoClipRefsSO.trash, Camera.main.transform.position);
    }

    private void BaseCounter_OnDropSomthing(object sender, System.EventArgs e)
    {
        PlayAuidoClip(auidoClipRefsSO.objectDrop, Camera.main.transform.position);
    }

    private void Player_OnPickSomething(object sender, System.EventArgs e)
    {
        PlayAuidoClip(auidoClipRefsSO.objectPickUp, Camera.main.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        PlayAuidoClip(auidoClipRefsSO.chop, Camera.main.transform.position);
    }

    private void DeliveryManager_OnDeliveryWrong(object sender, System.EventArgs e)
    {
        PlayAuidoClip(auidoClipRefsSO.deliveryWrong,Camera.main.transform.position);
    }

    private void DeliveryManager_OnDeliveryCorrect(object sender, System.EventArgs e)
    {
        PlayAuidoClip(auidoClipRefsSO.deliveryCorrect, Camera.main.transform.position);
    }

    private void PlayAuidoClip(AudioClip[] audioClip, Vector3 position, float volume = 1f)
    {
        AudioClip audioClipRand =audioClip[Random.Range(0,audioClip.Length)]; 
        PlayAuidoClip(audioClipRand,position,volume);
    }
    private void PlayAuidoClip(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip,position,volume * volumeLevel);
    }
    public void PlayFootSteps( Vector3 position, float volume = 1f)
    {
        AudioClip footStepsRand = auidoClipRefsSO.footStep[Random.Range(0, auidoClipRefsSO.footStep.Length)];
        PlayAuidoClip(footStepsRand, position, volume);
    }


    public void ChangeVolume()
    {
        volumeLevel += 0.1f;
        if (volumeLevel > 1f)
        {
            volumeLevel = 0;
        }
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_LEVEL_VOLUME,volumeLevel);
        PlayerPrefs.Save();
    }
    public float GetVolume()
    {
        return volumeLevel;
    }
}
