using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlayerControl))]
public class PlayerEffects : MonoBehaviour
{
    public GameObject Exhaust;
    public List<GameObject> TyreTracks = new List<GameObject>();

    private ParticleSystem _exhaust;
    private List<ParticleSystem> _tyreTracks;
    private PlayerControl _playerControl;

    private float maxParticleRate = 250;
    // Start is called before the first frame update
    void Start()
    {
        _exhaust = Exhaust.GetComponent<ParticleSystem>();

        _tyreTracks = TyreTracks.Select(x => x.GetComponent<ParticleSystem>()).ToList();
        _playerControl = GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        var (current, max) = _playerControl.GetSpeedData();
        var howSideways = _playerControl.GetSidewaysness();

        _tyreTracks.ForEach(track =>
        {
            var tEm = track.emission;
            tEm.rateOverTime = Mathf.Clamp(
            maxParticleRate * 5 * howSideways,
            0,
            maxParticleRate);
        });

        var emission = _exhaust.emission;

        emission.rateOverTime = Mathf.Clamp(
            maxParticleRate * (current / max),
            50,
            maxParticleRate);


    }
}
