using UnityEngine;
using System.Collections;

public class Coin : GrabAble {
    private GameController _gameController;
    void Awake()
    {
        _gameController = GameObject.FindGameObjectWithTag(Tags.GAMECONTROLLER).GetComponent<GameController>();
    }

    public override void GrabObject()
    {
        base.GrabObject();
    }

    public override void ObjectCatched()
    {
        base.ObjectCatched();

        //adding score
        _gameController.score += 100;
    }
}
