using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyShooter : EnemyRed 
{

	private BoxCollider2D _playerDetection;
	private List<GameObject> detectedObjects = new List<GameObject>();

	private GameObject _shootable;
	private float _timer = 5;

	public override void Update()
	{
		base.Update ();
		Timer ();
		
	}

	void Timer()
	{
		_timer -= Time.deltaTime;

		if (_timer <= 0) {
			Detect();
			_timer = 5;
		}
	}

	public override void OnTouchStarted(GameObject other, Vector2 dir)
	{
		base.OnTouchStarted (other, dir);
	}

	//Detecting objects within the drawn collider
	private void Detect()
	{
		Vector2 a = new Vector2 (this.gameObject.transform.position.x - 5, this.gameObject.transform.position.y + 5);
		Vector2 b = new Vector2 (this.gameObject.transform.position.x + 5, this.gameObject.transform.position.y - 5);
		Collider2D[] itemsInCollider = Physics2D.OverlapAreaAll (a, b);



		foreach (Collider2D col in itemsInCollider) 
		{
			if(col.tag == "Player" || _timer <= 0)
			{
				this.transform.LookAt(col.transform);
				Shoot();
			}
		}
	}

	public override void Move()
	{
		base.Move ();
	}

	private void Shoot()
	{
		Debug.Log ("shoot player");
		_shootable = _objectPool.GetObjectForType ("Bullet", false) as GameObject;
	}
}
