using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    private int _shakeAmount = 0;
    private float _shakeRadius = 2;
    private Transform _playerTransform;

    void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
    }

    /// <summary>
    /// Shakes screen with a certain amount and intensity
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="intensity"></param>
    public void StartShaking(int amount, float intensity = 0.5f)
    {
        _shakeAmount = amount;
        _shakeRadius = intensity;
        InvokeRepeating("Shake", 0f, 0.01f);
    }

    /// <summary>
    /// Shakes the camera
    /// </summary>
    private void Shake()
    {
        if (_shakeAmount > 0)
        {
            
            float shakeIntensity = Random.value * _shakeRadius * 2 - _shakeRadius;
            Vector3 pp = _playerTransform.position;
            pp.y += shakeIntensity;
            pp.x += shakeIntensity;
            this.transform.position = pp;
            _shakeAmount--;
        }
        else
        {
            StopShaking();
        }
    }

    /// <summary>
    /// Stops the invoking of the shake function
    /// </summary>
    public void StopShaking()
    {
        CancelInvoke("Shake");
    }
}
