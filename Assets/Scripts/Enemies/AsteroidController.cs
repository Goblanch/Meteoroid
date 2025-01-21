using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AsteroidController : MonoBehaviour, IDamagable
{
    [SerializeField] private string _id;
    public string Id => _id;
    [SerializeField] private float timeOutOfBoundsBeforeDestroy = 2f;
    public Vector2 speedRange;
    public Vector2 direction = Vector2.down;
    public Transform dirRefParent;
    public Transform[] dirReferences;


    private Rigidbody2D _rb2d;
    private float _speed;
    private float _outOfBoundsTimer = 0f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Transform t in dirReferences)
        {
            if (t != null) Gizmos.DrawLine(transform.position, t.position);
        }
    }

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _speed = Random.Range(speedRange.x, speedRange.y);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LookAtVelocityDirection();
        CheckOutOfBounds();
    }

    public void Initialize(Vector2 direction)
    {

        this.direction = direction;
    }

    private void Move()
    {
        _rb2d.linearVelocity = direction.normalized * _speed;
    }

    private void LookAtVelocityDirection()
    {
        float angle = Mathf.Atan2(_rb2d.linearVelocityY, _rb2d.linearVelocityX) * Mathf.Rad2Deg - 90f;
        //if(_rb2d.linearVelocityX > 0) angle += 180f;
        dirRefParent.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void TakeDamage()
    {
        Debug.Log("ASTEROID HIT");
    }

    private void CheckOutOfBounds()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        bool isOutOfBound = screenPosition.x < 0 || screenPosition.x > Screen.width ||
                            screenPosition.y < 0 || screenPosition.y > Screen.height; ;

        if(isOutOfBound){
            _outOfBoundsTimer += Time.deltaTime;

            if(_outOfBoundsTimer >= timeOutOfBoundsBeforeDestroy){
                Destroy(gameObject);
            }
        }else{
            _outOfBoundsTimer = 0;
        }
    }
}
