using UnityEngine;
using System.Collections;

public class Door : PuzzleObject {
    private Collider2D _collider;
    void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }
    public override void Activate()
    {
        base.Activate();
        _collider.enabled = false;
    }
}
