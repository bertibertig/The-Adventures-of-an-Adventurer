using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour {

    [Header("Cloudspeed, Negative => Move righ, Positive => Move Left")]
    public float cloudSpeed = -3;
    [Header("Seconds the Cloud waits untill it moves again.")]
    public float cloudWait = 3;
    public float maxSpeed = 3;

    private Rigidbody2D rb2d;

    public float CloudEnd { get; set; }

    void Start () {
        rb2d = this.GetComponent<Rigidbody2D>();
        StartCoroutine("MoveCloud");
	}

    IEnumerator MoveCloud()
    {
        do
        {
            ControlMaxSpeed();
            
            yield return new WaitForSeconds(cloudWait);
        } while (rb2d.transform.position.x >= CloudEnd);
    }

    public void ControlMaxSpeed()
    {
        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }

        else if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }
        else
        {
            rb2d.AddForce(new Vector2(cloudSpeed, 0));
        }
    }
}
