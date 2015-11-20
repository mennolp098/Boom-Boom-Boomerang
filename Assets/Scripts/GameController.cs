using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    private float _score;
    public float score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            //update scoreboard
        }
    }
}
