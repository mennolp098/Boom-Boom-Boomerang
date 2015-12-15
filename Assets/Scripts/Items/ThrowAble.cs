using UnityEngine;
using System.Collections;

public class ThrowAble : MonoBehaviour {
    protected bool _inHand = true;
    public virtual void Throw(Vector3 aimPos)
    {
        //throw function here
    }

    public bool inHand
    {
        set
        {
            _inHand = value;
        }
        get
        {
            return _inHand;
        }
    }
}
