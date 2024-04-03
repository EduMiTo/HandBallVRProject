using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider musicSlider;
    public AudioSource backGroundMusic;
    public Slider sfxSlider;
    public AudioSource sfx;
    public Slider audienceSlider;
    public AudioSource audienceAudioSource;

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        audienceSlider.value = PlayerPrefs.GetFloat("audienceVolume");

        backGroundMusic.volume = musicSlider.value;
        sfx.volume = sfxSlider.value;
        audienceAudioSource.volume = audienceSlider.value;

    }

    public void ChangeMusicVolume()
    {
        backGroundMusic.volume = musicSlider.value;
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
        
    }

    public void ChangeSFXVolume()
    {
        sfx.volume = sfxSlider.value;
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
        
    }

    public void ChangeAudienceVolume()
    {
        audienceAudioSource.volume = audienceSlider.value;
        PlayerPrefs.SetFloat("audienceVolume", audienceSlider.value);
    }
}
