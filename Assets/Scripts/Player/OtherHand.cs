using UnityEngine;
using System.Collections;

public class OtherHand : MonoBehaviour {
    private ThrowAction _throwAction;
    private GameObject _player;
    private Vector3 _currentAnimPos;

    private float _standardY = 0.75f;
    private float _animTime = 0.1f;
    private float _animSpeed = 100;
    private float _currentAnimTime;

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

    void Update()
    {
        if (!_throwing)
        {
            //Rotates the hand in the negative direction of the throwing hand
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(_player.transform.position);
            if (Vector3.Distance(playerScreenPos, Input.mousePosition) > 150)
            {
                Vector3 dir = Input.mousePosition - pos;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                Vector3 distance = dir - _player.transform.position;
                Vector3 newHandPos = distance.normalized;
                if (_player.transform.localScale.x != 1)
                    newHandPos.x *= -1;

                newHandPos.y += _standardY;
                this.transform.localPosition = newHandPos;
            }
        }
        else if(_currentAnimTime > Time.time)
        {
            //the throw animation
            _currentAnimPos.y -= _animSpeed;
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = _currentAnimPos - pos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Vector3 distance = dir - _player.transform.position;
            Vector3 newHandPos = distance.normalized;
            if (_player.transform.localScale.x != 1)
                newHandPos.x *= -1;

            newHandPos.y += _standardY;
            this.transform.localPosition = newHandPos;
        }
        else
        {
            _throwing = false;
        }
    }
}
