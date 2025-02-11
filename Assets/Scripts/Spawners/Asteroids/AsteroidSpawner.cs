using UnityEngine;
using NaughtyAttributes;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour
{
    [BoxGroup("Spawn Points")] public AsteroidSpawnPoint[] spawnLocations;
    [BoxGroup("Factory")] public AsteroidsConfiguration asteroidsConfiguration;
    [BoxGroup("Spawn Config")] public float spawnCoolDown = 2f;

    [field: SerializeField, BoxGroup("Debug Settings")] private float spawnCirclesRadius = .5f;

    private AsteroidFactory asteroidFactory;
    private bool _canSpawn = true;
    private bool _spawnerEnabled = true;

    private void Start() {
        asteroidFactory = new AsteroidFactory(asteroidsConfiguration);
        ServiceLocator.Instance.GetService<GameManager>().OnPlayerDeath += () => _spawnerEnabled = false;
    }

    private void Update() {
        if(!_spawnerEnabled) return;
        SpawnAsteroid();
    }

    private void OnDisable() {
        ServiceLocator.Instance.GetService<GameManager>().OnPlayerDeath -= () => _spawnerEnabled = false;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;

        foreach(AsteroidSpawnPoint spawnPoint in spawnLocations){
            if(spawnPoint != null) Gizmos.DrawWireSphere(spawnPoint.transform.position, spawnCirclesRadius);
        }
    }

    public void SpawnAsteroid(){
        if(!_canSpawn) return;

        string id = asteroidsConfiguration.asteroidTypes[Random.Range(0, asteroidsConfiguration.asteroidTypes.Length)];

        AsteroidSpawnPoint spawnPoint = GetRandomSpawnPoint();
        Vector2 direction = spawnPoint.GetRandomSpawnDirection();
        
        AsteroidController asteroid = asteroidFactory.Create(id);
        asteroid.transform.position = spawnPoint.transform.position;
        asteroid.Initialize(direction);

        StartCoroutine(SpawnCoolDown());
    }

    private AsteroidSpawnPoint GetRandomSpawnPoint(){

        AsteroidSpawnPoint point = null;

        while(!point){
            point = spawnLocations[Random.Range(0, spawnLocations.Length)];
            if(!point.IsActive){
                point = null;
            }
        }

        return spawnLocations[Random.Range(0, spawnLocations.Length)];
    }

    private IEnumerator SpawnCoolDown(){
        _canSpawn = false;
        yield return new WaitForSeconds(spawnCoolDown);
        _canSpawn = true;
    }
}
