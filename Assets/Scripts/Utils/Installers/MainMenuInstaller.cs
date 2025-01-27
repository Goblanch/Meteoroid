using NaughtyAttributes;
using UnityEngine;

public class MainMenuInstaller : MonoBehaviour
{
   [field: SerializeField, BoxGroup("Services")] private AudioManager audioManager;

   private void Awake() {
      ServiceLocator.Instance.RegisterService<AudioManager>(audioManager);
   }

   private void Start() {
      ServiceLocator.Instance.GetService<AudioManager>().PlaySound("MainMenu");
   }
   
}