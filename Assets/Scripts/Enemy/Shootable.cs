using UnityEngine;
using System.Collections;

public class Shootable : MonoBehaviour 
{
	private float _timer = 5;//seconds
	private float _speed = 7.5f;//speed of the projectile
	protected ObjectPool _objectPool;
	
	void Awake()
	{
		_objectPool = GameObject.FindGameObjectWithTag(Tags.GAMECONTROLLER).GetComponent<ObjectPool>();
	}
	
	public virtual void Update () 
	{
		this.transform.Translate (Vector2.left * _speed * Time.deltaTime);

		_timer -= Time.deltaTime;

		if (_timer <= 0) {
			Death();
		}

	}

	//Death, when the object "dies" he will be sent back to the objectpool
	public virtual void Death()
	{
		_objectPool.PoolObject (this.gameObject);
	}
}
