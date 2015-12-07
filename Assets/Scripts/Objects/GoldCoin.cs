using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoldCoin : GrabAble {
    public GameObject flashParticles;

    public int goldCoinIndex;

    private GameController _gameController;
    private ObjectPool _objectPool;

    private bool _alreadyCatched;
    void Awake()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag(Tags.GAMECONTROLLER);
        _gameController = gameController.GetComponent<GameController>();
        _objectPool = gameController.GetComponent<ObjectPool>();

        if (_gameController.goldCoins[_gameController.level][goldCoinIndex])
        {
            this.GetComponent<SpriteRenderer>().color = Color.gray;
            _alreadyCatched = true;
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
            //Creating a UI text for additional player feedback
            GameObject text = _objectPool.GetObjectForType("Text", false) as GameObject; //using the object pool for performance
            text.transform.position = this.transform.position;
            text.GetComponent<Text>().text = "+500";
            text.GetComponent<Text>().color = Color.green;
            AdditionalTextFunctions textScript = text.GetComponent<AdditionalTextFunctions>();
            textScript.SetPosition(this.transform.position + new Vector3(Random.Range(-1, 1), Random.Range(0, 2), 0));
            textScript.SetAbleToMove(true);
            textScript.SetResetTime(1);

            Instantiate(flashParticles, this.transform.position, Quaternion.identity);

            //Adding score
            _gameController.score += 500;
        }
    }
}
