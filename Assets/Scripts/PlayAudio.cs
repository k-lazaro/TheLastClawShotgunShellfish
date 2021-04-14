using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] clips;

    void Start()
    {
        audioSource.PlayOneShot(randomClip(), Random.Range(0.5f, 1f));
    }

    AudioClip randomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }
}
