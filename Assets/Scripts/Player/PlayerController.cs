using System.Collections;
using UnityEngine;
using NaughtyAttributes;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    [Header("INPUT")]
    [field: SerializeField, BoxGroup("Input")] private InputListener input;

    [Header("State Machine")]
    [field: SerializeField, BoxGroup("State Machine Reference")] private PlayerStateMachine stateMachine;

    [field: SerializeField, BoxGroup("Shooting")] public BulletController bulletPrefab;
    [field: SerializeField, BoxGroup("Shooting")] public Vector2 shootOrigin;
    [field: SerializeField, BoxGroup("Shooting")] public float shootCoolDown = 0.2f;
    
    private ShipMotor _motor;
    public ShipMotor Motor {get => _motor;}

    private Vector2 _input;
    public Vector2 PInput {get => _input;}

    private bool _sprint;
    public bool Sprint {get => _sprint;}

    private ObjectPool bulletsPool;

    public Rigidbody2D _rb2d {get; private set;}
    private Vector2 _velocity;
    private float _currentSpeed;
    private bool _canShoot = true;
    private bool _onCoolDown;
    private bool _break;

    private void OnEnable()
    {
        SubscribeToInput();
    }

    private void OnDisable()
    {
        UnsubscribeInput();
    }

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _motor = GetComponent<ShipMotor>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateMachine.ConfigureSMachine(this);
        stateMachine.Initialize();

        bulletsPool = new ObjectPool(bulletPrefab);
        bulletsPool.Init(40);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Step();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsStep();
    }

    private void LateUpdate()
    {
        stateMachine.LateStep();
    }

    private void SubscribeToInput()
    {
        input.MoveEvent += HandleMovement;
        input.SprintEvent += HandleSprint;
        input.BreakEvent += HandleBreak;
        input.ShootEvent += HandleShoot;
        input.PauseEvent += HandlePauseGame;
        input.ResumeEvent += HandleResumeGame;
    }

    private void UnsubscribeInput()
    {
        input.MoveEvent -= HandleMovement;
        input.SprintEvent -= HandleSprint;
        input.BreakEvent -= HandleBreak;
        input.ShootEvent -= HandleShoot;
        input.PauseEvent -= HandlePauseGame;
        input.ResumeEvent -= HandleResumeGame;
    }

    public void HandleMovement(Vector2 input)
    {
        _input = input;
        _motor.SetInput(_input.y, 0, _input.x);
    }

    public void HandleSprint(bool sprint)
    {
        _sprint = !_sprint;
        _motor.SetTurboMode(_sprint);
    }

    public void HandleBreak(){
        _break = !_break;

        if(_break){
            _motor.SetInput(_input.y, 1, _input.x);
        }else{
            _motor.SetInput(_input.y, 0, _input.x);
        }
    }

    public void HandleShoot()
    {
        if (_canShoot) bulletsPool.Spawn<BulletController>(transform.position);

        // Instantiate(
        //     bulletPrefab,
        //     transform.position + (Vector3)shootOrigin,
        //     Quaternion.identity
        // );

        if (!_onCoolDown) StartCoroutine(ShootCoolDown());
    }

    private IEnumerator ShootCoolDown()
    {
        _canShoot = false;
        _onCoolDown = true;
        yield return new WaitForSeconds(shootCoolDown);
        _canShoot = true;
        _onCoolDown = false;
    }

    public void HandlePauseGame()
    {

    }

    public void HandleResumeGame()
    {

    }
}
