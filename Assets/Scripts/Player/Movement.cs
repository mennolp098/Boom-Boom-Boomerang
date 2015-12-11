using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    private bool _onGround = false;
    private bool _hasDoubleJumped = false;
    private bool _autoMove = false;

    private float _dampSpeed = 0f;
    private float _speed = 5f;
    private float _jumpForce = 10f;
    private int _touchingDir = 0;
    private int _autoMoveDir = 1;

    private Rigidbody2D _rigidbody;
    private TouchDetector2D _touchDetector;
    private PlayerInput _playerInput;

    void Awake()
    {
        //getting all the required components
        _rigidbody = GetComponent<Rigidbody2D>();
        _touchDetector = GetComponent<TouchDetector2D>();
        _playerInput = GetComponent<PlayerInput>();
    }

	void Start ()
    {
        //adding touch events
        _touchDetector.TouchStarted += OnTouchStarted;
        _touchDetector.OnTouch += OnTouchStay;
        _touchDetector.TouchEnded += OnTouchExit;
        
        //adding keypressed events
        _playerInput.RightKeyPressed += Move;
        _playerInput.LeftKeyPressed += Move;
        _playerInput.JumpKeyPressed += Jump;

        GetComponent<Player>().OnWin += OnLevelCompleted;
	}

    private void OnLevelCompleted()
    {
        _autoMove = true;
        _playerInput.RightKeyPressed -= Move;
        _playerInput.LeftKeyPressed -= Move;
        _playerInput.JumpKeyPressed -= Jump;
    }

	private void Update()
    {
        if (!GameController.Instance.isPaused)
        {
            //Using dampspeed so the character will run smoothly and stop smoothly
            if (_dampSpeed > 0)
            {
                _dampSpeed -= 0.085f;
                if (_dampSpeed < 0)
                {
                    _dampSpeed = 0;
                }
            }
            //auto move to right
            if (_autoMove)
            {
                Move(_autoMoveDir);
            }
        }
    }

    /// <summary>
    /// Lets the character move in a certain direction with a certain amount of speed.
    /// </summary>
    /// <param name="dir"></param>
	private void Move (int dir)
    {
        if (!GameController.Instance.isPaused)
        {
            if (dir != _touchingDir)
            {
                if (_dampSpeed < _speed)
                {
                    _dampSpeed += 0.5f;
                }
                Vector3 movement = new Vector3(1, 0, 0);
                movement.x *= dir;
                _rigidbody.transform.position += movement * _dampSpeed * Time.deltaTime;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * dir, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    /// <summary>
    /// Lets the character jump with jumpforce
    /// </summary>
    private void Jump()
    {
        if (!GameController.Instance.isPaused)
        {
            if (_onGround)
            {
                _rigidbody.velocity += new Vector2(0, _jumpForce);
            }
            else if (!_hasDoubleJumped)
            {
                _rigidbody.velocity += new Vector2(0, _jumpForce / 2f);
                _hasDoubleJumped = true;
            }
        }
    }

    /// <summary>
    /// Checks in which direction you start to touch a object.
    /// </summary>
    /// <param name="other"></param>
    /// <param name="dir"></param>
    private void OnTouchStarted(GameObject other, Vector2 dir)
    {
        if(dir == Vector2.down)
        {
            if (other.transform.tag == Tags.GROUND)
            {
                _onGround = true;
                _hasDoubleJumped = false;
            }
        }
        if (dir == Vector2.left)
        {
            if (other.transform.tag != Tags.PLAYER && other.transform.tag != Tags.THROWABLE && other.transform.tag != Tags.PUZZLEOBJECT)
            {
                _touchingDir = -1;
            }
        }
        if (dir == Vector2.right)
        {
            if (other.transform.tag != Tags.PLAYER && other.transform.tag != Tags.THROWABLE && other.transform.tag != Tags.PUZZLEOBJECT)
            {
                _touchingDir = 1;
            }
        }
    }

    /// <summary>
    /// Checks in which direction you exit a object.
    /// </summary>
    /// <param name="other"></param>
    /// <param name="dir"></param>
    private void OnTouchExit(GameObject other, Vector2 dir)
    {
        if (dir == Vector2.down)
        {
            if (other.transform.tag == Tags.GROUND)
            {
                _onGround = false;
            }
        }
        if (dir == Vector2.left || dir == Vector2.right)
        {
            if (other.transform.tag != Tags.PLAYER && other.transform.tag != Tags.THROWABLE && other.transform.tag != Tags.PLAYERHAND && other.transform.tag != Tags.PUZZLEOBJECT)
            {
                _touchingDir = 0;
            }
        }
    }

    /// <summary>
    /// Checks in wich direction you stay on touching a object.
    /// </summary>
    /// <param name="other"></param>
    /// <param name="dir"></param>
    private void OnTouchStay(GameObject other, Vector2 dir)
    {
    }
}
