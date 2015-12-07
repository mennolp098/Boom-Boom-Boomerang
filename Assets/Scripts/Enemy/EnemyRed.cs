using UnityEngine;
using System.Collections;

public class EnemyRed : Enemy 
{
	private int _moveDirection = -1;
	private float _platformWidth;
	private Transform _platformTransform;

	public override void Update()
	{
		base.Update();
	}
	//Enemy movement
	//Moves to the other side when at the end of a platform.
	public override void Move ()
	{
		base.Move ();

		if (_platformTransform != null)
		{
			if(this.transform.position.x < _platformTransform.transform.position.x - _platformWidth/2 + 0.5f)
			{
				Debug.Log("Right");
				_moveDirection = 1;
			} 
			else if(this.transform.position.x > _platformTransform.transform.position.x + _platformWidth/2 - 0.5f)
			{
				Debug.Log("Left");
				_moveDirection = -1;

			}
		} else if (_platformTransform = null) 
		{
			this.transform.Translate (Vector2.down * 0 * Time.deltaTime);
		}
		transform.Translate (Vector2.right * _moveDirection * _moveSpeed * Time.deltaTime);
	}
	//Death
	public override void Death()
	{
		base.Death ();	
	}

	//Gets the width(x) of the platform that the enemy hits
	public override void OnTouchStarted(GameObject other, Vector2 dir)
	{
		base.OnTouchStarted(other, dir);

        if (dir == Vector2.down)
        {
            if (other.transform.tag == Tags.PLATFORM)
            {
                //Getting the collider size of the touched platform
                Collider2D collidedObject = other.GetComponent<Collider2D>();
                float platformWidth = collidedObject.bounds.size.x;

                _platformWidth = platformWidth;
                _platformTransform = other.transform;
            }
        }
        if(dir == Vector2.right || dir == Vector2.left)
        {
            if (other.transform.tag != Tags.PLAYER)
                _moveDirection *= -1;
        }
	}
}
