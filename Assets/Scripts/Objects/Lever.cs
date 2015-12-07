using UnityEngine;
using System.Collections;

public class Lever : PuzzleObject {
    public PuzzleObject objectToActivate;

    private Animator _myAnimator;

    void Start()
    {
        _isActivateAble = true;
        _isBoomerangActivateAble = false;

        _myAnimator = GetComponent<Animator>();
    }
    public override void Activate()
    {
        base.Activate();
        _myAnimator.SetTrigger("Pull");

        //activates a certain object with the puzzleobject component added to it
        objectToActivate.Activate();
    }
}
