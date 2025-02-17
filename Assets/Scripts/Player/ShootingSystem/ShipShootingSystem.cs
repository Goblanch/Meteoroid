using UnityEngine;
using NaughtyAttributes;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using System.Collections;
using System;

public class ShipShootingSystem : MonoBehaviour
{
    [BoxGroup("Configuration")] public BulletController bulletPrefab;
    [BoxGroup("Configuration")] public float shootCoolDown = .2f;
    [SerializeField, BoxGroup("Double Shoot Config")] private float doubleShootCoolDown = .1f;

    private ObjectPool bulletsPool;
    private Vector2 shootOrigin;
    private Vector2 shootDirection;
    private bool _canShoot = true;
    private bool _doubleShootEnabled = false;
    private bool _onCoolDown;
    private AudioManager audioManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletsPool = new ObjectPool(bulletPrefab);
        bulletsPool.Init(20);

        audioManager = ServiceLocator.Instance.GetService<AudioManager>();

        ClampDoubleShootTime();
    }

    public void Shoot(Vector2 origin, Vector2 direction)
    {
        if (!_canShoot) return;

        CreateBullet(origin, direction);

        if(_doubleShootEnabled) StartCoroutine(DoubleShoot(origin, direction));

        if (!_onCoolDown) StartCoroutine(ShootCoolDown());
    }

    private void CreateBullet(Vector2 origin, Vector2 direction)
    {
        BulletController bullet = bulletsPool.Spawn<BulletController>(origin);
        bullet.direction = direction;
        audioManager.PlaySound("Shot");
    }

    private IEnumerator ShootCoolDown()
    {
        _canShoot = false;
        _onCoolDown = true;
        yield return new WaitForSeconds(shootCoolDown);
        _canShoot = true;
        _onCoolDown = false;
    }

    private IEnumerator DoubleShoot(Vector2 origin, Vector2 direction){
        yield return new WaitForSeconds(doubleShootCoolDown);
        CreateBullet(origin, direction);
    }

    public void EnableDoubleShoot(float t){
        StartCoroutine(DoubleShootTimer(t));
    }

    private IEnumerator DoubleShootTimer(float t){
        _doubleShootEnabled = true;
        yield return new WaitForSeconds(t);
        _doubleShootEnabled = false;
    }

    private void ClampDoubleShootTime(){
        if(doubleShootCoolDown > shootCoolDown){
            Debug.LogWarning("Double Shoot Cooldown to high. Clampling value. Double Shoot Cooldown must be less or equal Shoot Cooldown");
            doubleShootCoolDown = Mathf.Clamp(doubleShootCoolDown, 0, shootCoolDown - .1f);
        }
    }
}
