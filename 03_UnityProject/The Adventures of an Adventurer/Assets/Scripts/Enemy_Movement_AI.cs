using UnityEngine;
using System.Collections;

public class Enemy_Movement_AI : MonoBehaviour {

    public float maxSpeed;
    public float speed;
    public float maxWayX;
    public float maxWayY;
    public float movementTimeout;
    public float jumpPower;

    private float goneWayX;
    private float goneWayY;
    private Vector3 oldPosition;
    private Vector3 newPosition;
    private int counter;

    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start()
    {
        if(speed <= 0)
            speed = 70f;
        if(maxSpeed <= 0)
            maxSpeed = 0.1f;
        if(maxWayX <= 0)
            maxWayX = 50;
        if (maxWayY <= 0)
            maxWayY = 20;
        if (movementTimeout <= 0)
            movementTimeout = 2f;
        if (jumpPower < 0)
            jumpPower = 0;

        goneWayX = 0;
        goneWayY = 0;
        counter = 0;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine("Move");
    }

    public IEnumerator Move()
    {
		while (true) {
			do {
				rb2d.AddForce (Vector2.up * jumpPower);
				rb2d.AddForce ((Vector2.right * speed));
				ControlMaxSpeed ();
				counter++;
				yield return new WaitForSeconds (2);
			} while (counter < 2);

			rb2d.AddForce (Vector2.up * (jumpPower * 2));
			rb2d.AddForce ((Vector2.right * speed));
			ControlMaxSpeed ();
			yield return new WaitForSeconds (2);

			counter = 0;
			yield return new WaitForSeconds (1);
			do {
				rb2d.AddForce (Vector2.up * jumpPower);
				rb2d.AddForce ((Vector2.left * speed));
				ControlMaxSpeed ();
				counter++;
				yield return new WaitForSeconds (2);
			} while (counter < 2);

			rb2d.AddForce (Vector2.up * (jumpPower * 2));
			rb2d.AddForce ((Vector2.left * speed));
			ControlMaxSpeed ();
			yield return new WaitForSeconds (2);

			counter = 0;
		}
    }

    public void ControlMaxSpeed()
    {
        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }

        //MaxSpeed of the enemy
        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }
    }

    public void StopMoveCoroutine()
    {
        StopCoroutine("Move");
    }

    public void StartMoveCoroutine()
    {
        StartCoroutine("Move");
    }
}