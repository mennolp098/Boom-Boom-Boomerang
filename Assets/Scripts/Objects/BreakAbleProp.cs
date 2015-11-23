using UnityEngine;
using System.Collections;

public class BreakAbleProp : MonoBehaviour {
    public float dropAmount;

    private ObjectPool _objectPool;

    void Awake()
    {
        _objectPool = GameObject.FindGameObjectWithTag(Tags.GAMECONTROLLER).GetComponent<ObjectPool>();
    }

	public void Break()
    {
        for(int i = 0; i < dropAmount; i++)
        {
            GameObject newCoin = _objectPool.GetObjectForType("Coin", false) as GameObject;
            newCoin.transform.position = this.transform.position;
            Rigidbody2D coinRigidbody = newCoin.GetComponent<Rigidbody2D>();
            coinRigidbody.gravityScale = 1;
            coinRigidbody.velocity += new Vector2(Random.Range(-5, 5), Random.Range(0, 10));
        }
        Destroy(this.gameObject);
    }
}
