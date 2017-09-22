using UnityEngine;
using System.Collections;

public class Enemy_Movement_AI : Photon.MonoBehaviour {

    public float maxSpeed;
    public float speed;
    public float maxWayX;
    public float maxWayY;
    public float movementTimeout;
    public float jumpPower;
    public float lerpStep = 0.1f;

    private float goneWayX;
    private float goneWayY;
    private Vector3 oldPosition;
    private Vector3 newPosition;
    private int counter;

    //Multiplayer
    private Vector2 newPos;
    private Vector2 newVelocity;

    public bool Died { get; set; }
    public bool InMotion { get; set; }
    public Ground_Check GroundTrigger { get; set; }

    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start()
    {
        if (speed <= 0)
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
        Died = false;
        InMotion = false;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        GroundTrigger = gameObject.GetComponentInChildren<Ground_Check>();
        StartCoroutine("Move");
    }

    private void FixedUpdate()
    {
        Vector3 velocity2D = new Vector3(newVelocity.x, newVelocity.y, 0);
        transform.position = Vector3.Lerp(rb2d.position, newPos, lerpStep) + velocity2D * Time.deltaTime;
    }

    public IEnumerator Move()
    {
		while (!Died || !gameObject.GetComponentInChildren<Enemy_CheckForPlayer>().HasSeenPlayer) {
			do {
                if (GroundTrigger.Gronded || !gameObject.GetComponentInChildren<Enemy_CheckForPlayer>().HasSeenPlayer)
                {
                    rb2d.AddForce(Vector2.up * jumpPower);
                    rb2d.AddForce((Vector2.right * speed));
                    ControlMaxSpeed();
                    counter++;
                    yield return new WaitForSeconds(2);
                }
                yield return new WaitForSeconds(0.1f);
            } while (counter < 2);

            if (GroundTrigger.Gronded || !gameObject.GetComponentInChildren<Enemy_CheckForPlayer>().HasSeenPlayer)
            {
                rb2d.AddForce(Vector2.up * (jumpPower * 2));
                rb2d.AddForce((Vector2.right * speed));
                ControlMaxSpeed();
            }
            yield return new WaitForSeconds(1);

            counter = 0;
			yield return new WaitForSeconds (1);
			do {
                if (GroundTrigger.Gronded || !gameObject.GetComponentInChildren<Enemy_CheckForPlayer>().HasSeenPlayer)
                {
                    rb2d.AddForce(Vector2.up * jumpPower);
                    rb2d.AddForce((Vector2.left * speed));
                    ControlMaxSpeed();
                    counter++;
                    yield return new WaitForSeconds(2);
                }
                yield return new WaitForSeconds(0.1f);
            } while (counter < 2);

            if (GroundTrigger.Gronded || !gameObject.GetComponentInChildren<Enemy_CheckForPlayer>().HasSeenPlayer)
            {
                rb2d.AddForce(Vector2.up * (jumpPower * 2));
                rb2d.AddForce((Vector2.left * speed));
                ControlMaxSpeed();
            }
            yield return new WaitForSeconds(1);
            counter = 0;
            yield return new WaitForSeconds(1);
		}
        print("CoRoutine exited");
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

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {
            stream.SendNext(rb2d.position);
            stream.SendNext(rb2d.velocity);
        }
        else if(stream.isReading)
        {
            newPos = (Vector2)stream.ReceiveNext();
            newVelocity = (Vector2)stream.ReceiveNext();
        }
    }
}