using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotor : MonoBehaviour
{
    [field: SerializeField, BoxGroup("Movement")] public float moveSpeed = 5f;
    [field: SerializeField, BoxGroup("Movement")] public float sprintSpeed = 10f;


    public Rigidbody2D _rb2d {get; private set;}

    private Vector2 _velocity;
    public Vector2 Velocity {get => _velocity;}
    private float _currentSpeed;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        _currentSpeed = moveSpeed;
    }

    public void Move(Vector2 input){
        _velocity = input.normalized * _currentSpeed;
        _rb2d.linearVelocity = _velocity;
    }

    public void StartSprint(){
        _currentSpeed = sprintSpeed;
    }

    public void EndSprint(){
        _currentSpeed = moveSpeed;
    }
}
