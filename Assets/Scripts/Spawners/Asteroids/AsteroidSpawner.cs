using UnityEngine;
using NaughtyAttributes;

public class AsteroidSpawner : MonoBehaviour
{
    [BoxGroup("Spawn Points")] public AsteroidSpawnPoint[] spawnLocations;
    [BoxGroup("Factory")] public AsteroidsConfiguration asteroidsConfiguration;

    [field: SerializeField, BoxGroup("Debug Settings")] private float spawnCirclesRadius = .5f;

    private AsteroidFactory asteroidFactory;

    private void Start() {
        asteroidFactory = new AsteroidFactory(asteroidsConfiguration);
    }

    private void Update() {
        // DEBUG
        // if(Input.GetKeyDown(KeyCode.G)){
        //     SpawnAsteroid("normal");
        // }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;

        foreach(AsteroidSpawnPoint spawnPoint in spawnLocations){
            if(spawnPoint != null) Gizmos.DrawWireSphere(spawnPoint.transform.position, spawnCirclesRadius);
        }
    }

    public void SpawnAsteroid(string id){
        AsteroidSpawnPoint spawnPoint = GetRandomSpawnPoint();
        Vector2 direction = spawnPoint.GetRandomSpawnDirection();
        
        AsteroidController asteroid = asteroidFactory.Create(id);
        asteroid.transform.position = spawnPoint.transform.position;
        asteroid.Initialize(direction);
    }

    private AsteroidSpawnPoint GetRandomSpawnPoint(){
        return spawnLocations[Random.Range(0, spawnLocations.Length)];
    }
}
