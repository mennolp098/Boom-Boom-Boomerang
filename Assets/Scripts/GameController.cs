using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    public delegate void NormalDelegate();
    public event NormalDelegate OnGamePaused;
    public event NormalDelegate OnGameResumed;
    public event NormalDelegate OnScoreUpdated;
    public event NormalDelegate OnLivesUpdated;

    private int _levelCount = 2;

    private int _score;
    private int _silverCoins = 0;
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

        //check if event onnewgame has been triggered
        GetComponent<SaveLoadDataSerialized>().OnNewGame += InitGame;

        if (Application.loadedLevel > 0)
            InitGame();
    }

    /// <summary>
    /// Setting standard values if a new game has been started
    /// </summary>
    void InitGame()
    {
        _level = 0;
        _lives = 3;
        _score = 0;
        _silverCoins = 0;
        for (int i = 0; i < _levelCount+1; i++) //Check how many levels are currently in the game and make a empty dictionary for each level.
        {
            bool[] newBoolarray = new bool[3];
            newBoolarray[0] = false;
            newBoolarray[1] = false;
            newBoolarray[2] = false;
            _levelGoldCoinsCollected.Add(i, newBoolarray);
        }
        StartGame();
    }

    void StartGame()
    {
        _fadeScreen = GameObject.FindGameObjectWithTag(Tags.FADESCREEN);
        _player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        
        Player currentPlayerScript = _player.GetComponent<Player>();
        currentPlayerScript.OnDeath += PlayerDeath;
        currentPlayerScript.OnWin += OnLevelCompleted;
        currentPlayerScript.OnCheckpointTouched += SetCheckPointPosition;
        currentPlayerScript.OnGoldCoinCatched += GoldCoinGrabbed;

        _player.GetComponent<PlayerInput>().EscapeKeyPressed += PauseOrResume;
    }

    /// <summary>
    /// Triggers when the level has been completed
    /// </summary>
    private void OnLevelCompleted()
    {
        //Fade the black screen to alpha 1
        _fadeScreen.GetComponent<FadeInOut>().Fade(1);
        _fadeScreen.GetComponent<FadeInOut>().OnFadeEnd += Win;
    }

    private void Win(float fadeValue = 0)
    {
        PauseOrResume();
        _gameEnded = true;

        if (_level < Application.loadedLevel)
            _level++;
    }

    /// <summary>
    /// Set wich gold coin to true
    /// </summary>
    /// <param name="index"></param>
    private void GoldCoinGrabbed(int index)
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
            //adding a fade effect to the death
            _fadeScreen.GetComponent<FadeInOut>().Fade(1);
            _fadeScreen.GetComponent<FadeInOut>().OnFadeEnd += RespawnPlayer;
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
    private void RespawnPlayer(float value = 0)
    {
        _player.transform.position = _checkpointPosition;
        _player.GetComponent<Movement>().enabled = true;
        _fadeScreen.GetComponent<FadeInOut>().OnFadeEnd -= RespawnPlayer;
        _fadeScreen.GetComponent<FadeInOut>().Fade(0);
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

    /// <summary>
    /// get paused game
    /// </summary>
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

    /// <summary>
    /// Public int level getter and setter
    /// </summary>
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

    /// <summary>
    /// Public dictionary that checks the level[int] and wich coin has been taken or not array boolean [coin1,coin2,coin3]
    /// </summary>
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

    /// <summary>
    /// Getter and setter for the int _silverCoins
    /// </summary>
    public int silverCoins
    {
        get
        {
            return _silverCoins;
        }
        set
        {
            _silverCoins = value;
        }
    }

    public bool gameEnded
    {
        get
        {
            return _gameEnded;
        }
        set
        {
            _gameEnded = value;
        }
    }
}
