using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.FilePathAttribute;

[RequireComponent(typeof (PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerControl : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private bool _accelerating = false;
    private bool _braking = false;
    private float _steer = 0.0f;

    private float _turnAmount = 0.0f;

    public GameObject FollowCamera;
    private Transform _followCameraTransform;

    public float AccelerationFactor = 15f;

    public float BrakingFactor = -20f;

    public float TurningSpeed = 90f;

    public float DriftFactor = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (FollowCamera != null)
        {
            _followCameraTransform = FollowCamera.GetComponent<Transform>();
        }
    }

    public void OnAccelerate(InputValue value)
    {
        _accelerating = value.isPressed;
    }

    public void OnBrake(InputValue value)
    {
        _braking = value.isPressed;
    }

    private const float _steerLeft = -1;
    private const float _steerRight = 1;

    public void OnSteer(InputValue value)
    {
        // right is 1, left is -1
        _steer = value.Get<float>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.deltaTime;
        // do this some other time
        //if (_rigidbody.velocity.magnitude > 0.01f && Vector3.Distance(_rigidbody.velocity.normalized, new Vector3(
        //    _rigidbody.rotation.x, _rigidbody.rotation.y, _rigidbody.rotation.z).normalized) > 0.01f)
        //{
        //    // pull the car toward forward
        //   // _rigidbody.velocity = Quaternion.RotateTowards(
        //     //   Quaternion.LookRotation(_rigidbody.velocity.normalized), _rigidbody.rotation, DriftFactor * deltaTime) * _rigidbody.velocity.normalized * _rigidbody.velocity.magnitude;
        //}

        if (_accelerating)
        {
            _rigidbody.velocity += _rigidbody.rotation * new Vector3(0, 0, AccelerationFactor) * deltaTime;
        }

        if (_braking)
        {
            _rigidbody.velocity += _rigidbody.rotation * new Vector3(0, 0, BrakingFactor) * deltaTime;
            if (_rigidbody.velocity.magnitude > 0 && Vector3.Dot(_rigidbody.velocity.normalized, transform.forward) < 0)
            {
                _rigidbody.velocity = new Vector3(0, 0, 0);
            }
        }
        var forwardMovementAmount = Vector3.Dot(_rigidbody.velocity.normalized, transform.forward);
        var movingForward = _rigidbody.velocity.magnitude > 0 && forwardMovementAmount > 0;
        if (movingForward)
        {
            if (_steer == _steerLeft)
            {
                _turnAmount -= TurningSpeed * 0.5f * forwardMovementAmount;
            } else if (_steer == _steerRight)
            {
                _turnAmount += TurningSpeed * 0.5f * forwardMovementAmount;
            }
            else
            {
                _turnAmount = 0;
            }
            if (Mathf.Abs(_turnAmount) > TurningSpeed)
            {
                _turnAmount = Mathf.Sign(_turnAmount) * TurningSpeed;
            }
            _rigidbody.rotation = _rigidbody.rotation * Quaternion.AngleAxis(_turnAmount * deltaTime, Vector3.up);
        }

        if (_followCameraTransform != null)
        {
            _followCameraTransform.position = _rigidbody.position;
        }
    }
}
