using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AuidoClipRefsSO : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] footStep;
    public AudioClip[] deliveryCorrect;
    public AudioClip[] deliveryWrong;
    public AudioClip[] objectDrop;
    public AudioClip[] objectPickUp;
    public AudioClip[] trash;
    public AudioClip[] warning;
    public AudioClip stoveSizzle;
}
