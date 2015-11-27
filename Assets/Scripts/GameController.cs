using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public delegate void NormalDelegate();
    public event NormalDelegate OnGamePaused;
    public event NormalDelegate OnGameResumed;
    public event NormalDelegate OnScoreUpdated;
    public event NormalDelegate OnLivesUpdated;

    private int _score;
    private int _lives = 3;
    private int _level = 0;

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
        _player.GetComponent<Movement>().enabled = true;
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
    /// <summary>
    /// Pauses or Resumes the game
    /// </summary>
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

    /// <summary>
    /// Public int score to set and get the _score value, this also triggers a update event
    /// </summary>
    public int score
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

    /// <summary>
    /// Public vector3 checkpointPosition to set and get the _checkpointPosition value
    /// </summary>
    public Vector3 checkpointPosition
    {
        get
        {
            return _checkpointPosition;
        }
        set
        {
            _checkpointPosition = value;
        }
    }

    public int level
    {
        get
        {
            return _level;
        }
        set
        {
            _level = value;
        }
    }

    /// <summary>
    /// Public int lives to set and get the _lives value, this also triggers a update event
    /// </summary>
    public int lives
    {
        get
        {
            return _lives;
        }
        set
        {
            _lives = value;
            if (OnLivesUpdated != null)
                OnLivesUpdated();
        }
    }
}
