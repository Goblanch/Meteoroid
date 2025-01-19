using System.Xml.Serialization;
using NaughtyAttributes;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AsteroidController : RecyclableObject, IDamagable
{   
    public int tier;
    private float _speed;
    private Vector2 _direction;

    private ObjectPool[] relatedPools;

    private Rigidbody2D _rb2d;
    private AsteroidSpawnerManager spawnerManager;
    private bool _active = false;

    private void Awake() {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        spawnerManager = ServiceLocator.Instance.GetService<AsteroidSpawnerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_active) Move();
    }

    public void Configure(float speed, Vector2 direction){
        _speed = speed;
        _direction = direction;
    }

    public void Move(){
        _rb2d.linearVelocity = _direction.normalized * _speed;
    }

    internal override void Init(Vector2 position)
    {
        _active = true;
    }

    internal override void Release()
    {
        _active = false;
    }

    public void TakeDamage()
    {
        Debug.Log("ASTEROID HIT");
        ServiceLocator.Instance.GetService<AsteroidSpawnerManager>().OnAsteroidDestroyed(tier, transform);
    }
}
