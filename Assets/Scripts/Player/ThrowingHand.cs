using UnityEngine;
using System.Collections;

public class ThrowingHand : HandOrbit {
    private float _catchCooldown = 1;
    private float _currentCatchCooldown;

    private GameObject _throwObject;
    private BoomerangTrajectory _boomerangTrajectory;

    void Awake()
    {
        _boomerangTrajectory = gameObject.GetComponent<BoomerangTrajectory>();

        OnAimingToClose += DisableTrajectory;
        OnAiming += EnableTrajectory;
    }

	protected override void Update () {
        base.Update();
        if(_throwObject != null)
        {
            _throwObject.transform.position = Vector3.Lerp(_throwObject.transform.position, this.transform.position, 100 * Time.deltaTime);
            _throwObject.transform.rotation = this.transform.rotation;
        }
    }

    /// <summary>
    /// Disable the trajectory of the certain throwable object
    /// </summary>
    void DisableTrajectory()
    {
        if (_throwObject != null && _throwObject.GetComponent<Boomerang>())
        {
            _boomerangTrajectory.enabled = false;
        }
    }

    /// <summary>
    /// Enable the trajectory of the certain throwable object
    /// </summary>
    void EnableTrajectory()
    {
        if (_throwObject != null && _throwObject.GetComponent<Boomerang>())
        {
            _boomerangTrajectory.enabled = true;
        }
    }

    /// <summary>
    /// triggers when a item is catched
    /// </summary>
    /// <param name="throwObject"></param>
    public void CatchThrowable(GameObject throwObject)
    {
        if (_currentCatchCooldown <= Time.time)
        {
            _throwObject = throwObject;
            _throwAction.ThrowObjectAction += ThrowObject;
            _throwObject.GetComponent<ThrowAble>().inHand = true;
            if (_throwObject.GetComponent<Boomerang>())
            {
                _boomerangTrajectory.enabled = true;
            }
        }
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

            if (_throwObject.GetComponent<Boomerang>())
                _boomerangTrajectory.enabled = false;

            _throwObject.GetComponent<ThrowAble>().Throw(aimPos);
            _throwObject = null;
            _currentCatchCooldown = Time.time + _catchCooldown;
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
