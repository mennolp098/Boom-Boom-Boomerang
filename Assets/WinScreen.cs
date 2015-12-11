using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour {
    public Text coinsText;
    public Text scoreText;
    public Text levelText;
    public GameObject[] coins = new GameObject[3];

    private GameController _gameController;
    private bool _isVisible;
    private float _moveSpeed = 5;
    private RectTransform _rectTransform;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _gameController = GameObject.FindGameObjectWithTag(Tags.GAMECONTROLLER).GetComponent<GameController>();
    }
	void Update () {
        //Moving the winscreen to the center of the screen so the player can see it
	    if(_isVisible && _rectTransform.anchoredPosition.y != 0)
        {
            Vector3 movePlace = new Vector2(0, 0);
            _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition, movePlace, _moveSpeed * Time.deltaTime);
        }
	}

    /// <summary>
    /// Shows the win screen
    /// </summary>
    public void ShowWinScreen()
    {
        //set boolean visible to true so it starts moving towards the center of the screen
        _isVisible = true;

        for (int i = 0; i < coins.Length; i++)
        {
            if (!_gameController.goldCoins[_gameController.level][i]) //if the coin is not picked up
                coins[i].SetActive(false); //hide the image of the coin
        }

        //Show how many coins you picked up
        coinsText.text = "x" + _gameController.silverCoins.ToString();

        //Showing score
        scoreText.text = _gameController.score.ToString();

        //Showing level
        levelText.text = "Level-" + Application.loadedLevel.ToString();
    }

    /// <summary>
    /// Triggers when the play button is pressed
    /// </summary>
    public void OnPlayButtonPressed()
    {
        //TODO: level++
        //TODO: Save new information
        //TODO: Application.LoadLevel(0);
    }

    /// <summary>
    /// Triggers when the restart button is pressed
    /// </summary>
    public void OnRestartButtonPressed()
    {
        //TODO: level++
        //TODO: Save new information
        //TODO: Application.LoadLevel(Application.loadedLevel);
    }

    /// <summary>
    /// Triggers when the options button is pressed
    /// </summary>
    public void OnOptionsButtonPressed()
    {

    }
}
