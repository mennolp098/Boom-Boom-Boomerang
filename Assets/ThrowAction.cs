using UnityEngine;
using System.Collections;

public class ThrowAction : MonoBehaviour {
    public delegate void Vector3Delegate(Vector3 value);
    public event Vector3Delegate ThrowObjectAction;

    public Transform hand;

    private PlayerInput _playerInput;

    void Awake ()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

	void Start () {
        _playerInput.MouseButtonPressed += ThrowObject;
	}
	
    /// <summary>
    /// Action to throw current object holding.
    /// </summary>
	void ThrowObject ()
    {
	    if (ThrowObjectAction != null)
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 10f;
            pos = Camera.main.ScreenToWorldPoint(pos);

            ThrowObjectAction(pos);
            //TODO: Make script for throwable object.
        }
	}
}
