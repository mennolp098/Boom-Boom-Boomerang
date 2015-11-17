using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    private Transform _player;
    private float _maxX = 2;
    private float _minX = -2;
    private float _maxY = 2;
    private float _minY = -2;
    void Awake ()
    {
        _player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
    }

	void Update () {
        transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, _player.position.x + _minX, _player.position.x + _maxX),
            Mathf.Clamp(this.transform.position.y, _player.position.y + _minY, _player.position.y + _maxY),
            -10);
    }
}
