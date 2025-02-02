using NaughtyAttributes;
using UnityEngine;

public class Particles : MonoBehaviour {
    [BoxGroup("Particles Config")] public ParticleSystem particleSys;
    [SerializeField, BoxGroup("Particles Config")] private string _id;

    public string Id {get => _id;}
    
}