using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public int _moveSpeed =1;

	private TouchDetector2D _touchDetectorEnemy;


	void Start()
	{
		_touchDetectorEnemy = GetComponent<TouchDetector2D>();

		_touchDetectorEnemy.TouchStarted += OnTouchStarted;
		_touchDetectorEnemy.OnTouch += OnTouchStay;
		_touchDetectorEnemy.TouchEnded += OnTouchExit;
	}
	
	
	public virtual void Update()
	{
		Move ();
	}

	//Overridable enemy movement
	public virtual void Move()
	{
	}
	
	void OnTouchStarted(GameObject other, Vector2 dir)
	{
		if (dir == Vector2.down) 
		{
			if(other.transform.tag == Tags.PLAYER)
			{
				Death();
			}
		}

	}

	void Idle()
	{
		_moveSpeed = 0;
	}


	void OnTouchStay(GameObject other, Vector2 dir)
    {

	}

	void OnTouchExit(GameObject other, Vector2 dir)
    {

	}


	void Death()
	{

	}
}
