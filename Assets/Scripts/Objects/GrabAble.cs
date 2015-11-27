using UnityEngine;
using System.Collections;

public class GrabAble : MonoBehaviour {
    public bool isAffectedByGravity;

    protected Rigidbody2D _rigidbody;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if(isAffectedByGravity)
        {
            _rigidbody.gravityScale = 1;
        }
    }

    /// <summary>
    /// Function triggers when object has been grabbed
    /// </summary>
    public virtual void GrabObject()
    {

    }

    /// <summary>
    /// Function triggers when object has been catched by the player
    /// </summary>
    public virtual void ObjectCatched()
    {

    }
}
