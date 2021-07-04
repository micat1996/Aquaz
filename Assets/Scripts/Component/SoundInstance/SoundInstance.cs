using Micat1996UnityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundInstanceType { Effect, BGM }
public enum SoundInstanceStatus { Stop, Playing, Pause }

[RequireComponent(typeof(AudioSource))]
public sealed class SoundInstance : MonoBehaviour, IObjectPoolable
{
    public AudioSource audioSource { get; private set; }
    public bool canRecyclable { get; set; }
    public Action onRecycleStartEvent { get; set; }
    public SoundInstanceStatus status { get; private set; }

    [HideInInspector]
    public SoundInstanceType m_SoundInstanceType;

    public Action onSoundStopped;
    public Action onSoundPaused;

    public float _InitTime;
    public float _Length;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (status == SoundInstanceStatus.Pause) return;

        if (status == SoundInstanceStatus.Playing)
        {
            if (!audioSource.isPlaying)
            {
                Stop();
            }
        }
    }

    public SoundInstance InitializeSoundInstance(SoundInstanceType soundInstanceType)
    {
        m_SoundInstanceType = soundInstanceType;
        _InitTime = Time.time;
        return this;
    }

    public void Play(AudioClip audioClip = null)
    {
        // AudioSource Null Check
        if (!audioSource)
        {
            Debug.LogError("audioClip is null");
            return;
        }

        if (audioClip != null)
        {
            audioSource.clip = audioClip;
            _Length = audioClip.length;

            Debug.Log("InitTime = " + _InitTime);
            Debug.Log("_Length = " + _Length);
            audioSource.Play();
        }
        else
            audioSource.UnPause();

        status = SoundInstanceStatus.Playing;
    }

    public void Stop()
    {
        onSoundStopped?.Invoke();
        audioSource.Stop();
    }

    public void Pause()
    {
        status = SoundInstanceStatus.Pause;
        audioSource.Pause();
        onSoundPaused?.Invoke();
    }


}
