using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private TouchDetector2D _touchDetector;

    void Start() {
        //adding all the player components
        gameObject.AddComponent<TouchDetector2D>();
        gameObject.AddComponent<PlayerInput>();
        gameObject.AddComponent<Movement>();

        _touchDetector.TouchStarted += OnTouchStarted;
        _touchDetector.OnTouch += OnTouchStay;
        _touchDetector.TouchEnded += OnTouchExit;
	}

    /// <summary>
    /// Checks in which direction you start to touch a object.
    /// </summary>
    /// <param name="other"></param>
    /// <param name="dir"></param>
    void OnTouchStarted(GameObject other, Vector2 dir)
    {
        if(dir == Vector2.down)
        {
            if(other.transform.tag == Tags.ENEMY)
            {
                ///other.GetComponent<Enemy>().GetHit();
            }
        }
    }

    /// <summary>
    /// Checks in wich direction you stay on touching a object.
    /// </summary>
    /// <param name="other"></param>
    /// <param name="dir"></param>
    void OnTouchStay(GameObject other, Vector2 dir)
    {

    }

    /// <summary>
    /// Checks in wich direction you are trying exit while touching a object.
    /// </summary>
    /// <param name="other"></param>
    /// <param name="dir"></param>
    void OnTouchExit(GameObject other, Vector2 dir)
    {

    }

    /// <summary>
    /// When player dies
    /// </summary>
	void Death () {
	
	}
    /// <summary>
    /// When player gets hit
    /// </summary>
    void GetHit()
    {
        Death();
    }
}
