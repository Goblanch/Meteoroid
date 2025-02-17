using NaughtyAttributes;
using UnityEngine;

public class PowerUp : MonoBehaviour, IConsumible
{
    [SerializeField, BoxGroup("Powe Up Settings")] protected string _id;
    public string Id => _id;

    public void ApplyEffect()
    {
        PowerUpEffect();
        Destroy(gameObject);
    }

    protected virtual void PowerUpEffect(){
        Debug.Log("Power Up BASE");
    }
}
