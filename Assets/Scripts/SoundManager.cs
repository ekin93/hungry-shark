using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip eatSound;
    public AudioClip dieSound;

    public void PlayEatSound()
    {
        audioSource.clip = eatSound;
        audioSource.Play();
    }

    public void PlayDieSound()
    {
        audioSource.clip = dieSound;
        audioSource.Play();
    }
}
