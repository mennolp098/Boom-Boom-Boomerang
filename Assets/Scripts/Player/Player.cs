using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public delegate void NormalDelegate();
    public delegate void IntDelegate(int value);
    public event NormalDelegate OnDeath;
    public event NormalDelegate OnWin;
    public event IntDelegate OnGoldCoinCatched;

    public delegate void Vector3Delegate(Vector3 value);
    public event Vector3Delegate OnCheckpointTouched;

    private TouchDetector2D _touchDetector;
    private ObjectPool _objectPool;

    private Rigidbody2D _rigidbody;

    void Awake()
    {
        //adding all the player components
        _touchDetector = gameObject.AddComponent<TouchDetector2D>();
        gameObject.AddComponent<PlayerInput>();
        gameObject.AddComponent<Movement>();

        //getting components
        _rigidbody = GetComponent<Rigidbody2D>();
        _objectPool = GameObject.FindGameObjectWithTag(Tags.GAMECONTROLLER).GetComponent<ObjectPool>();
    }

    void Start()
    {
        //event handlers for touchdetection
        _touchDetector.TouchStarted += OnTouchStarted;
        _touchDetector.OnTouch += OnTouchStay;
        _touchDetector.TouchEnded += OnTouchExit;
	}
    void OnCollisionEnter2D(Collision2D other)
    {
        //checks if you can grab a object
        if (other.transform.GetComponent<GrabAble>())
        {
            if (other.transform.tag == Tags.GOLDCOIN && OnGoldCoinCatched != null)
                OnGoldCoinCatched(other.transform.GetComponent<GoldCoin>().goldCoinIndex);

            other.transform.GetComponent<GrabAble>().ObjectCatched(); //fire the function from the grabable object
            _objectPool.PoolObject(other.gameObject); //pool the object

            //if the object is not pooled then delete it
            if (other.gameObject.activeInHierarchy)
                Destroy(other.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //checks if it is a puzzle object
        if(other.transform.tag == Tags.PUZZLEOBJECT)
        {
            if(other.transform.GetComponent<PuzzleObject>().isActivateAble)
            {
                other.transform.GetComponent<PuzzleObject>().Activate();
            }
        }
        //if you hit the endpoint then send the message onwin
        if (other.transform.tag == Tags.ENDPOINT)
        {
            if (OnWin != null)
                OnWin();
        }
        if(other.transform.tag == Tags.BOTTOMBORDER)
        {
            Death();
        }
    }

    /// <summary>
    /// Checks in which direction you start to touch a object.
    /// </summary>
    /// <param name="other"></param>
    /// <param name="dir"></param>
    void OnTouchStarted(GameObject other, Vector2 dir)
    {
        if(dir == Vector2.down)
        {
            if(other.transform.tag == Tags.ENEMY)
            {
                other.GetComponent<Enemy>().Death();
                _rigidbody.velocity += new Vector2(0, 2);
            }
            if(other.transform.tag == Tags.CHECKPOINT)
            {
                if (OnCheckpointTouched != null)
                    OnCheckpointTouched(other.transform.position);
            }
        }
    }

    /// <summary>
    /// Checks in wich direction you stay on touching a object.
    /// </summary>
    /// <param name="other"></param>
    /// <param name="dir"></param>
    void OnTouchStay(GameObject other, Vector2 dir)
    {

    }

    /// <summary>
    /// Checks in wich direction you are trying exit while touching a object.
    /// </summary>
    /// <param name="other"></param>
    /// <param name="dir"></param>
    void OnTouchExit(GameObject other, Vector2 dir)
    {

    }

    /// <summary>
    /// Player death function
    /// </summary>
	void Death ()
    {
        if (OnDeath != null)
            OnDeath();
	}

    /// <summary>
    /// When player gets hit
    /// </summary>
   public void GetHit()
    {
        //pause movement until death anim is done
        //TODO: start anim + check anim playtime
        GetComponent<Movement>().enabled = false;
        Invoke("Death", 0.5f);
    }
}
