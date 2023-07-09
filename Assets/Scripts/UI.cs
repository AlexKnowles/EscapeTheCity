using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;
    private TextMeshProUGUI _speedText;
    private TextMeshProUGUI _gameOverText;
    private Slider _timeRemainingSlider;
    private Image _timeRemainingFillImage;
    private Image _timeRemainingBackgroundImage;
    private TextMeshProUGUI _timeRemainingText;

    private PlayerStatus _playerStatus;
    private PlayerControl _playerControl;


    public GameObject ScoreText;
    public GameObject SpeedText;
    public GameObject GameEndText;
    public GameObject TimeRemainingSlider;
    public GameObject TimeRemainingText;


    // Start is called before the first frame update
    void Start()
    {
        _scoreText = ScoreText.GetComponent<TextMeshProUGUI>();
        _speedText = SpeedText.GetComponent<TextMeshProUGUI>();
        _gameOverText = GameEndText.GetComponent<TextMeshProUGUI>();

        _timeRemainingText = TimeRemainingText.GetComponent<TextMeshProUGUI>();
        _timeRemainingSlider = TimeRemainingSlider.GetComponent<Slider>();
        _timeRemainingFillImage = _timeRemainingSlider.transform.Find("Fill Area").Find("Fill").GetComponent<Image>();
        _timeRemainingBackgroundImage = _timeRemainingSlider.transform.Find("Background").GetComponent<Image>();

        var player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            _playerStatus = player.GetComponent<PlayerStatus>();
            _playerControl = player.GetComponent<PlayerControl>();

            _timeRemainingSlider.maxValue = _playerStatus.Lifetime;
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
            _speedText.SetText($"Speed: \n {currentSpeed:0.00}m/s");
            UpdateTimeRemainingSlider();

            if (_playerStatus.PlayerShouldUnwreckThemselves)
            {
                _gameOverText.enabled = true;
                _gameOverText.SetText($"Hit <space> to right yourself!");

            }
            else
            {
                _gameOverText.enabled = false;
            }
        }
        else
        {
            _speedText.enabled = false;
            _gameOverText.enabled = true;
            var closingText = _playerStatus.GameStatus == GameStatus.Won ? "You Win!" : "Game Over";
            _gameOverText.SetText($"{closingText}\nPress \"R\" to Restart");
        }
    }

    private void UpdateTimeRemainingSlider()
    {
        _timeRemainingText.SetText($"{_playerStatus.TimeRemaining:00.00}");

        _timeRemainingSlider.value = _playerStatus.TimeRemaining;

        float timeRemainingAsPercent = (_timeRemainingSlider.value / _timeRemainingSlider.maxValue); 

        if (timeRemainingAsPercent >= 0.65f)
        {
            _timeRemainingFillImage.color = new(0.2745098f, 1f, 0);
            _timeRemainingBackgroundImage.color = new(0.2431373f, 0.7372549f, 0.2392157f);
        }
        else if (timeRemainingAsPercent >= 0.30f)
        {
            _timeRemainingFillImage.color = new(1f, 0.7450981f, 0f);
            _timeRemainingBackgroundImage.color = new(0.7372549f, 0.6f, 0.2392157f);
        }
        else
        {
            _timeRemainingFillImage.color = new(1f, 0f, 0.3227463f);
            _timeRemainingBackgroundImage.color = new(0.7372549f, 0.2392157f, 0.3281927f);
        }
    }
}
