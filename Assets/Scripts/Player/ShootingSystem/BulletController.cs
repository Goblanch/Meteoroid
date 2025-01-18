using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class BulletController : RecyclableObject
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float timeToHide = 3f;

    private bool _enabled = false;
    public Vector2 direction = Vector2.up;

    private Rigidbody2D _rb2d;

    private void Awake() {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        StartCoroutine(HideBullet());
    }

    // Update is called once per frame
    void Update()
    {
        if(_enabled) Move(direction);
    }

    private void Move(Vector2 direction){
        _rb2d.linearVelocity = direction.normalized * bulletSpeed;
    }

    private IEnumerator HideBullet(){
        yield return new WaitForSeconds(timeToHide);
        Recycle();
    }

    internal override void Init(Vector2 position)
    {
        transform.position = position;
        _enabled = true;
    }

    internal override void Release()
    {
        
    }
}
