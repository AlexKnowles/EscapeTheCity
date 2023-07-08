using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;
    private TextMeshProUGUI _speedText;

    private PlayerStatus _playerStatus;
    private PlayerControl _playerControl;


    public GameObject ScoreText;
    public GameObject SpeedText;


    // Start is called before the first frame update
    void Start()
    {
        _scoreText = ScoreText.GetComponent<TextMeshProUGUI>();
        _speedText = SpeedText.GetComponent<TextMeshProUGUI>();
        var player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            _playerStatus = player.GetComponent<PlayerStatus>();
            _playerControl = player.GetComponent<PlayerControl>();
        }
        else
        {
            Debug.LogError("Could not find player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.SetText($"Score: {_playerStatus.Points}");
        var (currentSpeed, maxSpeed) = _playerControl.GetSpeedData();
        if (currentSpeed < 0.01f)
        {
            currentSpeed = 0;
        }
        _speedText.SetText($"Speed: {currentSpeed:0.00}/{maxSpeed}");
    }
}
