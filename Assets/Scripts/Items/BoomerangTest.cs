using UnityEngine;
using System.Collections;

public class BoomerangTest : MonoBehaviour
{
    public Transform playerHand;

    private GameObject _player;
    private bool _inPlayersHand = true;
    private float duration = 1; // in seconds

    private Vector3 beginPoint = new Vector3(0, 0, 0);
    private Vector3 finalPoint = new Vector3(0, 0, 30);
    private Vector3 farPoint = new Vector3(0, 0, 0);

    private float startTime;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        _player.GetComponent<ThrowAction>().ThrowObjectAction += SetTargetPosition;
    }

    void Update()
    {
        if(!_inPlayersHand)
        {
            Movement();
        }
        else
        {
            this.transform.position = playerHand.position;
        }
    }
    void SetTargetPosition(Vector3 pos)
    {
        beginPoint = this.transform.position;
        finalPoint = pos;
        _inPlayersHand = false;
        startTime = Time.time;
        _player.GetComponent<ThrowAction>().ThrowObjectAction -= SetTargetPosition;
    }
    void Movement()
    {
        Vector3 center = (beginPoint + finalPoint) * 0.5F;
        center -= farPoint;

        Vector3 riseRelCenter = beginPoint - center;
        Vector3 setRelCenter = finalPoint - center;

        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, (Time.time - startTime) / duration);
        transform.position += center;

        if(Vector3.Distance(this.transform.position,finalPoint) < 1f && finalPoint != playerHand.position)
        {
            SetTargetPosition(playerHand.position);
        }
        else if(Vector3.Distance(this.transform.position, finalPoint) < 1f)
        {
            _inPlayersHand = true;
            _player.GetComponent<ThrowAction>().ThrowObjectAction += SetTargetPosition;
        }
    }
}