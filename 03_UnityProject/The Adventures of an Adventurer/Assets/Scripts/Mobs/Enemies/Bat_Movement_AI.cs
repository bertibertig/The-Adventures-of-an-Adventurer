using UnityEngine;
using System.Collections;

public class Bat_Movement_AI : Photon.MonoBehaviour
{
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
    private bool hasSeenPlayer;

    //Multiplayer
    private Vector2 newPos;
    private Vector2 newVelocity;

    public bool Died { get; set; }
    public bool InMotion { get; set; }

    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start()
    {
        if (speed <= 0)
            speed = 70f;
        if (maxSpeed <= 0)
            maxSpeed = 0.1f;
        if (maxWayX <= 0)
            maxWayX = 50;
        if (maxWayY <= 0)
            maxWayY = 20;

        hasSeenPlayer = false;
        goneWayX = 0;
        goneWayY = 0;
        counter = 0;
        Died = false;
        InMotion = false;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine("Move");
    }

    private void FixedUpdate()
    {
        Vector3 velocity2D = new Vector3(newVelocity.x, newVelocity.y, 0);
        transform.position = Vector3.Lerp(rb2d.position, newPos, lerpStep) + velocity2D * Time.deltaTime;
    }

    public IEnumerator Move()
    {
        yield return null;
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
        hasSeenPlayer = true;
        StopCoroutine("Move");
    }

    public void StartMoveCoroutine()
    {
        hasSeenPlayer = false;
        StartCoroutine("Move");
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(rb2d.position);
            stream.SendNext(rb2d.velocity);
        }
        else if (stream.isReading)
        {
            newPos = (Vector2)stream.ReceiveNext();
            newVelocity = (Vector2)stream.ReceiveNext();
        }
    }
}