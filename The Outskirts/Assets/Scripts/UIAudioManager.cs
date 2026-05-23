using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClickSound(AudioClip clipToPlay)
    {
        if (clipToPlay != null)
        {
            audioSource.PlayOneShot(clipToPlay);
        }
    }
}
