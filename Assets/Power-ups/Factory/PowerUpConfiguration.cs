using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpConfiguration", menuName = "Meteoroid!/Factory Configurations/PowerUpConfiguration")]
public class PowerUpConfiguration : ScriptableObject
{
    [SerializeField] private PowerUp[] powerUps;
    private Dictionary<string, PowerUp> idToPoweUp;

    private void OnEnable()
    {
        idToPoweUp = new Dictionary<string, PowerUp>(powerUps.Length);
        foreach(var powerUp in powerUps){
            idToPoweUp.Add(powerUp.Id, powerUp);
        }
    }

    public PowerUp GetPowerUpPrefabById(string id){
        if(!idToPoweUp.TryGetValue(id, out var powerUp)) throw new System.Exception($"PowerUp with id {id} does not exist");

        return powerUp;
    }
}
