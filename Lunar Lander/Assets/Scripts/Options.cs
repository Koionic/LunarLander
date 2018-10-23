using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{

    public AudioMixer audioMixer;

    private AudioManager audioManager;

    [SerializeField]
    private Toggle muteToggle;

    void Start()
    {
        audioManager = AudioManager.instance;

        if (audioManager.isMuted)
        {
            muteToggle.isOn = true;
        }

    }

    // links options slider to audiomixer groups
    public void SetVolumeMaster(float volume)
    {
        audioMixer.SetFloat("Master", volume);
    }
    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }
    public void SetVolumeSFX(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
    }

    // pauses audio listeners to mute all audio
    public void Mute()
    {
        AudioListener.pause = !AudioListener.pause;
        audioManager.isMuted = !audioManager.isMuted;
    }
}
