using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource menuMusic;
    public AudioSource gameMusic;

    private void Awake()
    {
        // Garante que o SoundManager persista entre as cenas
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMenuMusic()
    {
        gameMusic.Stop();
        menuMusic.Play();
    }

    public void PlayGameMusic()
    {
        menuMusic.Stop();
        gameMusic.Play();
    }
}
