using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("INPUT")]
    [SerializeField] private InputListener input;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform shootOrigin;
    public float shootCoolDown = 0.2f;

    private Rigidbody2D _rb2d;
    private Vector2 _input;
    private Vector2 _velocity;
    private float _currentSpeed;
    private bool _canShoot = true;
    private bool _onCoolDown;

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
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SubscribeToInput()
    {
        input.MoveEvent += HandleMovement;
        input.SprintEvent += HandleSprint;
        input.ShootEvent += HandleShoot;
        input.PauseEvent += HandlePauseGame;
        input.ResumeEvent += HandleResumeGame;
    }

    private void UnsubscribeInput()
    {
        input.MoveEvent -= HandleMovement;
        input.SprintEvent -= HandleSprint;
        input.ShootEvent -= HandleShoot;
        input.PauseEvent -= HandlePauseGame;
        input.ResumeEvent -= HandleResumeGame;
    }

    public void HandleMovement(Vector2 input)
    {
        _input = input;
        _velocity = input.normalized * _currentSpeed;
        _rb2d.linearVelocity = _velocity;
    }

    public void HandleSprint(bool sprint)
    {
        _currentSpeed = moveSpeed;
        if (sprint) _currentSpeed = sprintSpeed;
        HandleMovement(_input);
    }

    public void HandleShoot()
    {
        if(_canShoot) Instantiate(bulletPrefab, shootOrigin.position, Quaternion.identity);
        if(!_onCoolDown) StartCoroutine(ShootCoolDown());
    }

    private IEnumerator ShootCoolDown(){
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
