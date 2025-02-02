using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "AsteroidsConfiguration", menuName = "Meteoroid!/Factory Configurations/Particles Configuration")]
public class ParticlesConfiguration : ScriptableObject
{
    [SerializeField] private Particles[] particles;
    private Dictionary<string, Particles> idToParticles;

    private void OnEnable() {
        idToParticles = new Dictionary<string, Particles>(particles.Length);
        foreach(var particle in particles){
            idToParticles.Add(particle.Id, particle);
        }
    }

    public Particles GetParticlePrefabById(string id){
        if(!idToParticles.TryGetValue(id, out var particle)) throw new Exception($"Asteroid with id {id} does not exist");

        return particle;
    }
}
