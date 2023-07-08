using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.FilePathAttribute;

[RequireComponent(typeof (PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerStatus))]
public class PlayerControl : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PlayerStatus _playerStatus;

    private bool _accelerating = false;
    private bool _braking = false;
    private bool _reversing = false;
    private float _steer = 0.0f;

    private Quaternion _initRotation;

    private float _turnAmount = 0.0f;

    public GameObject StartPoint;

    public GameObject FollowCamera;
    private Transform _followCameraTransform;

    public float AccelerationFactor = 15f;

    public float BrakingFactor = -20f;

    public float TurningSpeed = 90f;

    public float DriftFactor = 10f;

    public float TopSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerStatus = GetComponent<PlayerStatus>();

        _initRotation = _rigidbody.rotation;

        OnReset();

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
        if (!_braking && _rigidbody.velocity.magnitude < 0.1f)
        {
            _reversing = true;
        }
        else
        {
            _reversing = false;
            _braking = value.isPressed;
        }
    }

    private const float _steerLeft = -1;
    private const float _steerRight = 1;

    public void OnSteer(InputValue value)
    {
        // right is 1, left is -1
        _steer = value.Get<float>();
    }

    public void OnReset()
    {
        if (!_playerStatus.IsPlaying)
        {
            transform.position = StartPoint.transform.position;
            _rigidbody.velocity = new Vector3(0, 0, 0);
            _rigidbody.rotation = _initRotation;
            _playerStatus.Reset();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.deltaTime;

        if (!_playerStatus.IsPlaying)
        {
            return;
        }

        if (Vector3.Dot(_rigidbody.velocity, transform.forward) < 0.1f)
        {
            _rigidbody.angularVelocity *= (1f - _rigidbody.angularDrag * deltaTime);
        }

        if (_accelerating)
        {
            _rigidbody.velocity += _rigidbody.rotation * new Vector3(0, 0, AccelerationFactor) * deltaTime;
        }

        if (_reversing)
        {
            _rigidbody.velocity += _rigidbody.rotation * new Vector3(0, 0, -AccelerationFactor) * deltaTime;
        }

        if (_rigidbody.velocity.magnitude > TopSpeed)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * TopSpeed;
        }

        if (_braking)
        {
            _rigidbody.velocity += _rigidbody.rotation * new Vector3(0, 0, BrakingFactor) * deltaTime;
            if (_rigidbody.velocity.magnitude > 0 && Vector3.Dot(_rigidbody.velocity.normalized, transform.forward) < 0)
            {
                _rigidbody.velocity = new Vector3(0, 0, 0);
            }
        }

        if (_rigidbody.velocity.magnitude > 0.1f)
        {
            if (_steer == _steerLeft)
            {
                _turnAmount -= TurningSpeed * 0.5f * Vector3.Dot(_rigidbody.velocity, transform.forward);
            } else if (_steer == _steerRight)
            {
                _turnAmount += TurningSpeed * 0.5f * Vector3.Dot(_rigidbody.velocity, transform.forward);
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
