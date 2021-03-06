﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    //Floats
    public float maxSpeed = 3;
    public float speed = 50f;
    public float jumpPower = 400f;

    //Bools
    public bool grounded;
    public bool isDying = false;
    public bool canDoubleJump;

    //References
    private Rigidbody2D rb2d;
    private Animator anim;

    //Inventory
    private Inventory inventory;
    private bool showInventory;

    // Use this for initialization
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        inventory = gameObject.GetComponentInParent<Inventory>();
        showInventory = true;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("grounded", grounded);
        anim.SetBool("isDying", isDying);
        anim.SetFloat("speed", Mathf.Abs(rb2d.velocity.x));

        //Rotation of the Player
        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        //Jumping /Dobble Jumping
        if (Input.GetButtonDown("Jump"))
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

        //Inventory
        if (Input.GetButtonDown("Inventory"))
        {
            inventory.ShowInventory(showInventory);
            showInventory = !showInventory;
        }
    }

    void FixedUpdate()
    {
        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f;
        easeVelocity.x *= 0.75f;

        float h = Input.GetAxis("Horizontal");

        //Disables Sliding of the Player
        if (grounded)
        {
            rb2d.velocity = easeVelocity;
        }

        //Movement of the player
        rb2d.AddForce((Vector2.right * speed) * h);

        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }

        //MaxSpeed of the player
        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }

        
    }

    public IEnumerator Knockback(float knockDur, float knockbackPowr, Vector3 knockbackDir)
    {
        float timer = 0;

        while(knockDur > timer)
        {
            timer += Time.deltaTime;

            rb2d.AddForce(new Vector3(knockbackDir.x * -100, knockbackDir.y * knockbackPowr, transform.position.z));
        }

        yield return 0;
    }
}
