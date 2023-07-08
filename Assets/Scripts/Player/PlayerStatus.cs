using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus
{
    Playing,
    Won,
    Lost
}

public class PlayerStatus : MonoBehaviour
{
    public bool IsPlaying
    {
        get
        {
            return _gameStatus == GameStatus.Playing;
        }
    }

    public GameStatus GameStatus
    {
        get
        {
            return _gameStatus;
        }
    }

    private GameStatus _gameStatus = GameStatus.Playing;

    private GameStatus? _pendingStatus;

    public int Points = 0;

    public void Reset()
    {
        _gameStatus = GameStatus.Playing;
        _pendingStatus = null;
        Points = 0;
    }

    public void PlayerWins()
    {
        if (IsPlaying && _pendingStatus == null)
        {
            _pendingStatus = GameStatus.Won;
            Invoke("InternalChangeStatus", 0.25f);
        }
    }

    private void InternalChangeStatus()
    {
        _gameStatus = (GameStatus)_pendingStatus;
    }

    public void PlayerLoses()
    {
        if (IsPlaying && _pendingStatus == null)
        {
            _pendingStatus = GameStatus.Lost;
            Invoke("InternalChangeStatus", 0.25f);
        }
    }


    public void AdjustPoints(int adjustment)
    {
        Points += adjustment;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
