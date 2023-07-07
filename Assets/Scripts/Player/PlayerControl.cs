using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof (PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerControl : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private bool _accelerating = false;
    private bool _braking = false;
    private float _steer = 0.0f;

    public GameObject FollowCamera;
    private Transform _followCameraTransform;

    public float AccelerationFactor = 20f;

    public float BrakingFactor = -40f;

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

        var movingForward = _rigidbody.velocity.magnitude > 0 && Vector3.Dot(_rigidbody.velocity.normalized, transform.forward) > 0;
        if (movingForward)
        {
            if (_steer == _steerLeft)
            {
                _rigidbody.rotation = _rigidbody.rotation * Quaternion.AngleAxis(-TurningSpeed * deltaTime, Vector3.up);
            }

            if (_steer == _steerRight)
            {
                _rigidbody.rotation = _rigidbody.rotation * Quaternion.AngleAxis(TurningSpeed * deltaTime, Vector3.up);
            }
        }

        if (_followCameraTransform != null)
        {
            _followCameraTransform.position = _rigidbody.position;
        }
    }
}
