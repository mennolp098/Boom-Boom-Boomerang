using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public int moveSpeed =1;

    protected ObjectPool _objectPool;
    protected int _dropAmount = 5;

    private TouchDetector2D _touchDetector;
    private bool _isHit;
    private float _currentRecoverTime;
    private float _recoverTime = 2;

    void Awake()
	{
		_touchDetector = gameObject.AddComponent<TouchDetector2D>();

		_touchDetector.TouchStarted += OnTouchStarted;
		_touchDetector.OnTouch += OnTouchStay;
		_touchDetector.TouchEnded += OnTouchExit;

		_objectPool = GameObject.FindGameObjectWithTag(Tags.GAMECONTROLLER).GetComponent<ObjectPool>();
	}
	
	//Overridable Update
	public virtual void Update()
	{
        if (!_isHit)
        {
            Move();
        }
        else
        {
            CheckRecoverHit();
        }
	}


	//if the enemy hits the player from the left, right and down side
	public virtual void OnTouchStarted(GameObject other, Vector2 dir)
	{
        if(dir == Vector2.left || dir == Vector2.right || dir == Vector2.down)
        {
            if(other.transform.tag == Tags.PLAYER)
            {
                other.GetComponent<Player>().GetHit();
            }
        }
	}
	//Base OntouchStay
	void OnTouchStay(GameObject other, Vector2 dir)
	{

	}
	//Base onTouchExit
	void OnTouchExit(GameObject other, Vector2 dir)
	{

	}
	//Base Enemy Idle
	void Idle()
	{
		_moveSpeed = 0;
	}

    //if a enemy gets hit reset recover time
    public virtual void GetHit()
    {
        if(!_isHit)
        {
            _isHit = true;
            _currentRecoverTime = Time.time + _recoverTime;
        }
    }

    //checks if this enemy can recover from being hit.
    private void CheckRecoverHit()
    {
        if (_currentRecoverTime < Time.time)
        {
            _isHit = false;
        }
    }


	//If a enemy dies it will be send back to the Objectpool.
	//Overridable death
	public virtual void Death()
	{
        //_deathParticle.transform = this.gameObject.transform;
        DropCoins(_dropAmount);
        _objectPool.PoolObject (this.gameObject);
    }

	//Overridable enemy movement
	public virtual void Move()
	{
	}

    public void DropCoins(int dropAmount)
    {
        for (int i = 0; i < dropAmount; i++)
        {
            GameObject newCoin = _objectPool.GetObjectForType("Coin", false) as GameObject;
            newCoin.transform.position = this.transform.position + new Vector3(0,1,0);
            Rigidbody2D coinRigidbody = newCoin.GetComponent<Rigidbody2D>();
            coinRigidbody.gravityScale = 1;
            coinRigidbody.velocity += new Vector2(Random.Range(-10, 10), Random.Range(0, 10));
        }
    }
}
