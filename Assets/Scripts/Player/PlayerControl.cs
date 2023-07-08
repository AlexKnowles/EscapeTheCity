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

    private GameObject StartPoint;

    private Transform _followCameraTransform;
    private Camera _mainCamera;

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

        StartPoint = GameObject.FindWithTag("StartPoint");

        OnReset();

        var FollowCamera = GameObject.FindWithTag("MainCamera");

        if (FollowCamera != null)
        {
            _followCameraTransform = FollowCamera.transform.parent;
            _mainCamera = FollowCamera.GetComponent<Camera>();
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

    public (float, float) GetSpeedData()
    {
        return (_rigidbody.velocity.magnitude, TopSpeed);
    }

    public float GetSidewaysness()
    {
        Vector3 forward = transform.forward;
        Vector3 velocity = _rigidbody.velocity.normalized;
        float dotProduct = Vector3.Dot(forward, velocity);
        float sidewaysness = Mathf.Clamp01(1f - dotProduct);
        if (sidewaysness < 0.01f || sidewaysness == 1)
        {
            return 0;
        }
        return sidewaysness;
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
            _rigidbody.detectCollisions = true;
            _playerStatus.Reset();
        }
    }

    private void HandleStoppedPlayer()
    {
        if (_playerStatus.GameStatus == GameStatus.Lost)
        {
            // just fall off the world for now
            _rigidbody.detectCollisions = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.deltaTime;

        if (!_playerStatus.IsPlaying)
        {
            HandleStoppedPlayer();
            return;
        }

        var sidewaysNess = GetSidewaysness();
        if (sidewaysNess > 0.2f)
        {
            //_rigidbody.angularVelocity *= (1f - _rigidbody.angularDrag * deltaTime);

            var lostToSideways = _rigidbody.velocity * sidewaysNess;

            _rigidbody.velocity -= lostToSideways;

            _rigidbody.velocity += (lostToSideways.magnitude / 2 * transform.forward);
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
            var (current, max) = GetSpeedData();
            var cameraZoom = 1.5f * ((current + 2f) / max);
            _mainCamera.orthographicSize = Mathf.Lerp(_mainCamera.orthographicSize, Mathf.Clamp(1.5f + cameraZoom, 1.5f, 3.0f), Time.deltaTime);
        }
    }
}
