using UnityEngine;
using System.Collections;

public class Lever : PuzzleObject {
    public PuzzleObject objectToActivate;
    void Start()
    {
        _isActivateAble = true;
        _isBoomerangActivateAble = false;
    }
    public override void Activate()
    {
        base.Activate();
        //activates a certain object with the puzzleobject component added to it
        objectToActivate.Activate();
    }
}
