using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    private bool _onGround = false;
    private bool _hasDoubleJumped = false;

    private float _dampSpeed = 0f;
    private float _speed = 5f;
    private float _jumpForce = 5f;

    private Rigidbody2D _rigidbody;
    private TouchDetector2D _touchDetector;
    private PlayerInput _playerInput;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _touchDetector = GetComponent<TouchDetector2D>();
        _playerInput = GetComponent<PlayerInput>();
    }

	void Start ()
    {
        _touchDetector.TouchStarted += OnTouchStarted;
        _touchDetector.OnTouch += OnTouchStay;
        _touchDetector.TouchEnded += OnTouchExit;
        _playerInput.RightKeyPressed += Move;
        _playerInput.LeftKeyPressed += Move;
        _playerInput.JumpKeyPressed += Jump;
	}

	private void Update()
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
    }

    /// <summary>
    /// Lets the character move in a certain direction with a certain amount of speed.
    /// </summary>
    /// <param name="dir"></param>
	private void Move (int dir)
    {
        if (_dampSpeed < _speed)
        {
            _dampSpeed += 0.5f;
        }
        Vector3 movement = new Vector3(1, 0, 0);
        movement.x *= dir;
        _rigidbody.transform.position += movement * _dampSpeed * Time.deltaTime;
        transform.localScale = new Vector3(transform.localScale.x * dir, transform.localScale.y, transform.localScale.z);
    }

    /// <summary>
    /// Lets the character jump with jumpforce
    /// </summary>
    private void Jump()
    {
        if (_onGround)
        {
            _rigidbody.velocity += new Vector2(0, _jumpForce);
        }
        else if(!_hasDoubleJumped)
        {
            _rigidbody.velocity += new Vector2(0, _jumpForce / 1.25f);
            _hasDoubleJumped = true;
        }
    }

    /// <summary>
    /// Checks in which direction you touch a object.
    /// </summary>
    /// <param name="other"></param>
    /// <param name="dir"></param>
    private void OnTouchStarted(GameObject other, Vector2 dir)
    {
        Debug.Log(dir + " " + other.transform.tag);
        if(dir == Vector2.down)
        {
            Debug.Log(other.transform.tag);
            if (other.transform.tag == Tags.GROUND)
            {
                _onGround = true;
                _hasDoubleJumped = false;
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
