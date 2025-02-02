using UnityEngine;

public class ParticlesFactory {
    private readonly ParticlesConfiguration particlesConfig;

    public ParticlesFactory(ParticlesConfiguration particlesConfig){
        this.particlesConfig = particlesConfig;
    }

    public Particles Create(string id){
        var prefab = particlesConfig.GetParticlePrefabById(id);

        return Object.Instantiate(prefab);
    }
}