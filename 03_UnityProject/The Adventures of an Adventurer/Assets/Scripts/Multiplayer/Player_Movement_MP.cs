using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement_MP : Photon.MonoBehaviour {

    //Floats
    public float maxSpeed = 3;
    public float speed = 50f;
    public float jumpPower = 400f;
    public float lerpStep = 0.1f;
    private float knockbackPower;

    //Bools
    public bool multiplayerOn = false;
    public bool grounded = false;
    public bool isDying = false;
    public bool canDoubleJump;
    public bool isAbleToJump = false;
    private bool knockbackCoroutineStarted = false;
    private bool ungroundedAfterKnockbackStarted = false;


    //References
    private Rigidbody2D rb2d;
    private Animator anim;
    private bool movementDisabled;
    private float knockDur;
    private float knockbackPowr;
    private Vector3 knockbackDir;


    //Multiplayer Variables
    public Vector2 newVelocity = Vector2.zero;
    private Vector3 newPos;
    public Quaternion newRotation;

    GameObject player;
    new Camera camera;
    private Vector2 playerScreenPosition;
    private Vector2 enemyScreenPosition;

    public bool MovementDisabled { get { return this.movementDisabled; } set { this.movementDisabled = value; } }

    // Use this for initialization
    void Start()
    {
        if (!multiplayerOn)
            PhotonNetwork.offlineMode = true;
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camera_follow>() != null)
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camera_follow>().Player = this.gameObject;
        if (GameObject.FindGameObjectsWithTag("Player").Length >= 2)
        {
            //Destroy(GameObject.FindGameObjectsWithTag("Player")[1]);
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
        if (this.photonView.isMine)
        {
            //Rotation of the Player
            if (!movementDisabled)
            {
                if (Input.mousePosition.x < camera.WorldToScreenPoint(player.transform.localPosition).x)
                {
                    newRotation = Quaternion.Euler(0, 180, 0);
                }

                if (Input.mousePosition.x > camera.WorldToScreenPoint(player.transform.localPosition).x)
                {
                    newRotation = Quaternion.Euler(0, 0, 0);
                }
                transform.rotation = newRotation;
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
    }

    public bool GetGrounded()
    {
        return grounded;
    }

    void FixedUpdate()
    {
        if (this.photonView.isMine)
        {
            Vector3 easeVelocity = rb2d.velocity;
            easeVelocity.y = rb2d.velocity.y;
            easeVelocity.z = 0.0f;
            easeVelocity.x *= 0.75f;

            float h = Input.GetAxis("Horizontal");

            if (multiplayerOn)
            {
                this.photonView.RPC("OnMovement", PhotonTargets.All);
                anim.SetFloat("speed", Mathf.Abs(rb2d.velocity.x));
            }
            else
            {
                anim.SetBool("grounded", grounded);
                //anim.SetBool("isDying", isDying);
                anim.SetFloat("speed", Mathf.Abs(rb2d.velocity.x));
            }

            //Disables Sliding of the Player
            if (grounded && !movementDisabled)
            {
                rb2d.velocity = easeVelocity;
            }

            //Movement of the player
            if (!movementDisabled)
            {
                rb2d.AddForce((Vector2.right * speed) * h);

                if (rb2d.velocity.x > maxSpeed)
                {
                    rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
                }
            }

            //MaxSpeed of the player
            if (rb2d.velocity.x < -maxSpeed)
            {
                rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
            }
        }
        else
        {
            Vector3 velocity2D = new Vector3(newVelocity.x, newVelocity.y, 0);
            transform.position = Vector3.Lerp(transform.position, newPos, lerpStep) + velocity2D * Time.deltaTime;
            transform.rotation = newRotation;
        }
    }

    public void StartKnockback(float knockbackPowr, Vector3 playerPos, Vector3 enemyPos)
    {
        this.knockbackPower = knockbackPowr;

        Vector3 playerScreenPos = camera.WorldToScreenPoint(playerPos);
        this.playerScreenPosition = new Vector2(playerScreenPos.x, playerScreenPos.y);

        Vector3 enemyScreenPos = camera.WorldToScreenPoint(enemyPos);
        this.enemyScreenPosition = new Vector2(enemyScreenPos.x, enemyScreenPos.y);

        StartCoroutine("Knockback");
    }

    public IEnumerator Knockback()
    {
        knockbackCoroutineStarted = true;

        rb2d.AddForce(transform.up * knockbackPower);
        if (playerScreenPosition.x < enemyScreenPosition.x)
        {
            print("left");
            rb2d.AddForce(transform.right * knockbackPower * -1.2f);
        }
        else if (playerScreenPosition.x > enemyScreenPosition.x)
        {
            print("right");
            rb2d.AddForce(transform.right * knockbackPower * 1.2f);
        }

        yield return new WaitForSeconds(1);
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        InizialiseComponents();
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(this.rb2d.velocity);
            stream.SendNext(transform.rotation);
        }
        else if (stream.isReading)
        {
            newPos = (Vector3)stream.ReceiveNext();
            newVelocity = (Vector2)stream.ReceiveNext();
            newRotation = (Quaternion)stream.ReceiveNext();
        }
    }

    [PunRPC]
    void OnMovement()
    {
        InizialiseComponents();
        if (anim != null && rb2d != null)
        {
            anim.SetBool("grounded", grounded);
            //anim.SetBool("isDying", isDying);
            anim.SetFloat("speed", Mathf.Abs(newVelocity.x));
        }
    }

    void InizialiseComponents()
    {
        if (anim == null)
            anim = gameObject.GetComponent<Animator>();
        if (rb2d == null)
            rb2d = gameObject.GetComponent<Rigidbody2D>();
    }
}
