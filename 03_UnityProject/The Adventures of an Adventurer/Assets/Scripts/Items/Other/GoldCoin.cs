using UnityEngine;
using System.Collections;

public class GoldCoin : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<PolygonCollider2D>(), GetComponent<PolygonCollider2D>());
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("GoldCoin").GetComponent<PolygonCollider2D>(), GetComponent<PolygonCollider2D>());
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if(!col.gameObject.CompareTag("Ground") && col.gameObject.tag != "")
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), GetComponent<PolygonCollider2D>());
    }

}
