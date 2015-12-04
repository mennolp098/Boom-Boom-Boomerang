using UnityEngine;
using System.Collections;

public class AdditionalTextFunctions : MonoBehaviour {
	private RectTransform _rectTransform;
	private bool _ableToMove = false;
	void Awake()
	{
		_rectTransform = GetComponent<RectTransform>();
	}
    //setting a boolean
	public void SetAbleToMove(bool ableToMove)
	{
		_ableToMove = ableToMove;
	}
    //setting the rect position via world position
	public void SetPosition(Vector3 pos)
	{
		Vector3 rectPos = Camera.main.WorldToScreenPoint(pos);
		_rectTransform.position = rectPos;
	}
    //setting a reset time
	public void SetResetTime(float time)
	{
		Invoke ("PoolMyself", time);
	}
	private void PoolMyself()
	{
		GameObject.FindGameObjectWithTag(Tags.GAMECONTROLLER).GetComponent<ObjectPool>().PoolObject(this.gameObject);
	}
	void Update () 
	{
        //just a small movement
		if(_ableToMove)
		{
			Vector3 movement = _rectTransform.position;
			movement.x += 0.3f;
			movement.y += 0.3f;
			_rectTransform.position = movement;
		}
	}
}
