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

    public float AccelerationFactor = 0.01f;

    public float BrakingFactor = -0.05f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnAccelerate(InputValue value)
    {
        _accelerating = value.isPressed;
    }

    public void OnBrake(InputValue value)
    {
        _braking = value.isPressed;
    }

    // Update is called once per frame
    void Update()
    {
        if (_accelerating)
        {
            _rigidbody.velocity += new Vector3(0, 0, AccelerationFactor);
        }

        if (_braking)
        {
            _rigidbody.velocity += new Vector3(0, 0, BrakingFactor);
            if (_rigidbody.velocity.z < 0)
            {
                _rigidbody.velocity = new Vector3(0, 0, 0);
            }
        }
    }
}
