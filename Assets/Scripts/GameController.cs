using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public delegate void NormalDelegate();
    public event NormalDelegate OnGamePaused;
    public event NormalDelegate OnGameResumed;
    public event NormalDelegate OnScoreUpdated;

    private float _score;
    private float _lives = 3;

    private GameObject _player;

    private Vector3 _checkpointPosition;

    private bool _paused;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        Player currentPlayerScript = _player.GetComponent<Player>();
        currentPlayerScript.OnDeath += PlayerDeath;
        currentPlayerScript.OnCheckpointTouched += SetCheckPointPosition;
    }

    /// <summary>
    /// Triggers when the player dies
    /// </summary>
    private void PlayerDeath()
    {
        if(_lives > 0)
        {
            _lives--;
            RespawnPlayer();
        }
        else
        {
            LoseGame();
        }
    }

    /// <summary>
    /// Shows endscreen and pauses the game
    /// </summary>
    private void LoseGame()
    {
        //TODO: show endsceen and pause game
    }

    /// <summary>
    /// set player position to checkpointposition and unpause movement
    /// </summary>
    private void RespawnPlayer()
    {
        _player.transform.position = _checkpointPosition;
        //TODO: unpause movement
    }

    /// <summary>
    /// Set a new checkpoint position
    /// </summary>
    /// <param name="pos"></param>
    private void SetCheckPointPosition(Vector3 pos)
    {
        _checkpointPosition = pos;
    }

    public float score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            if (OnScoreUpdated != null)
                OnScoreUpdated();
        }
    }

    public void PauseOrResume()
    {
        if(!_paused)
        {
            _paused = true;
            if (OnGamePaused != null)
                OnGamePaused();
        }
        else
        {
            _paused = false;
            if (OnGameResumed != null)
                OnGameResumed();
        }
    }
}
