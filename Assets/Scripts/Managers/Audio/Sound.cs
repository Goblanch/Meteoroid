using UnityEngine.Audio;
using UnityEngine;

public enum SoundTypes {
    Music, FX, Subtitle, Enviroment
}

[System.Serializable]
public class Sound{
    public string name;
    public SoundTypes tag;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    public bool loop;
    public AudioSource source;

}