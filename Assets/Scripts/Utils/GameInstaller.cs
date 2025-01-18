using NaughtyAttributes;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
   [field: SerializeField, BoxGroup("Services")] private AudioManager audioManager;

   private void Awake() {
      ServiceLocator.Instance.RegisterService<AudioManager>(audioManager);
   }
   
}