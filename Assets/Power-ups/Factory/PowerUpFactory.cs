using UnityEngine;

public class PowerUpFactory
{
    private readonly PowerUpConfiguration powerUpConfig;

    public PowerUpFactory(PowerUpConfiguration powerUpConfig){
        this.powerUpConfig = powerUpConfig;
    }

    public PowerUp Create(string id){
        var prefab = powerUpConfig.GetPowerUpPrefabById(id);

        return Object.Instantiate(prefab);
    }
}
