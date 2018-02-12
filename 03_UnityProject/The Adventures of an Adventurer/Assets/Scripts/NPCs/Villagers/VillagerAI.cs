using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerAI : MonoBehaviour {

    public bool runningEnabled = true;
    [Header("Position where the villager should stop Running")]
    public Vector3 endPos = new Vector2(0,0);
    [Header("Running Speed")]
    public float speed = 3;
    [Header("Fastest Speed the villager runs")]
    public float maxSpeed = 3;

    private Vector2 dirNormalized;
    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start ()
    {
        dirNormalized = (endPos - transform.position).normalized;
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        IgnorePlayerColliders();
        if (runningEnabled)
        {
            StartCoroutine("Run");
        }
	}

    private IEnumerator Run()
    {
        do
        {
            if (Vector2.Distance(endPos, transform.position) <= 1)
                runningEnabled = false;
            else
                ControlMaxSpeedX();
            yield return new WaitForFixedUpdate();
        } while (runningEnabled);
    }

    private void ControlMaxSpeedX()
    {
        if (rb2d.velocity.x > maxSpeed)
            rb2d.velocity = new Vector2(maxSpeed,0);
        if (rb2d.velocity.x < -maxSpeed)
            rb2d.velocity = new Vector2(-maxSpeed, 0);
        else
        {
            float xSpeed = transform.position.x + dirNormalized.x * speed * Time.deltaTime;
            rb2d.AddForce(new Vector2(xSpeed, 0));
        }
    }

    public void IgnorePlayerColliders()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(this.GetComponent<PolygonCollider2D>(), player.GetComponent<PolygonCollider2D>());
        Physics2D.IgnoreCollision(this.GetComponent<PolygonCollider2D>(), player.GetComponents<CircleCollider2D>()[0]);
        Physics2D.IgnoreCollision(this.GetComponent<PolygonCollider2D>(), player.GetComponents<CircleCollider2D>()[1]);
    }
}
