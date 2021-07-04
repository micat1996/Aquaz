using Micat1996UnityFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : ManagerClassBase<SoundManager>
{

    [SerializeField]
    private SoundInstance _SoundInstancePrefab;

    private ObjectPool<SoundInstance> _SoundPool = new ObjectPool<SoundInstance>();


    private SoundInstance GetUsableSoundInstance(SoundInstanceType soundInstanceType)
    {
        SoundInstance newSoundInstance = _SoundPool.GetRecycleObject((SoundInstance soundInstance) =>
        soundInstance.m_SoundInstanceType == soundInstanceType) ??
        _SoundPool.RegisterRecyclableObject(
        Instantiate(_SoundInstancePrefab, transform));

        return newSoundInstance.InitializeSoundInstance(soundInstanceType);
    }



    public SoundInstance PlayEffectSound(AudioClip audioClip)
    {
        SoundInstance soundInstance = GetUsableSoundInstance(SoundInstanceType.Effect);

        soundInstance.audioSource.loop = false;
        soundInstance.Play(audioClip);

        return soundInstance;
    }
    
}
