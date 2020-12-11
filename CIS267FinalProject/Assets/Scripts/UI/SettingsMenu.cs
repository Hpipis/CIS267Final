using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider slider;
    static float volumestatic;

    void Update()
    {
        slider.value = volumestatic;
    }

    public void SetVolume(float volume)
    {
        volumestatic = volume;
        audioMixer.SetFloat("volume", volume);
    }

}
