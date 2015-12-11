using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Coin : GrabAble {
    private GameController _gameController;
    private ObjectPool _objectPool;
    void Awake()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag(Tags.GAMECONTROLLER);
        _gameController = gameController.GetComponent<GameController>();
        _objectPool = gameController.GetComponent<ObjectPool>();
    }

    public override void GrabObject()
    {
        base.GrabObject();
    }

    public override void ObjectCatched()
    {
        base.ObjectCatched();

        //Creating a UI text for additional player feedback
        GameObject text = _objectPool.GetObjectForType("Text", false) as GameObject; //using the object pool for performance
        text.transform.position = this.transform.position;
        text.GetComponent<Text>().text = "+100";
        text.GetComponent<Text>().color = Color.yellow;
        AdditionalTextFunctions textScript = text.GetComponent<AdditionalTextFunctions>();
        textScript.SetPosition(this.transform.position + new Vector3(Random.Range(-1,1),Random.Range(0,2),0));
        textScript.SetAbleToMove(true);
        textScript.SetResetTime(1);

        //Adding score
        _gameController.score += 100;
        _gameController.silverCoins++;
    }
}
