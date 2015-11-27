﻿using UnityEngine;
using System.Collections;

public class ThrowingHand : MonoBehaviour {
    private ThrowAction _throwAction;
    private GameObject _player;
    private GameObject _throwObject;
    private Vector3 _currentAnimPos;

    private float _animTime = 0.1f;
    private float _currentAnimTime;
    private float _animSpeed = 100;
    private float _standardY = 0.75f;

    private bool _throwing;
    void Start()
    {
        _throwAction = gameObject.AddComponent<ThrowAction>();
        _player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        _throwAction.ThrowObjectAction += HandAnim;
    }
    /// <summary>
    /// Starts animation if item is thrown
    /// </summary>
    /// <param name="mousepos"></param>
    void HandAnim(Vector3 mousepos)
    {
        _throwing = true;
        _currentAnimPos = Input.mousePosition;
        _currentAnimTime = Time.time + _animTime;
    }
	void Update () {
        if (!_throwing)
        {
            //rotates the hand in the direction of the mouse
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = Input.mousePosition - pos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Vector3 distance = dir - _player.transform.position;
            Vector3 newHandPos = distance.normalized;

            if (_player.transform.localScale.x != -1)
                newHandPos.x *= -1;

            newHandPos.y *= -1;
            newHandPos.y += _standardY;
            this.transform.localPosition = newHandPos;
        }
        else if (_currentAnimTime > Time.time)
        {
            //the throw animation
            _currentAnimPos.y -= _animSpeed;

            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = _currentAnimPos - pos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Vector3 distance = dir - _player.transform.position;
            Vector3 newHandPos = distance.normalized;
            if (_player.transform.localScale.x != -1)
                newHandPos.x *= -1;

            newHandPos.y *= -1;
            newHandPos.y += _standardY;
            this.transform.localPosition = newHandPos;
        }
        else
        {
            _throwing = false;
        }
        if (_throwObject != null)
        {
            _throwObject.transform.position = this.transform.position;
            _throwObject.transform.rotation = this.transform.rotation;
        }
    }

    /// <summary>
    /// triggers when a item is catched
    /// </summary>
    /// <param name="throwObject"></param>
    public void CatchThrowable(GameObject throwObject)
    {
        _throwObject = throwObject;
        _throwAction.ThrowObjectAction += ThrowObject;
    }


    /// <summary>
    /// throws a throwable object
    /// </summary>
    /// <param name="aimPos"></param>
    public void ThrowObject(Vector3 aimPos)
    {
        if (_throwObject != null)
        {
            _throwAction.ThrowObjectAction -= ThrowObject;
            _throwObject.GetComponent<ThrowAble>().Throw(aimPos);
            _throwObject = null;
        }
    }

    /// <summary>
    /// catching a throwable object
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == Tags.THROWABLE)
        {
            CatchThrowable(other.gameObject);
        }
    }
}
