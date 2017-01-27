using UnityEngine;
using System.Collections;

public class Troll_Slime_Fly_Away : MonoBehaviour
{

    public float flyPower;

    private Rigidbody2D rb2d;
    private bool playerEntered;
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        playerEntered = false;
    }

    void Update()
    {
        StartCoroutine("MoveToPlayer");
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        playerEntered = true;
    }

    public IEnumerator MoveToPlayer()
    {
        while (!playerEntered)
        {
            if (rb2d.velocity.y >= 100)
            {
                Destroy(this);
            }
            rb2d.AddForce(Vector2.up * flyPower);
            yield return 0;
        }
    }
}
