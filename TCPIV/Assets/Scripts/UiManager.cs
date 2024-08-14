using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour
{
    public Slider volumeSlider;

    private AndroidJavaObject audioManager;

    void Start()
    {
        // Inicializa o AudioManager do Android
        audioManager = new AndroidJavaObject("android.media.AudioManager");

        // Define o valor inicial do slider para o volume atual do dispositivo
        volumeSlider.value = GetDeviceVolume();
    }

    void Update()
    {
        // Atualiza o valor do slider para refletir o volume atual do dispositivo
        volumeSlider.value = GetDeviceVolume();
    }

    float GetDeviceVolume()
    {
        // Obtém o volume do dispositivo Android usando o AudioManager
        int currentVolume = audioManager.Call<int>("getStreamVolume", 3); // AudioManager.STREAM_MUSIC = 3
        int maxVolume = audioManager.Call<int>("getStreamMaxVolume", 3); // AudioManager.STREAM_MUSIC = 3

        float deviceVolume = (float)currentVolume / maxVolume;
        return deviceVolume;
    }
}








