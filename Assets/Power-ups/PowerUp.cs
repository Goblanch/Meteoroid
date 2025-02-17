using NaughtyAttributes;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour, IConsumible
{
    [SerializeField, BoxGroup("Powe Up Settings")] protected string _id;
    public string Id => _id;

    public void ApplyEffect(PlayerController player)
    {
        PowerUpEffect(player);
        Destroy(gameObject);
    }

    protected virtual void PowerUpEffect(PlayerController player){
        Debug.Log("Power Up BASE");
    }
}
