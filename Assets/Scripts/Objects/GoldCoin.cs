using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoldCoin : GrabAble {
    public GameObject flashParticles;

    public int goldCoinIndex;

    private GameController _gameController;
    private ObjectPool _objectPool;

    private bool _alreadyCatched;
    protected override void Awake()
    {
        base.Awake();
        GameObject gameController = GameObject.FindGameObjectWithTag(Tags.GAMECONTROLLER);
        _gameController = gameController.GetComponent<GameController>();
        _objectPool = gameController.GetComponent<ObjectPool>();
    }

    void Start()
    {
        //if the goldcoin is already grabbed in the certain level
        if (_gameController.goldCoins.ContainsKey(Application.loadedLevel))
        {
            if (_gameController.goldCoins[Application.loadedLevel][goldCoinIndex])
            {
                Color newColor = Color.gray;
                newColor.a = 0.5f;
                this.GetComponent<SpriteRenderer>().color = newColor;
                _alreadyCatched = true;
            }
        }
    }

    public override void GrabObject()
    {
        base.GrabObject();
    }

    public override void ObjectCatched()
    {
        base.ObjectCatched();
        if (!_alreadyCatched)
        {
            Instantiate(flashParticles, this.transform.position, Quaternion.identity);
            _gameController.goldCoins[Application.loadedLevel][goldCoinIndex] = true;
        }
        //Creating a UI text for additional player feedback
        GameObject text = _objectPool.GetObjectForType("Text", false) as GameObject; //using the object pool for performance
        text.transform.position = this.transform.position;
        text.GetComponent<Text>().text = "+500";
        text.GetComponent<Text>().color = Color.green;
        AdditionalTextFunctions textScript = text.GetComponent<AdditionalTextFunctions>();
        textScript.SetPosition(this.transform.position + new Vector3(Random.Range(-1, 1), Random.Range(0, 2), 0));
        textScript.SetAbleToMove(true);
        textScript.SetResetTime(1);

        //Adding score
        _gameController.score += 500;
    }
}
