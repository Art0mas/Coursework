using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudioChange : MonoBehaviour
{
    [SerializeField] private AudioClip[] tracks;

    private AudioSource audioSource;
    private int currentAudioIndex = 0; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (tracks.Length > 0)
            PlayTrack(currentAudioIndex);
        
    }

    void Update()
    {
        if (!audioSource.isPlaying && tracks.Length > 0)
            PlayNextTrack();
    }

    private void PlayTrack(int index)
    {
        audioSource.clip = tracks[index];
        audioSource.Play();
    }

    private void PlayNextTrack()
    {
        currentAudioIndex++;

        if (currentAudioIndex >= tracks.Length)
            currentAudioIndex = 0;

        PlayTrack(currentAudioIndex);
    }
}
