using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    private int _shakeAmount = 0;
    private float _shakeRadius = 2;

    public void StartShaking(int amount, float intensity = 0.5f)
    {
        _shakeAmount = amount;
        _shakeRadius = intensity;
        InvokeRepeating("Shake", 0f, 0.01f);
    }

    private void Shake()
    {
        if (_shakeAmount > 0)
        {
            
            float shakeIntensity = Random.value * _shakeRadius * 2 - _shakeRadius;
            Vector3 pp = this.transform.position;
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
    public void StopShaking()
    {
        CancelInvoke("Shake");
    }
}
