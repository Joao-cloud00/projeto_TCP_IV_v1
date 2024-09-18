using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MenuScene")
        {
            SoundManager.instance.PlayMenuMusic();
        }
        else if (scene.name == "Fase 1" )
        {
            SoundManager.instance.PlayGameMusic();
        }
    }
}
