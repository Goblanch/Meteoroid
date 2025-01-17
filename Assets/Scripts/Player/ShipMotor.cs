using UnityEngine;
using NaughtyAttributes;
using UnityEditor.Callbacks;
using UnityEditor.Rendering;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipMotor : MonoBehaviour
{
    [field: SerializeField, BoxGroup("Car Settings")] private float maxSpeed = 50f;
    [field: SerializeField, BoxGroup("Car Settings")] private float accelerationFactor = 30f;
    [field: SerializeField, BoxGroup("Car Settings")] private float turnFactor = 50f;
    [field: SerializeField, BoxGroup("Car Settings"), Range(0, 1)] private float minSpeedFactor = .05f;
    [field: SerializeField, BoxGroup("Car Settings")] private float limitDrag = 2f;
    [field: SerializeField, BoxGroup("Car Settings")] private float driftFactor = .9f;

    private Rigidbody2D _rb2d;
    private float _accelerationInput;
    private float _steeringInput;
    private float _breakInput;
    private float _rotationAngle;
    private float _totalAcceleration;
    private float _velocityForward;

    private void Awake() {
        _rb2d = GetComponent<Rigidbody2D>();
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

    public void SetInput(float accelerationInput, float breakInput, float steeringInput){
        _accelerationInput = accelerationInput;
        _breakInput = breakInput;
        _steeringInput = steeringInput;

        _totalAcceleration = _accelerationInput - _breakInput;
    }
}
