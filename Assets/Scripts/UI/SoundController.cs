using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioClip backgroundClip;
    [SerializeField] private AudioClip clickClip;
    [SerializeField] private AudioClip correctClip;
    [SerializeField] private AudioClip wrongClip;
    [SerializeField] private AudioClip applauseClip;
    private AudioSource backgroundSound;
    private AudioSource clickSound;
    private AudioSource correctSound;
    private AudioSource wrongSound;
    private AudioSource applauseSound;
    private bool isMuted; // Mute status

    private void Start()
    {
        isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        backgroundSound = audioSources[0];
        clickSound = audioSources[1];
        if(audioSources.Length > 2){
            correctSound = audioSources[2];
            wrongSound = audioSources[3];
            applauseSound = audioSources[4];
        }

        // Set up and play background sound in a loop
        backgroundSound.clip = backgroundClip;
        backgroundSound.loop = true;
        backgroundSound.Play();
    }
    public void ClickSound()
    {
        clickSound.PlayOneShot(clickClip);
    }

    public void CorrectSound()
    {
        correctSound.PlayOneShot(correctClip);
    }

    public void WrongSound()
    {
        wrongSound.PlayOneShot(wrongClip);
    }

    public void ApplauseSound()
    {
        applauseSound.PlayOneShot(applauseClip);
    }

    public void ToggleMute()
    {
        isMuted = !isMuted; // Toggle mute status

        // Set mute status for all audio sources
        backgroundSound.mute = isMuted;
        if (correctSound != null)
            correctSound.mute = isMuted;
        if (wrongSound != null)
            wrongSound.mute = isMuted;
        if (applauseSound != null)
            applauseSound.mute = isMuted;
    }
}

