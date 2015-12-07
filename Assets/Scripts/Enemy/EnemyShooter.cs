using UnityEngine;
using System.Collections;

public class EnemyShooter : EnemyRed 
{

	private BoxCollider2D _playerDetection;

	void Start()
	{
		_playerDetection = gameObject.AddComponent<BoxCollider2D> () as BoxCollider2D;
		_playerDetection.isTrigger = true;
		_playerDetection.size = new Vector2 (10, 10);
	}


	public override void Update()
	{
		base.Update ();
		//check when player is close
	}

	public override void OnTouchStarted(GameObject other, Vector2 dir)
	{
		if (other.gameObject.tag == "player") 
		{
			Shoot();
		}
	}

	public override void Move()
	{
		base.Move ();
	}

	private void Shoot()
	{
		Debug.Log ("shoot player");
	}


}
