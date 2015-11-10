using UnityEngine;
using System.Collections;

public class Boomerang : MonoBehaviour {

    private Transform _player;
    private bool _inPlayersHand = true;
    private Vector3 _target = Vector3.zero;
    private float _mass = 100;
    private float _speed = 12;
    private Vector2 _directionMoving = new Vector2(0, 1);

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
        _player.GetComponent<ThrowAction>().ThrowObjectAction += SetTargetPosition;
    }

    void Update()
    {
        
        if (_target != Vector3.zero)
        {
            MoveTowardsPosition(_target);
            if (Vector3.Distance(this.transform.position, _target) < 1f)
            {
                _target = Vector3.zero;
            }
        }
        else if(!_inPlayersHand)
        {
            MoveTowardsPosition(_player.position);
            if (Vector3.Distance(this.transform.position, _player.position) < 1f)
            {
                _inPlayersHand = true;
                _player.GetComponent<ThrowAction>().ThrowObjectAction += SetTargetPosition;
            }
        } else
        {
            this.transform.position = _player.position;
        }
    }

    /// <summary>
    /// Lets this move towards a position via mass and steering
    /// </summary>
    void MoveTowardsPosition(Vector3 pos)
    {
        Vector2 targetPosition = pos;

        Vector2 desiredStep = targetPosition - new Vector2(transform.position.x, transform.position.y);

        Vector2 desiredVelocity = desiredStep.normalized * _speed;

        Vector2 steeringForce = desiredVelocity - _directionMoving;
        _directionMoving += (steeringForce / _mass);
        this.transform.position += new Vector3(_directionMoving.x, _directionMoving.y, 0) * Time.deltaTime;
    }

    void SetTargetPosition(Vector3 position)
    {
        _inPlayersHand = false;
        _target = position;
        _player.GetComponent<ThrowAction>().ThrowObjectAction -= SetTargetPosition;
    }
}
