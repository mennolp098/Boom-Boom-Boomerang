using UnityEngine;
using System.Collections;

public class Door : PuzzleObject {
    private float _moveSpeed = 1;

    private Transform _moveToObject;
    private Vector3 _movePosition;

    private bool _activated = false;

    void Awake()
    {
        //get child GameObject to check where the object is positioned
        _moveToObject = transform.GetChild(0);
        _movePosition = _moveToObject.position;
        _moveToObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public override void Activate()
    {
        base.Activate();
        Debug.Log("activated");
        _activated = true;
    }

    void Update()
    {
        if(_activated)
        {
            //If the door is activated then move towards the move object.
            Debug.Log("derp");
            this.transform.position = Vector3.Lerp(this.transform.position, _movePosition, _moveSpeed * Time.deltaTime);
        }
    }
}
