using UnityEngine;
using System.Collections;

public class Boomerang : ThrowAble {
    public delegate void NormalDelegate();
    public event NormalDelegate DestinationReached;

    private Color _particleColor = Color.red;
    private int _colorFase = 0;
    private ParticleSystem _particleSystem;

    private Transform _grabbedObject;
    private GameObject _playerHand;

    private bool _isMoving = false;
    private bool _returningToHand = false;

    private Vector3 _target = Vector3.zero;

    private float _mass = 50;
    private float _speed = 30;
    private float _rotationSpeed = 400;

    private Vector2 _directionMoving = new Vector2(0, 1);

    void Start()
    {
        _particleColor.a = 0.5f;
        _playerHand = GameObject.FindGameObjectWithTag(Tags.PLAYERHAND);
        _playerHand.GetComponent<ThrowingHand>().CatchThrowable(this.gameObject);
        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystem.enableEmission = false;
    }

    void Update()
    {
        if (!GameController.Instance.isPaused)
        {
            if (_target != Vector3.zero) //here we check if there is a target
            {
                MoveTowardsPosition(_target); //using the movetowards function to go to the current position it's targeting at.
                RotateAnimation();
                RotateColor();
            }
            else if (_returningToHand)
            {
                MoveTowardsPosition(_playerHand.transform.position);
                RotateAnimation();
                RotateColor();
            }
            if (_grabbedObject != null)
            {
                _grabbedObject.position = this.transform.position;
            }
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
    /// Rotating the particle color
    /// </summary>
    void RotateColor()
    {
        switch(_colorFase)
        {
            case 0:
                if (_particleColor.g < 1)
                {
                    _particleColor.g += 0.01f;
                }
                else
                {
                    _colorFase++;
                }
                break;
            case 1:
                if (_particleColor.r > 0)
                {
                    _particleColor.r -= 0.01f;
                }
                else
                {
                    _colorFase++;
                }
                break;
            case 2:
                if (_particleColor.b < 1)
                {
                    _particleColor.b += 0.01f;
                }
                else
                {
                    _colorFase++;
                }
                break;
            case 3:
                if (_particleColor.g > 0)
                {
                    _particleColor.g -= 0.01f;
                }
                else
                {
                    _colorFase++;
                }
                break;
            case 4:
                if (_particleColor.r < 1)
                {
                    _particleColor.r += 0.01f;
                }
                else
                {
                    _colorFase++;
                }
                break;
            case 5:
                if (_particleColor.b > 0)
                {
                    _particleColor.b -= 0.01f;
                }
                else
                {
                    _colorFase = 0;
                }
                break;
        }
        _particleSystem.startColor = _particleColor;
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
        _isMoving = true;
        _particleSystem.enableEmission = true;
        _inHand = false;
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
        _isMoving = false;
        _particleSystem.enableEmission = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_isMoving && !_inHand)
        {
            
            if (other.transform.tag != Tags.PLAYER && other.transform.tag != Tags.PLAYERHAND)
            {
                if (other.GetComponent<GrabAble>()) //if the object touched has the grabable component.
                {
                    _grabbedObject = other.transform; // grab the object
                    other.GetComponent<GrabAble>().GrabObject();
                }
                else if (other.GetComponent<PuzzleObject>() && (other.GetComponent<PuzzleObject>().isActivateAble && other.GetComponent<PuzzleObject>().isBoomerangActivateAble)) //checks if the puzzle is activate able by the boomerang
                {
                    other.GetComponent<PuzzleObject>().Activate();
                }
                else if(other.GetComponent<BreakAbleProp>())
                {
                    other.GetComponent<BreakAbleProp>().Break();
                }
                else if (other.transform.tag == Tags.ENEMY) //if the enemy gets hit by the boomerang
                {
                    other.GetComponent<Enemy>().GetHit();
                }
                else
                {
                    StopMoving(); //stops the boomerang if it touched something different
                }
            }
        }
    }
}
