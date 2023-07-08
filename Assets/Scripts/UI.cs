using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;
    private TextMeshProUGUI _speedText;
    private TextMeshProUGUI _gameOverText;

    private PlayerStatus _playerStatus;
    private PlayerControl _playerControl;


    public GameObject ScoreText;
    public GameObject SpeedText;
    public GameObject GameEndText;


    // Start is called before the first frame update
    void Start()
    {
        _scoreText = ScoreText.GetComponent<TextMeshProUGUI>();
        _speedText = SpeedText.GetComponent<TextMeshProUGUI>();
        _gameOverText = GameEndText.GetComponent<TextMeshProUGUI>();
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
        if (_playerStatus.IsPlaying)
        {
            _speedText.enabled = true;
            _gameOverText.enabled = false;
            _scoreText.SetText($"Score: {_playerStatus.Points}");
            var (currentSpeed, maxSpeed) = _playerControl.GetSpeedData();
            if (currentSpeed < 0.01f)
            {
                currentSpeed = 0;
            }
            _speedText.SetText($"Speed: {currentSpeed:0.00}/{maxSpeed}");
        }
        else
        {
            _speedText.enabled = false;
            _gameOverText.enabled = true;
            var closingText = _playerStatus.GameStatus == GameStatus.Won ? "You Win!" : "Game Over";
            _gameOverText.SetText($"{closingText}\nPress \"R\" to Restart");
        }
    }
}
