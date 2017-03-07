using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour {
	
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
    private float knockDur;
    private float knockbackPowr;
    private Vector3 knockbackDir;

    public bool MovementDisabled { get { return this.movementDisabled; } set { this.movementDisabled = value; } }

	// Use this for initialization
	void Start () {
        if (GameObject.FindGameObjectsWithTag("Player").Length >= 2)
        {
            Destroy(GameObject.FindGameObjectsWithTag("Player")[1]);
        }
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
	}

	void Update()
	{
		anim.SetBool("grounded", grounded);
		//anim.SetBool("isDying", isDying);
		anim.SetFloat("speed", Mathf.Abs(rb2d.velocity.x));
		
		//Rotation of the Player
        if (!movementDisabled)
        {
			if (Input.mousePosition.x < Screen.width/2)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

			if (Input.mousePosition.x > Screen.width/2)
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

    public void StartKnockback(float knockDur, float knockbackPowr, Vector3 knockbackDir)
    {
        this.knockDur = knockDur;
        this.knockbackPowr = knockbackPowr;
        this.knockbackDir = knockbackDir;
        StartCoroutine("Knockback");
    }

    public IEnumerator Knockback()
    {
        float timer = 0;

        while(knockDur > timer)
        {
            timer += Time.deltaTime;
            //rb2d.AddForce(new Vector3(knockbackDir.x * -100, 10 *  knockbackPowr, transform.position.z));
            rb2d.AddForce(new Vector3(knockbackDir.x * -100, knockbackDir.y * knockbackPowr, transform.position.z));
        }

        yield return 0;
    }
}
