using UnityEngine;
using NaughtyAttributes;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using System.Collections;

public class ShipShootingSystem : MonoBehaviour
{
    [BoxGroup("Configuration")] public BulletController bulletPrefab;
    [BoxGroup("Configuration")] public float shootCoolDown = .2f;
    
    private ObjectPool bulletsPool;
    private Vector2 shootOrigin;
    private Vector2 shootDirection;
    private bool _canShoot = true;
    private bool _onCoolDown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletsPool = new ObjectPool(bulletPrefab);
        bulletsPool.Init(20);
    }

    public void Shoot(Vector2 origin, Vector2 direction){
        if(!_canShoot) return;
        
        BulletController bullet = bulletsPool.Spawn<BulletController>(origin);
        bullet.direction = direction;

        if(!_onCoolDown) StartCoroutine(ShootCoolDown());
    }

    private IEnumerator ShootCoolDown(){
        _canShoot = false;
        _onCoolDown = true;
        yield return new WaitForSeconds(shootCoolDown);
        _canShoot = true;
        _onCoolDown = false;
    }
}
