using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoomerangTrajectory : MonoBehaviour {
    private float _mass = 50;
    private float _speed = 30;

    private Vector2 _directionMoving = new Vector2(0, 5);

    private List<Vector3> _stepPositions = new List<Vector3>();

    private LineRenderer _lineRenderer;

    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void OnDisable()
    {
        _lineRenderer.enabled = false;
    }

    void OnEnable()
    {
        _lineRenderer.enabled = true;
    }

    void Update()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 10f;
        pos = Camera.main.ScreenToWorldPoint(pos);
        CalculateSteps(pos);
    }

    void CalculateSteps(Vector3 pos)
    {
        _directionMoving = new Vector2(0, 5);
        //_stepPositions.Clear();
        Vector3 stepPosition = this.transform.position;
        int steps = 40;
        int lineSteps = 0;
        for (int i = 0; i < steps; i++)
        {
            Vector2 targetPosition = pos;

            Vector2 desiredStep = targetPosition - new Vector2(transform.position.x, transform.position.y);

            Vector2 desiredVelocity = desiredStep.normalized * _speed;

            Vector2 steeringForce = desiredVelocity - _directionMoving;
            _directionMoving += (steeringForce / _mass);
            stepPosition += new Vector3(_directionMoving.x, _directionMoving.y, 0) * 0.019f;
            if (i % 2 == 0)
            {
                _lineRenderer.SetPosition(lineSteps, stepPosition);
                lineSteps++;
            }
        }
    }
}
