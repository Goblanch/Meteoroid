using NaughtyAttributes;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
   [field: SerializeField, BoxGroup("Services")] private AudioManager audioManager;
   [field: SerializeField, BoxGroup("Services")] private AsteroidSpawnerManager asteroidSpawnerManager;
   [field: SerializeField, BoxGroup("Services")] private AsteroidSpawner asteroidSpawner;

   private void Awake() {
      ServiceLocator.Instance.RegisterService<AudioManager>(audioManager);
      ServiceLocator.Instance.RegisterService<AsteroidSpawnerManager>(asteroidSpawnerManager);
      ServiceLocator.Instance.RegisterService<AsteroidSpawner>(asteroidSpawner);
   }
   
}