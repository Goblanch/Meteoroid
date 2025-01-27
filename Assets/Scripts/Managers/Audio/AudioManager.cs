using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioConfiguration audioConfiguration;

    private void OnEnable() {
        audioConfiguration.OnAudioChange += SetAudioConfiguration;
    }

    private void OnDisable() {
        audioConfiguration.OnAudioChange -= SetAudioConfiguration;
    }

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        InitializeAudios();
    }

    private void InitializeAudios()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    private void SetAudioConfiguration(){
        ChangeVolumeByType(SoundTypes.Music, audioConfiguration.musicVolume);
        ChangeVolumeByType(SoundTypes.FX, audioConfiguration.fxVolume);
    }

    public void PlaySound(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);

        if (s == null)
        {
            Debug.LogError($"Sound with name {soundName} not found");
            return;
        }

        s.source.Play();
    }

    public void PlaySoundsByType(SoundTypes type)
    {
        foreach (Sound s in sounds)
        {
            if (s.tag == type) s.source.Play();
        }
    }

    public void StopSound(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);

        if (s == null)
        {
            Debug.LogError($"Sound with name {soundName} not found");
            return;
        }

        s.source.Stop();
    }

    public void StopSoundsByType(SoundTypes type)
    {
        foreach (Sound s in sounds)
        {
            if (s.tag == type) s.source.Stop();
        }
    }

    public void ChangeVolumeByType(SoundTypes type, float volume)
    {
        foreach (Sound s in sounds)
        {
            if (s.tag == type) s.source.volume = volume;
        }
    }

    public void MuteAudioByType(SoundTypes type)
    {
        foreach (Sound s in sounds)
        {
            if (s.tag == type) s.volume = 0;
        }
    }
}