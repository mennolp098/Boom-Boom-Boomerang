﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public delegate void NormalDelegate();
    public event NormalDelegate OnDeath;

    public delegate void Vector3Delegate(Vector3 value);
    public event Vector3Delegate OnCheckpointTouched;

    private TouchDetector2D _touchDetector;
    private ObjectPool _objectPool;

    void Awake()
    {
        //adding all the player components
        _touchDetector = gameObject.AddComponent<TouchDetector2D>();
        gameObject.AddComponent<PlayerInput>();
        gameObject.AddComponent<Movement>();

        //getting components
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
        if(other.transform.GetComponent<GrabAble>())
        {

            other.transform.GetComponent<GrabAble>().ObjectCatched();
            _objectPool.PoolObject(other.gameObject);
            if (other.gameObject.activeInHierarchy)
                Destroy(other.gameObject);
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
