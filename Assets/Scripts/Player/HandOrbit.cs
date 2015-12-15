using UnityEngine;
using System.Collections;

public class HandOrbit : MonoBehaviour {
    public delegate void NormalDelegate();
    public event NormalDelegate OnAimingToClose;
    public event NormalDelegate OnAiming;

    public bool reversedDirection;
    
    protected ThrowAction _throwAction;
    protected GameObject _player;
    protected Vector3 _currentAnimPos;

    protected float _standardY = 0.75f;
    protected float _animTime = 0.1f;
    protected float _animSpeed = 100;
    protected float _currentAnimTime;

    protected bool _throwing;


    private float _distanceY = 125;
    private float _distanceX = 150;
    private Vector3 _standardPos;

    void Start()
    {
        _throwAction = gameObject.AddComponent<ThrowAction>();
        _player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        _throwAction.ThrowObjectAction += HandAnim;
        _standardPos = this.transform.localPosition;
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

    protected virtual void Update()
    {
        if (!GameController.Instance.isPaused)
        {
            if (!_throwing)
            {
                OrbitHand();
            }
            else if (_currentAnimTime > Time.time)
            {
                ThrowAnimation();
            }
            else
            {
                _throwing = false;
            }
        }
    }
    private void ThrowAnimation()
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
        {
            newHandPos.x *= -1;
        }

        if (reversedDirection)
        {
            newHandPos.y *= -1;
            newHandPos.x *= -1;
        }
        newHandPos.y += _standardY;
        this.transform.localPosition = newHandPos;
    }
    private void OrbitHand()
    {
        //Rotates the hand
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 playerPos = Camera.main.WorldToScreenPoint(_player.transform.position);
        if (Input.mousePosition.x - playerPos.x > _distanceX || Input.mousePosition.x - playerPos.x < -_distanceX || Input.mousePosition.y - playerPos.y > _distanceY || Input.mousePosition.y - playerPos.y < -_distanceY)
        {
            Vector3 dir = Input.mousePosition - pos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Vector3 distance = dir - _player.transform.position;
            Vector3 newHandPos = distance.normalized;

            if (_player.transform.localScale.x != 1)
                newHandPos.x *= -1;

            if (reversedDirection)
            {
                newHandPos.y *= -1;
                newHandPos.x *= -1;
            }

            newHandPos.y += _standardY;
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, newHandPos, 20 * Time.deltaTime);

            if (OnAiming != null)
                OnAiming();
        }
        else
        {
            if (OnAimingToClose != null)
                OnAimingToClose();
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, _standardPos, 5 * Time.deltaTime);
        }
    }
}
