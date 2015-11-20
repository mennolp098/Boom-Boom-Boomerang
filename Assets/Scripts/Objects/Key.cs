using UnityEngine;
using System.Collections;

public class Key : GrabAble {
    public PuzzleObject objectToActivate;
    public override void GrabObject()
    {
        base.GrabObject();
    }
    public override void ObjectCatched()
    {
        base.ObjectCatched();
        //activates a certain object with the puzzleobject component added to it
        objectToActivate.Activate();
    }
}
