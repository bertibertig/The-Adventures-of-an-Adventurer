using UnityEngine;
using System.Collections;
using System;

public class Player_Movement : Photon.MonoBehaviour
{

    //Floats
    public float maxSpeed = 3;
    public float speed = 50f;
    public float jumpPower = 400f;

    //Bools
    public bool grounded;
    public bool isDying = false;
    public bool canDoubleJump;
    public bool isAbleToJump = false;

    //References
    private Rigidbody2D rb2d;
    private Animator anim;
    private bool movementDisabled;
    private bool knockbackCoroutineStarted = false;
    private bool ungroundedAfterKnockbackStarted = false;
    private bool turnToMovement;
    private float knockbackPower;
    private Vector2 playerScreenPosition;
    private Vector2 enemyScreenPosition;
    private Vector2 addForceForMovePlayer;

    //Events
    public event EventHandler MovementToPositionEndedHandler;

    GameObject player;
    new Camera camera;

    public bool MovementDisabled { get { return this.movementDisabled; } set { this.movementDisabled = value; } }

    private void MovementToPositionEnded()
    {
        print("Notifieing Subscribers (MovementToPositionEnded)");
        if (MovementToPositionEndedHandler != null)
            MovementToPositionEndedHandler(this, null);
    }

    // Use this for initialization
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (GameObject.FindGameObjectsWithTag("Player").Length >= 2)
        {
            Destroy(GameObject.FindGameObjectsWithTag("Player")[1]);
        }
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    void OnLevelWasLoaded()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        anim.SetBool("grounded", grounded);
        anim.SetFloat("speed", Mathf.Abs(rb2d.velocity.x));

        //Rotation of the Player
        if (!movementDisabled)
        {
            if (Input.mousePosition.x < camera.WorldToScreenPoint(player.transform.localPosition).x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (Input.mousePosition.x > camera.WorldToScreenPoint(player.transform.localPosition).x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        //Jumping /Double Jumping
        if (Input.GetButtonDown("Jump") && isAbleToJump && !movementDisabled)
        {
            if (grounded)
            {
                rb2d.AddForce(Vector2.up * jumpPower);
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                canDoubleJump = false;
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                rb2d.AddForce(Vector2.up * (jumpPower * 0.8f));
            }
        }

        if (knockbackCoroutineStarted && !grounded)
        {
            ungroundedAfterKnockbackStarted = true;
            MovementDisabled = true;
        }

        if (ungroundedAfterKnockbackStarted && grounded)
        {
            movementDisabled = false;
            knockbackCoroutineStarted = false;
            ungroundedAfterKnockbackStarted = false;
        }
    }

    public bool GetGrounded()
    {
        return grounded;
    }

    void FixedUpdate()
    {
        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f;
        easeVelocity.x *= 0.75f;

        float h = Input.GetAxis("Horizontal");

        //Disables Sliding of the Player
        if (grounded && !movementDisabled)
        {
            rb2d.velocity = easeVelocity;
        }

        //Movement of the player
        if (!movementDisabled)
        {
            rb2d.AddForce((Vector2.right * speed) * h);
            ControlMaxSpeed();
        }
    }

    private void ControlMaxSpeed()
    {
        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }

        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }
    }

    public void StartKnockback(float knockbackPowr, Vector3 playerPos, Vector3 enemyPos)
    {
        this.knockbackPower = knockbackPowr;

        Vector3 playerScreenPos = camera.WorldToScreenPoint(playerPos);
        this.playerScreenPosition = new Vector2(playerScreenPos.x, playerScreenPos.y);

        Vector3 enemyScreenPos = camera.WorldToScreenPoint(enemyPos);
        this.enemyScreenPosition = new Vector2(enemyScreenPos.x, enemyScreenPos.y);

		rb2d.velocity = Vector2.zero;
        StartCoroutine("Knockback");
    }

    public IEnumerator Knockback()
    {
        knockbackCoroutineStarted = true;

        rb2d.AddForce(transform.up * knockbackPower);
        if (playerScreenPosition.x < enemyScreenPosition.x)
        {
            rb2d.AddForce(transform.right * knockbackPower * -1.2f);
        }
        else if (playerScreenPosition.x > enemyScreenPosition.x)
        {
            rb2d.AddForce(transform.right * knockbackPower * 1.2f);
        }
        else
        {
        }

        yield return 0;
    }

    public void MovePlayerToPosition(Vector2 endPosition, bool _turnToMovement = true)
    {
        this.turnToMovement = _turnToMovement;
        StartCoroutine("MovePlayerToPositionCoroutine", endPosition);
    }

    private IEnumerator MovePlayerToPositionCoroutine(Vector2 endPosition)
    {
        bool arrived = false;
        Vector2 orientation;
        if (endPosition.x < gameObject.transform.position.x)
            orientation = Vector2.left;
        else
            orientation = Vector2.right;
        do
        {
            MovementDisabled = true;
            rb2d.AddForce(orientation * speed);
            ControlMaxSpeed();
            yield return null;
            if (endPosition.x >= this.gameObject.transform.position.x)
                arrived = true;
        } while (!arrived);
        MovementToPositionEnded();
        MovementDisabled = false;
    }
}