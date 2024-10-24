using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource musicSource;   
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip walking;
    public AudioClip jumping;
    public AudioClip deaded;
    public AudioClip attack;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
