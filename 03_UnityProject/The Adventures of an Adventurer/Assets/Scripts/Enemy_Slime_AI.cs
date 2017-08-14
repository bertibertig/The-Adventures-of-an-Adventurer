using UnityEngine;
using System.Collections;

public class Enemy_Slime_AI : MonoBehaviour {

    public float health;
    public float minGoldDrop;
    public float maxGoldDrop;
    public float xp;
    public float maxSpeed;
    public float speed;
    public float maxWayX;
    public float maxWayY;
    public float movementTimeout;

    private float goneWayX;
    private float goneWayY;
    private Vector3 oldPosition;
    private Vector3 newPosition;
    private int counter;

    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start()
    {
        if(minGoldDrop <= 0)
            minGoldDrop = 2;
        if(maxGoldDrop <= 0)
            maxGoldDrop = 3;
        if(health <= 0)
            health = 50;
        if(xp <= 0)
            xp = 10;
        if(speed <= 0)
            speed = 20;
        if(maxSpeed <= 0)
            maxSpeed = 2;
        if(maxWayX <= 0)
            maxWayX = 50;
        if (maxWayY <= 0)
            maxWayY = 20;
        if (movementTimeout <= 0)
            movementTimeout = 2f;

        goneWayX = 0;
        goneWayY = 0;
        counter = 0;

        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("MoveSlime");
    }

    void FixedUpdate()
    {
        
    }

    public IEnumerator MoveSlime()
    {
        while (counter < 3)
        {
            rb2d.AddForce((Vector2.right * speed) * 1);
            ControlMaxSpeed();
            counter++;
            Debug.Log(counter);
            yield return new WaitForSeconds(500000f);
        }
        Debug.Log("Exited");
        counter = 0;
        yield return new WaitForSeconds(1000000f);
        while (counter < 3)
        {
            rb2d.AddForce((Vector2.left * speed) * -1);
            ControlMaxSpeed();
            counter++;
            Debug.Log("Another one2");
            yield return new WaitForSeconds(500000f);
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
}

//this.gameObject.transform.position = oldPosition;  
/*this.gameObject.transform.position = newPosition;
goneWayX = newPosition.x - oldPosition.x;
goneWayY = newPosition.y - oldPosition.y;*/
