using UnityEngine;
using System.Collections;

public class ThrowingHand : MonoBehaviour {
    private ThrowAction _throwAction;
    private GameObject _player;
    private GameObject _throwObject;
    private float standardY = 0.75f;
    void Start()
    {
        _throwAction = gameObject.AddComponent<ThrowAction>();
        _player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
    }
	void Update () {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Vector3 distance = dir - _player.transform.position;
        Vector3 newHandPos = distance.normalized;

        if(_player.transform.localScale.x != -1)
            newHandPos.x *= -1;

        newHandPos.y *= -1;
        newHandPos.y += standardY;
        this.transform.localPosition = newHandPos;

        if (_throwObject != null)
        {
            _throwObject.transform.position = this.transform.position;
            _throwObject.transform.rotation = this.transform.rotation;
        }
    }

    public void CatchThrowable(GameObject throwObject)
    {
        _throwObject = throwObject;
        _throwAction.ThrowObjectAction += ThrowObject;
    }

    public void ThrowObject(Vector3 aimPos)
    {
        if (_throwObject != null)
        {
            _throwAction.ThrowObjectAction -= ThrowObject;
            _throwObject.GetComponent<ThrowAble>().Throw(aimPos);
            _throwObject = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == Tags.THROWABLE)
        {
            CatchThrowable(other.gameObject);
        }
    }
}
