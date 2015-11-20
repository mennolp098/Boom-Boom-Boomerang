using UnityEngine;
using System.Collections;

public class PuzzleObject : MonoBehaviour {
    protected bool _isActivateAble;
    protected bool _isBoomerangActivateAble;

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
    /// Activate function triggers by certain events
    /// </summary>
	public virtual void Activate()
    {
        //Here comes the activate function
    }

    public bool isActivateAble
    {
        get
        {
            return _isActivateAble;
        }
        set
        {
            _isActivateAble = value;
        }
    }
    public bool isBoomerangActivateAble
    {
        get
        {
            return _isBoomerangActivateAble;
        }
        set
        {
            _isBoomerangActivateAble = value;
        }
    }
}
