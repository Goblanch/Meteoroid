using UnityEngine;

public class AsteroidFactory {
    private readonly AsteroidsConfiguration asteroidConfig;

    public AsteroidFactory(AsteroidsConfiguration asteroidConfig){
        this.asteroidConfig = asteroidConfig;
    }

    public AsteroidController Create(string id){
        var prefab = asteroidConfig.GetAsteroidPrefabById(id);

        return Object.Instantiate(prefab);
    }
}