using UnityEngine;
using System.Collections;

public class enemy_slime : MonoBehaviour {

    public float health;
    public float minGoldDrop;
    public float maxGoldDrop;
    public float xp;
    public float maxSpeed;
    public float speed;
    public float maxWay;
    public float goneWay;

    private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        minGoldDrop = 2;
        maxGoldDrop = 3;
        health = 50;
        xp = 10;
        speed = 20;
        maxSpeed = 2;
        maxWay = 50;
        goneWay = 0;

        rb2d = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void FixedUpdate()
    {
        StartCoroutine("MoveSlime");
    }

    public IEnumerator MoveSlime()
    {
        float h = Random.Range(-1, 1);
        if (goneWay < maxWay)
        {
            rb2d.AddForce((Vector2.right * speed) * h);
            goneWay = 0;
        }

        if (goneWay >= maxWay)
        {
            rb2d.AddForce((Vector2.left * speed) * h);
        }

        goneWay += speed * h;

        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }

        //MaxSpeed of the enemy
        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }
        yield return new WaitForSeconds(.1f);
    }
}
