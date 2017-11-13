using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPBossFight : MonoBehaviour {

    public GameObject weapon;
    public GameObject lColl;
    public GameObject rColl;
    public float maxSpeed;

    private Rigidbody2D rb2d;
    private Animator wepAnim;
    private GameObject cam;
    private GameObject player;
    private float xStd;
    private float yStd;

    private void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        wepAnim = this.GetComponentInChildren<Animator>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
        IgnoreColls(true);

        //For Testing
        //..
        StartFight();
        //..
    }

    public void StartFight()
    {
        cam.GetComponent<camera_follow>().Static = true;
        cam.transform.position = new Vector3(-70.39f, 1.58f, -10);
        Charge();
    }

    private void Charge()
    {
        StartCoroutine("ChargeCoRoutine");
    }

    private void Update()
    {
        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }
        else if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }
    }

    IEnumerator ChargeCoRoutine()
    {
        wepAnim.SetBool("Charge",true);
        yield return new WaitForSeconds(2);
        while(this.transform.localPosition.x <=  rColl.transform.localPosition.x -1)
        {
            rb2d.AddForce(new Vector2(10, 0));
            yield return null;
        }

        yield return new WaitForSeconds(2);
        transform.Rotate(0, 180, 0);

        yield return new WaitForSeconds(1);

        while (this.transform.localPosition.x - 1 >= lColl.transform.localPosition.x)
        {
            rb2d.AddForce(new Vector2(-10, 0));
            yield return null;
        }

        yield return new WaitForSeconds(2);
        this.transform.Rotate(0, 180, 0);
        wepAnim.SetBool("Charge", false);

        //weapon.transform.localPosition = new Vector2(xStd, yStd);
        //weapon.transform.Rotate(new Vector3(0, 0, 0));
    }

    private void IgnoreColls(bool ignore = true)
    {
        Physics2D.IgnoreCollision(this.gameObject.GetComponent<PolygonCollider2D>(), player.GetComponent<PolygonCollider2D>(), ignore);
        Physics2D.IgnoreCollision(this.gameObject.GetComponent<PolygonCollider2D>(), player.GetComponents<CircleCollider2D>()[0], ignore);
        Physics2D.IgnoreCollision(this.gameObject.GetComponent<PolygonCollider2D>(), player.GetComponents<CircleCollider2D>()[1], ignore);

        Physics2D.IgnoreCollision(this.gameObject.GetComponents<CircleCollider2D>()[0], player.GetComponent<PolygonCollider2D>(), ignore);
        Physics2D.IgnoreCollision(this.gameObject.GetComponents<CircleCollider2D>()[0], player.GetComponents<CircleCollider2D>()[0], ignore);
        Physics2D.IgnoreCollision(this.gameObject.GetComponents<CircleCollider2D>()[0], player.GetComponents<CircleCollider2D>()[1], ignore);

        Physics2D.IgnoreCollision(this.gameObject.GetComponents<CircleCollider2D>()[1], player.GetComponent<PolygonCollider2D>(), ignore);
        Physics2D.IgnoreCollision(this.gameObject.GetComponents<CircleCollider2D>()[1], player.GetComponents<CircleCollider2D>()[0], ignore);
        Physics2D.IgnoreCollision(this.gameObject.GetComponents<CircleCollider2D>()[1], player.GetComponents<CircleCollider2D>()[1], ignore);
    }
}
