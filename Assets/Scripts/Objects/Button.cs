using UnityEngine;
using System.Collections;

public class Button : PuzzleObject {
    public PuzzleObject objectToActivate;
	void Start () {
        _isActivateAble = true;
        _isBoomerangActivateAble = true;
	}

    public override void Activate()
    {
        base.Activate();
        objectToActivate.Activate();
    }
}
