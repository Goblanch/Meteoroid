using UnityEngine;
using NaughtyAttributes;
using UnityEditor.Callbacks;
using UnityEditor.Rendering;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipMotor : MonoBehaviour
{
    [field: SerializeField, BoxGroup("Ship Settings")] private float maxSpeed = 50f;
    [field: SerializeField, BoxGroup("Ship Settings")] private float turboMultiplier = 2f;
    [field: SerializeField, BoxGroup("Ship Settings")] private float accelerationFactor = 30f;
    [field: SerializeField, BoxGroup("Ship Settings")] private float turnFactor = 50f;
    [field: SerializeField, BoxGroup("Ship Settings"), Range(0, 1)] private float minSpeedFactor = .05f;
    [field: SerializeField, BoxGroup("Ship Settings")] private float limitDrag = 2f;
    [field: SerializeField, BoxGroup("Ship Settings")] private float driftFactor = .9f;

    private Rigidbody2D _rb2d;
    public Vector2 Velocity {get => _rb2d.linearVelocity;}
    private float _currentMaxSpeed;
    private float _currentMaxSpeedBackUp;
    private float _accelerationInput;
    private float _steeringInput;
    private float _breakInput;
    private float _rotationAngle;
    private float _totalAcceleration;
    private float _velocityForward;
    private AudioManager audioManager;
    private bool _engineSoundEnabled = false;

    #region SETTERS

    public void SetMaxSpeed(float maxSpeed) => this.maxSpeed = maxSpeed;
    public void SetAccelerationFactor(float accelerationFactor) => this.accelerationFactor = accelerationFactor;
    public void SetTurnFactor(float turnFactor) => this.turnFactor = turnFactor;
    public void SetLimitDrag(float limitDrag) => this.limitDrag = limitDrag;
    public void SetDriftFactor(float driftFactor) => this.driftFactor = driftFactor;
    public void SetInput(float accelerationInput, float breakInput, float steeringInput){
        _accelerationInput = accelerationInput;
        _breakInput = breakInput;
        _steeringInput = steeringInput;

        _totalAcceleration = _accelerationInput - _breakInput;
    }

    #endregion

    private void Awake() {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        _currentMaxSpeed = maxSpeed;
        _currentMaxSpeedBackUp = _currentMaxSpeed;

        audioManager = ServiceLocator.Instance.GetService<AudioManager>();
    }

    private void Update() {
        EngineSound();
    }

    private void FixedUpdate() {
        ApplyEngineForce();
        ApplySteering();
        KillOrthogonalVelocity();
    }

    private void ApplyEngineForce(){
        _velocityForward = Vector3.Dot(_rb2d.linearVelocity, transform.up);
        if(_velocityForward > maxSpeed && _totalAcceleration > 0) return;
        if(_velocityForward < -maxSpeed * 0.5f && _totalAcceleration < 0) return;

        if(_totalAcceleration == 0){
            _rb2d.linearDamping = Mathf.Lerp(_rb2d.linearDamping, limitDrag, Time.deltaTime);
        }else{
            _rb2d.linearDamping = 0;
        }

        Vector2 engineForceVector = transform.up * _totalAcceleration * accelerationFactor * Time.deltaTime;
        _rb2d.linearVelocity += engineForceVector;
    }

    private void ApplySteering(){

        float deltaRotation = _steeringInput * turnFactor;
        _rotationAngle += deltaRotation;

        Debug.Log(transform.right);

        _rb2d.rotation = _rotationAngle;
    }

    private void KillOrthogonalVelocity(){
        Vector3 forwardVelocity = transform.up * Vector3.Dot(_rb2d.linearVelocity, transform.up);
        Vector3 rightVelocity = transform.right * Vector3.Dot(_rb2d.linearVelocity, transform.right);
        Vector3 fixedVelocity = forwardVelocity + rightVelocity * driftFactor;
        fixedVelocity.y = _rb2d.linearVelocityY;
        _rb2d.linearVelocity = fixedVelocity;
    }

    public void SetTurboMode(bool enabled){
        if(enabled) maxSpeed = _currentMaxSpeed * turboMultiplier;
        if(!enabled) maxSpeed = _currentMaxSpeedBackUp;
    }

    private void EngineSound(){
        if(!_engineSoundEnabled && _totalAcceleration != 0){
            audioManager.PlaySound("RocketEngine");
            _engineSoundEnabled = true;
        }

        if(_engineSoundEnabled && _totalAcceleration == 0){
            audioManager.StopSound("RocketEngine");
            _engineSoundEnabled = false;
        }
    }
   
}
