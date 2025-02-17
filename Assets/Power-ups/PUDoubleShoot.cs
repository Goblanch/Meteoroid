using NaughtyAttributes;
using UnityEngine;

public class PUDoubleShoot : PowerUp{

    [SerializeField, BoxGroup("Power Up Config")] private float doubleShootTime = 5f;

    protected override void PowerUpEffect(PlayerController player)
    {
        Debug.Log("DOUBLE SHOOT");
        player.ShootSys.EnableDoubleShoot(doubleShootTime);
    }
}