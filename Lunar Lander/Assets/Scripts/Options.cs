using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{

    public AudioMixer audioMixer;

    private AudioManager audioManager;

    private UIController uiController;

    [SerializeField] Toggle muteToggle;
    [SerializeField] Slider masterSlider, musicSlider, sfxSlider;

    void Start()
    {
        uiController = FindObjectOfType<UIController>();

        audioManager = AudioManager.instance;

        if (audioManager.muted)
        {
            muteToggle.isOn = true;
        }
    }

    // links options slider to audiomixer groups
    public void SetMasterVolume()
    {
        audioMixer.SetFloat("Master", Mathf.Log(masterSlider.value) * 20);
    }

    public void SetMusicVolume()
    {
        audioMixer.SetFloat("Music", Mathf.Log(musicSlider.value) * 20);
    }

    public void SetSFXVolume()
    {
        audioMixer.SetFloat("SFX", Mathf.Log(sfxSlider.value) * 20);
    }

    // pauses audio listeners to mute all audio
    public void MuteToggle()
    {
        if(!audioManager.muted)
        {
            AudioListener.pause = !AudioListener.pause;
            audioManager.muted = false;
        }

        if (audioManager.muted)
        {
            AudioListener.pause = AudioListener.pause;
            audioManager.muted = true;
        }
    }
}
