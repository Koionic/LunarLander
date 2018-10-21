using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundArray
{
    private AudioSource source;
    public string clipName;
    public AudioClip clip;
    public AudioMixerGroup audioMixerGroup;
    public bool loop = false;
    public bool playOnAwake = false;
    public bool oneShot = false;

    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 3f)]
    public float pitch;
    [Range(0f, 1f)]
    public float spatial = 0;

    public float minDistance3Dsound = 1f, maxDistance3DSound = 5f;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.volume = volume;
        source.playOnAwake = playOnAwake;
        source.loop = loop;
        source.outputAudioMixerGroup = audioMixerGroup;
        source.spatialBlend = spatial;
        source.minDistance = minDistance3Dsound;
        source.maxDistance = maxDistance3DSound;
    }

    public void PlaySound()
    {
        if (oneShot == true)
        {
            if (!source.isPlaying)
                source.Play();
        }
        else source.Play();
    }

    public void Play(AudioSource _audioSource)
    {
        SetSource(_audioSource);

        if (oneShot == true)
        {
            if (!_audioSource.isPlaying)
                _audioSource.Play();
        }
        else _audioSource.Play();
    }
}



