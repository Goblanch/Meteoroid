using UnityEngine;

public class AsteroidSpawnerManager : MonoBehaviour
{

    public Vector2 asteroidSpeedRange;
    public Vector2 spawnDirectionOffset;

    private AsteroidSpawner asteroidSpawner;

    private void Start()
    {
        asteroidSpawner = ServiceLocator.Instance.GetService<AsteroidSpawner>();
    }

    public void OnAsteroidDestroyed(int tier, Transform asteroidTransform)
    {
        switch (tier)
        {
            case 1:
                // TODO: Add points only
                break;
            case 2:
                asteroidSpawner.SpawnAsteroid(1, asteroidTransform.position, Random.Range(asteroidSpeedRange.x, asteroidSpeedRange.y), GetRandomDirection(asteroidTransform.up));
                break;
            case 3:
                asteroidSpawner.SpawnAsteroid(2, asteroidTransform.position, Random.Range(asteroidSpeedRange.x, asteroidSpeedRange.y), GetRandomDirection(asteroidTransform.up));
                break;
            default:
                Debug.LogError($"Error OnAsteroidDestroyed. Unknown tier {tier}");
                break;
        }
    }

    private Vector2 GetRandomDirection(Vector2 direction)
    {

        return new Vector2(
            direction.x + Random.Range(spawnDirectionOffset.x, spawnDirectionOffset.y),
            direction.y + Random.Range(spawnDirectionOffset.x, spawnDirectionOffset.y)
        );
    }
}