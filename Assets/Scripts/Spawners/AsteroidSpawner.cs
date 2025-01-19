using UnityEngine;
using NaughtyAttributes;
using System;
using System.Drawing;
using UnityEditor.PackageManager;
using System.Runtime.CompilerServices;

public class AsteroidSpawner : MonoBehaviour
{

    [BoxGroup("Asteroid Prefabs")] public AsteroidController tierOneAsteroid;
    [BoxGroup("Asteroid Prefabs")] public AsteroidController tierTwoAsteroid;
    [BoxGroup("Asteroid Prefabs")] public AsteroidController tierThreeAsteroid;
    [BoxGroup("Spawn Points")] public AsteroidSpawnPoint[] spawnPoints;
    [BoxGroup("Spawn Config")] public Vector2 spawnTimeRange;

    private ObjectPool tierOnePool;
    private ObjectPool tierTwoPool;
    private ObjectPool tierThreePool;
    private float _spawnTimer;
    private float _currentCoolDown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tierOnePool = new ObjectPool(tierOneAsteroid);
        tierTwoPool = new ObjectPool(tierTwoAsteroid);
        tierThreePool = new ObjectPool(tierThreeAsteroid);

        tierOnePool.Init(20);
        tierTwoPool.Init(40);
        tierThreePool.Init(60);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimer();
    }

    private void SpawnTimer()
    {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= _currentCoolDown)
        {
            SpawnAsteroid();
            _spawnTimer = 0;
        }
    }

    // TODO: Override function to tier based spawn asteroid (SpawnAsteroid(tier))

    private void SpawnAsteroid()
    {
        ObjectPool pool = GetRandomPool();
        AsteroidSpawnPoint spawnPoint = GetAvailableRandomSpawnPoint();

        AsteroidController spawnedAsteroid = pool.Spawn<AsteroidController>(spawnPoint.transform.position);
        _currentCoolDown = GetRandomCoolDown();
    }

    public void SpawnAsteroid(int tier, Vector2 position, float speed, Vector2 direction)
    {
        AsteroidController spawnedAsteroid;

        switch (tier)
        {
            case 1:
                spawnedAsteroid = tierOnePool.Spawn<AsteroidController>(position);
                spawnedAsteroid.Configure(speed, direction);
                break;
            case 2:
                spawnedAsteroid = tierTwoPool.Spawn<AsteroidController>(position);
                spawnedAsteroid.Configure(speed, direction);
                break;
            case 3:
                spawnedAsteroid = tierThreePool.Spawn<AsteroidController>(position);
                spawnedAsteroid.Configure(speed, direction);
                break;
            default:
                Debug.LogError($"Error trying to spawn asteroid. Unknow tier {tier}");
                break;
        }
    }

    private float GetRandomCoolDown()
    {
        return UnityEngine.Random.Range(spawnTimeRange.x, spawnTimeRange.y);
    }

    private ObjectPool GetRandomPool()
    {
        int number = UnityEngine.Random.Range(1, 4);
        switch (number)
        {
            case 1:
                return tierOnePool;
            case 2:
                return tierTwoPool;
            case 3:
                return tierThreePool;
            default:
                Debug.LogError($"[Asteroid Spawner] Error trying to get an pool. Tier {number} pool doen't exist");
                return null;
        }
    }

    private AsteroidSpawnPoint GetAvailableRandomSpawnPoint()
    {
        AsteroidSpawnPoint point = Array.Find(spawnPoints, p => p.Available == true);
        if (point == null) return null;
        return point;
    }
}
