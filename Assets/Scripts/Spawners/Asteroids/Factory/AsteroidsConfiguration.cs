using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsteroidsConfiguration", menuName = "Meteoroid/Factory Configurations/AsteroidsConfiguration")]
public class AsteroidsConfiguration : ScriptableObject
{
    [SerializeField] private AsteroidController[] asteroids;
    private Dictionary<string, AsteroidController> idToAsteroid;

    private void OnEnable() {
        idToAsteroid = new Dictionary<string, AsteroidController>(asteroids.Length);
        foreach(var asteroid in asteroids){
            idToAsteroid.Add(asteroid.Id, asteroid);
        }
    }

    public AsteroidController GetAsteroidPrefabById(string id){
        if(!idToAsteroid.TryGetValue(id, out var asteroid)) throw new Exception($"Asteroid with id {id} does not exist");

        return asteroid;        
    }
}
