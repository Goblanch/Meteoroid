using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioConfiguration", menuName = "Meteoroid/Audio/AudioConfiguration")]
public class AudioConfiguration : ScriptableObject
{
    public float musicVolume {get; private set;}
    public  float fxVolume {get; private set;}

    public Action OnAudioChange;

    public void ChangeMusicVolume(float newVolume){
        Debug.Log(newVolume);

        musicVolume = newVolume;

        OnAudioChange?.Invoke();
    }

    public void ChangeFxVolume(float newVolume){
        newVolume = Mathf.Clamp01(musicVolume);

        fxVolume = newVolume;

        OnAudioChange?.Invoke();
    }
}
