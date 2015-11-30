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
	//Green Enemy movement
	public override void Move ()
	{
		base.Move ();
		transform.Translate (Vector2.left * _moveSpeed * Time.deltaTime);
	}

	//Green Enemy specific death function
	public override void Death()
	{
		base.Death ();
	}

	void OnTouchStay()
	{
	
	}

	void OnTouchExit()
	{

	}

}
