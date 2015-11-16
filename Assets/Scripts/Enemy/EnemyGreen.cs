using UnityEngine;
using System.Collections;

public class EnemyGreen : Enemy 
{
	void Start() 
	{

	}

	public override void Update()
	{
		base.Update();
	}
	public override void Move ()
	{
		base.Move ();
		transform.Translate (Vector2.left * _moveSpeed * Time.deltaTime);
	}



	void OnTouchStay()
	{
	
	}

	void OnTouchExit()
	{

	}

}
