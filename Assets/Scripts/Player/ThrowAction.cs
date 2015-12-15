using UnityEngine;
using System.Collections;

public class ThrowAction : MonoBehaviour {
    public delegate void Vector3Delegate(Vector3 value);
    public event Vector3Delegate ThrowObjectAction;

    private bool _ableToThrow = true;
    private PlayerInput _playerInput;

    void Awake ()
    {
        _playerInput = GetComponentInParent<PlayerInput>();
    }

	void Start () {
        _playerInput.MouseButtonPressed += ThrowObject;

        HandOrbit _handOrbitScript = GameObject.FindGameObjectWithTag(Tags.PLAYERHAND).GetComponent<HandOrbit>();
        _handOrbitScript.OnAiming += EnableThrowing;
        _handOrbitScript.OnAimingToClose += DisableThrowing;

    }
	
    void EnableThrowing()
    {
        _ableToThrow = true;
    }
    void DisableThrowing()
    {
        _ableToThrow = false;
    }

    /// <summary>
    /// Action to throw current object holding.
    /// </summary>
	void ThrowObject ()
    {
        if (!GameController.Instance.isPaused)
        {
            if (ThrowObjectAction != null && _ableToThrow)
            {
                Vector3 pos = Input.mousePosition;
                pos.z = 10f;
                pos = Camera.main.ScreenToWorldPoint(pos);

                ThrowObjectAction(pos);
                //TODO: Make script for throwable object.
            }
        }
	}
}
