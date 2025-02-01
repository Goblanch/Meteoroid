using NaughtyAttributes;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
   [field: SerializeField, BoxGroup("Services")] private AudioManager audioManager;
   [field: SerializeField, BoxGroup("Services")] private AsteroidSpawnerManager asteroidSpawnerManager;
   [field: SerializeField, BoxGroup("Services")] private AsteroidSpawner asteroidSpawner;
   [field: SerializeField, BoxGroup("Services")] private GameManager gameManager;

   private void Awake() {
      ServiceLocator.Instance.RegisterService<AsteroidSpawnerManager>(asteroidSpawnerManager);
      ServiceLocator.Instance.RegisterService<AsteroidSpawner>(asteroidSpawner);
      ServiceLocator.Instance.RegisterService<GameManager>(gameManager);
   }
   
   private void Start() {
      ServiceLocator.Instance.GetService<AudioManager>().StopSound("MainMenu");
      ServiceLocator.Instance.GetService<AudioManager>().PlaySound("Game");
   }
}