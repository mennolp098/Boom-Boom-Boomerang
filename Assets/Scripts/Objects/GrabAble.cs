using UnityEngine;
using System.Collections;

public class GrabAble : MonoBehaviour {
    public bool isAffectedByGravity;
    public bool bouncy;

    protected Rigidbody2D _rigidbody;

    private float _startTimeFalling;
    private float _startYFalling;
    private float _bounceDamp = 0.1f;
    private bool _timeSet;
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if(isAffectedByGravity)
        {
            _rigidbody.gravityScale = 1;
        }
    }
    void Start()
    {
        _startTimeFalling = Time.time;
        _startYFalling = this.transform.position.y;
        _timeSet = true;
    }

    void Update()
    {
        //create a little bit of bouncynes for the grabable object
        if(isAffectedByGravity && bouncy)
        {
            if(_rigidbody.velocity.y <= 0.25f && !_timeSet)
            {
                _startTimeFalling = Time.time;
                _startYFalling = this.transform.position.y;
                _timeSet = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other != null && other.transform.tag != Tags.PLAYER)
        {
            //giving the coins a little bit of bouncyness
            if (isAffectedByGravity && bouncy)
            {
                float fallingSpeed = (_startYFalling - this.transform.position.y) / (Time.time - _startTimeFalling);
                Vector3 newVelocity = Vector3.zero;
                newVelocity.y += fallingSpeed - _bounceDamp;
                _rigidbody.velocity = newVelocity;
                _timeSet = false;
            }
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
