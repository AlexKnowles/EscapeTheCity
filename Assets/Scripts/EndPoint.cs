using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EndPoint : MonoBehaviour
{
    public GameObject Player;

    private Rigidbody _playerRb;

    private PlayerStatus _playerStatus;

    public bool IsPlaying = true;

    // Start is called before the first frame update
    void Start()
    {
        _playerRb = Player.GetComponent<Rigidbody>();
        _playerStatus = Player.GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        var otherRb = other.GetComponent<Rigidbody>();
        if (otherRb != null && otherRb == _playerRb)
        {
            Debug.Log("Entered the winning zone");
            _playerStatus.PlayerWins();
        }
    }
}
