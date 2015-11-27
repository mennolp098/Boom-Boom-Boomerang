using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public int _moveSpeed =1;
	private TouchDetector2D _touchDetector;
	protected ObjectPool _objectPool;



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
		Move ();
	}


	//if the player hits the enemy on the head
	public virtual void OnTouchStarted(GameObject other, Vector2 dir)
	{
		if (dir == Vector2.down) 
		{
			if(other.transform.tag == Tags.PLAYER)
			{
				Death();
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

	//If a enemy dies it will be send back to the Objectpool.
	//Overridable death
	public virtual void Death()
	{
		//_deathParticle.transform = this.gameObject.transform;
		_objectPool.PoolObject (this.gameObject);
	}

	//Overridable enemy movement
	public virtual void Move()
	{
	}
}
