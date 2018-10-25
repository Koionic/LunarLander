using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //To use:
    // AudioManager audiomanager;
    // in start method { audiomanager = AudioManager.instance; }
    // to play sound audioManager.PlaySound("Sound Name")
    // to play sound from a specific audiosource audioManager.PlaySound("Sound Name", audiosource)

    public static AudioManager instance; 

    [SerializeField] SoundArray[] sound; 

    public bool muted = false;

    void Awake()
    {
        
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
        for (int i = 0; i < sound.Length; i++)
        {
            GameObject _go = new GameObject("Sound " + i + " " + sound[i].clipName);
            _go.transform.SetParent(this.transform);
            sound[i].SetSource(_go.AddComponent<AudioSource>());
        }

        PlaySound("MultiplayerMusic");
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sound.Length; i++)
        {
            if (sound[i].clipName == _name)
            {
                sound[i].PlaySound();
                return;
            }
        }
    }

    public void PlaySound(string _name, AudioSource _audioSource)
    {
        for (int i = 0; i < sound.Length; i++)
        {
            if (sound[i].clipName == _name)
            {
                sound[i].Play(_audioSource);
                return;
            }
        }
    }
}
