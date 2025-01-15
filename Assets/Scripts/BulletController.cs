using JetBrains.Annotations;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float timeToDestroy = 10f;
    public Vector2 direction = Vector2.up;

    private Rigidbody2D _rb2d;

    private void Awake() {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        Destroy(this.gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        Move(direction);
    }

    private void Move(Vector2 direction){
        _rb2d.linearVelocity = direction.normalized * bulletSpeed;
    }
}
