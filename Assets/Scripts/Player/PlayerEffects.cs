using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerControl))]
public class PlayerEffects : MonoBehaviour
{
    public GameObject Exhaust;

    private ParticleSystem _exhaust;
    private PlayerControl _playerControl;

    private float maxParticleRate = 250;
    // Start is called before the first frame update
    void Start()
    {
        _exhaust = Exhaust.GetComponent<ParticleSystem>();
        _playerControl = GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        var (current, max) = _playerControl.GetSpeedData();
        var emission = _exhaust.emission;

        emission.rateOverTime = Mathf.Clamp(
            maxParticleRate * (current / max),
            50,
            maxParticleRate);

    }
}
