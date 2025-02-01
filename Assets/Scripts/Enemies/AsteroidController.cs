using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AsteroidController : MonoBehaviour, IDamagable
{
    [SerializeField, BoxGroup("Asteroid Settings")] private string _id;
    public string Id => _id;
    [SerializeField, BoxGroup("Asteroid Settings")] private float timeOutOfBoundsBeforeDestroy = 2f;
    [BoxGroup("Asteroid Settings")] public Vector2 speedRange;
    [BoxGroup("Asteroid Settings")] public Vector2 direction = Vector2.down;
    [BoxGroup("References")] public AsteroidsConfiguration asteroidsConfiguration;
    [BoxGroup("References")] public Transform dirRefParent;
    [BoxGroup("References")] public Transform[] dirReferences;


    private Rigidbody2D _rb2d;
    private float _speed;
    private float _outOfBoundsTimer = 0f;
    private AsteroidFactory _asteroidFactory;

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
        _asteroidFactory = new AsteroidFactory(asteroidsConfiguration);
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
        dirRefParent.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void TakeDamage()
    {
        Debug.Log("ASTEROID HIT");
        SplitAsteroid();
        Destroy(gameObject);
    }

    private void SplitAsteroid(){
        switch(_id){
            case "big":
                SpawnAsteroidsSideWays("normal");
                break;
            case "normal":
                SpawnAsteroidsSideWays("small");
                break;
        }
    }

    private void SpawnAsteroidsSideWays(string id){
        foreach(Transform t in dirReferences){
            Vector2 direction = t.position - transform.position;

            AsteroidController newAsteroid = _asteroidFactory.Create(id);
            newAsteroid.transform.position = transform.position;
            newAsteroid.Initialize(direction);
        }
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
