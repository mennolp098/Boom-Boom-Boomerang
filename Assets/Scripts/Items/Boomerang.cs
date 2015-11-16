using UnityEngine;
using System.Collections;

public class Boomerang : ThrowAble {
    public delegate void NormalDelegate();
    public event NormalDelegate DestinationReached;

    private GameObject _playerHand;
    private bool _returningToHand = false;
    private Vector3 _target = Vector3.zero;
    private float _mass = 50;
    private float _speed = 24;
    private float _rotationSpeed = 400;
    private Vector2 _directionMoving = new Vector2(0, 1);

    void Start()
    {
        _playerHand = GameObject.FindGameObjectWithTag(Tags.PLAYERHAND);
        _playerHand.GetComponent<ThrowingHand>().CatchThrowable(this.gameObject);
    }

    void Update()
    {
        if (_target != Vector3.zero) //here we check if there is a target
        {
            MoveTowardsPosition(_target); //using the movetowards function to go to the current position it's targeting at.
            RotateAnimation();
        }
        else if(_returningToHand)
        {
            MoveTowardsPosition(_playerHand.transform.position);
            RotateAnimation();
        }
    }

    /// <summary>
    /// Lets this move towards a position via mass and steering
    /// </summary>
    void MoveTowardsPosition(Vector3 pos)
    {
        Vector2 targetPosition = pos;

        Vector2 desiredStep = targetPosition - new Vector2(transform.position.x, transform.position.y);

        Vector2 desiredVelocity = desiredStep.normalized * _speed;

        Vector2 steeringForce = desiredVelocity - _directionMoving;
        _directionMoving += (steeringForce / _mass);
        this.transform.position += new Vector3(_directionMoving.x, _directionMoving.y, 0) * Time.deltaTime;

        if (Vector3.Distance(this.transform.position, pos) < 1f && DestinationReached != null)
            DestinationReached();
    }

    /// <summary>
    /// Rotating the boomerang
    /// </summary>
    void RotateAnimation()
    {
        transform.eulerAngles += new Vector3(0, 0, 1) * _rotationSpeed * Time.deltaTime;
    }

    /// <summary>
    /// Throws this boomerang to a certain position with a small bend in the direction its moving.
    /// </summary>
    /// <param name="aimPos"></param>
    public override void Throw(Vector3 aimPos)
    {
        _directionMoving = new Vector2(0, 5);
        _target = aimPos;
        DestinationReached += MoveBack;
    }

    /// <summary>
    /// Lets this move back to the hand of the player
    /// </summary>
    void MoveBack()
    {
        _returningToHand = true;
        _target = Vector3.zero;
        DestinationReached -= MoveBack;
        DestinationReached += StopMoving;
    }
    /// <summary>
    /// Stops the movement
    /// </summary>
    void StopMoving()
    {
        DestinationReached = null;
        _returningToHand = false;
        _target = Vector3.zero;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == Tags.ENEMY)
        {
            //TODO: Enemy function
        }
        else if(other.transform.tag == Tags.PUZZLEOBJECT)
        {
            //TODO: Puzzle function
        } else if(other.transform.tag != Tags.PLAYER)
        {
            StopMoving();
        }
    }
}
