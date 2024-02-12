using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; set; }

    private AudioSource audioSource;
    private float volumeLevel = 0.5f;
    private const string PLAYER_PREFS_MUSÝC_LEVEL_VOLUME = "MusicLevelVolume";
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSÝC_LEVEL_VOLUME, 0.5f);
        volumeLevel = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSÝC_LEVEL_VOLUME, 0.5f);
    }
    public void ChangeVolume()
    {
        volumeLevel += 0.1f;
        if (volumeLevel > 1f)
        {
            volumeLevel = 0;
        }
        audioSource.volume = volumeLevel;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSÝC_LEVEL_VOLUME,volumeLevel);
        PlayerPrefs.Save();
    }
    public float GetVolume()
    {
        return volumeLevel;
    }
}
