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

    private ShipShootingSystem _shootSys;

    private ShipMotor _motor;
    public ShipMotor Motor {get => _motor;}

    private Vector2 _input;
    public Vector2 PInput {get => _input;}

    private bool _sprint;
    public bool Sprint {get => _sprint;}
    
    public Rigidbody2D _rb2d {get; private set;}
    private Vector2 _velocity;
    private float _currentSpeed;
    private bool _break;

    private Animator _animator;
    private GameManager _gManager;

    private void OnEnable()
    {
        SubscribeToInput();
    }

    private void OnDisable()
    {
        UnsubscribeInput();
        _gManager.OnGameReset -= ResetPlayer;
        _gManager.OnPlayerDeath -= PlayerDeath;
    }

    private void Awake()
    {
        InitializeReferences();
    }

    private void InitializeReferences(){
        _rb2d = GetComponent<Rigidbody2D>();
        _motor = GetComponent<ShipMotor>();
        _shootSys = GetComponent<ShipShootingSystem>();
        _animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateMachine.ConfigureSMachine(this);
        stateMachine.Initialize();

        _gManager = ServiceLocator.Instance.GetService<GameManager>();

        _gManager.OnGameReset += ResetPlayer;
        _gManager.OnPlayerDeath += PlayerDeath;
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Step();
        if(Input.GetKeyDown(KeyCode.M)){
            ServiceLocator.Instance.GetService<AudioManager>().PlaySound("MainMenu");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        IConsumible consumible = collision.GetComponent<IConsumible>();

        if(consumible != null){
            consumible.ApplyEffect();
        }
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

        if(input.y == 0){
            _animator.SetBool("moving", false);
            return;
        }

        _animator.SetBool("moving", true);
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
        _shootSys.Shoot(transform.position, transform.up);
    }

    public void HandlePauseGame()
    {

    }

    public void HandleResumeGame()
    {

    }

    public void ResetPlayer(){
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        _motor.ResetMotor();
        input.ChangeGameMode(InputListener.GameModes.Game);
    }

    public void PlayerDeath(){
        _motor.ResetMotor();
        input.ChangeGameMode(InputListener.GameModes.UI);
    }
}
