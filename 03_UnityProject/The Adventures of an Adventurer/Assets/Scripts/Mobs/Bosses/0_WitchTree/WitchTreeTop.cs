using UnityEngine;
using System.Collections;

public class WitchTreeTop : MonoBehaviour {

    private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<PolygonCollider2D>(), GetComponent<PolygonCollider2D>());
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("GoldCoin").GetComponent<PolygonCollider2D>(), GetComponent<PolygonCollider2D>());
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        rb2d.AddForce(new Vector2((1.5f),1),ForceMode2D.Impulse);
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Ground") && col.gameObject.tag != "")
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), GetComponent<PolygonCollider2D>());
    }
}
