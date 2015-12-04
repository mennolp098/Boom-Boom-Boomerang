using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    public delegate void NormalDelegate();
    public event NormalDelegate OnGamePaused;
    public event NormalDelegate OnGameResumed;
    public event NormalDelegate OnScoreUpdated;
    public event NormalDelegate OnLivesUpdated;

    private int _score;
    private int _lives = 3;
    private int _level = 0;
    private Dictionary<int, bool[]> _levelGoldCoinsCollected = new Dictionary<int, bool[]>();

    private GameObject _player;

    private GameObject _fadeScreen;

    private Vector3 _checkpointPosition;

    private bool _paused;
    private bool _gameEnded;

    public static GameController Instance;

    void Awake()
    {
        if (Instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

    void OnLevelWasLoaded(int level)
    {
        if (level > 0)
            StartGame();
    }

    void StartGame()
    {
        _player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        Player currentPlayerScript = _player.GetComponent<Player>();
        currentPlayerScript.OnDeath += PlayerDeath;
        currentPlayerScript.OnWin += LevelCompleted;
        currentPlayerScript.OnCheckpointTouched += SetCheckPointPosition;
        currentPlayerScript.OnGoldCoinCatched += GetGoldCoin;

        _fadeScreen = GameObject.FindGameObjectWithTag(Tags.FADESCREEN);
    }

    /// <summary>
    /// Triggers when the level has been completed
    /// </summary>
    private void LevelCompleted()
    {
        //Fade the black screen to alpha 1
        _fadeScreen.GetComponent<FadeInOut>().Fade(1);
        _fadeScreen.GetComponent<FadeInOut>().OnFadeEnd += ShowWinScreen;
    }

    private void ShowWinScreen(float fadeValue = 0)
    {
        PauseOrResume();
        _gameEnded = true;

        //TODO: show win screen
    }

    private void GetGoldCoin(int index)
    {
        _levelGoldCoinsCollected[_level][index] = true;
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
        if (!_gameEnded)
        {
            if (!_paused)
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

    public bool isPaused
    {
        get
        {
            return _paused;
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
    public Dictionary<int, bool[]> goldCoins
    {
        get
        {
            return _levelGoldCoinsCollected;
        }
        set
        {
            _levelGoldCoinsCollected = value;
        }
    }
}
