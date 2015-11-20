using UnityEngine;
using System.Collections;

public class OtherHand : MonoBehaviour {
    private ThrowAction _throwAction;
    private GameObject _player;
    private Vector3 _currentAnimPos;

    private float _standardY = 0.75f;
    private float _animTime = 0.25f;
    private float _animSpeed = 75;
    private float _currentAnimTime;

    private bool _throwing;

    void Start()
    {
        _throwAction = gameObject.AddComponent<ThrowAction>();
        _player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        _throwAction.ThrowObjectAction += HandAnim;
    }
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
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
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
        else if(_currentAnimTime > Time.time)
        {
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
