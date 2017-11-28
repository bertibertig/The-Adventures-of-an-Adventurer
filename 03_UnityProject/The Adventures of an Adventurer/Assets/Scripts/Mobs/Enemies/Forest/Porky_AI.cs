using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porky_AI : MonoBehaviour {

    public float maxSpeed;

    private bool hasSeenPlayer = false;
    private Rigidbody2D rb2d;
    private Animator anim;

	// Use this for initialization
	void Start () {
        GetComponentInChildren<PlayerChecker>().PlayerEnter += Porky_AI_PlayerEnter;
        GetComponentInChildren<PlayerChecker>().PlayerExiting += Porky_AI_PlayerExiting;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine("NormalMovement");
	}

    private void Porky_AI_PlayerExiting(object sender, System.EventArgs e)
    {
        hasSeenPlayer = false;
        anim.SetBool("attack", false);
        StartCoroutine("NormalMovement");
        print("Leave Player");
    }

    private void Porky_AI_PlayerEnter(object sender, System.EventArgs e)
    {
        hasSeenPlayer = true;
        StopCoroutine("NormalMovement");
        rb2d.velocity = new Vector2(0, 0);
        anim.SetBool("attack", true);
        print("Following Player");
    }

    IEnumerator NormalMovement()
    {
        do
        {
            for (int i = 0; i < 100; i++)
            {
                if(!hasSeenPlayer)
                    rb2d.AddForce(Vector2.left * 50);
                ControlMaxSpeed();
                yield return null;
            }
            yield return new WaitForSeconds(1.5f);
            rb2d.velocity = new Vector2(0, 0);

            for (int i = 0; i < 100; i++)
            {
                if (!hasSeenPlayer)
                    rb2d.AddForce(Vector2.right * 50);
                ControlMaxSpeed();
                yield return null;
            }
            yield return new WaitForSeconds(1.5f);
            rb2d.velocity = new Vector2(0, 0);

        } while (!hasSeenPlayer);
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
}
