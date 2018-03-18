using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VillagerAI : MonoBehaviour {

    public bool runningEnabled = true;
    [Header("Position where the villager should stop Running")]
    public Vector3 endPos = new Vector2(0,0);
    [Header("Running Speed")]
    public float speed = 3;
    [Header("Fastest Speed the villager runs")]
    public float maxSpeed = 3;

    private Vector2 dirNormalized;
    private Rigidbody2D rb2d;
    private Animator anim;
    private PolygonCollider2D polColl;

    public PolygonCollider2D StandPolColl { get; set; }
    public PolygonCollider2D RunPolColl { get; set; }

    // Use this for initialization
    void Start ()
    {
        if (this.GetComponent<PolygonCollider2D>() == null)
        {
            StandPolColl = this.gameObject.transform.Find("StandingCollider").GetComponent<PolygonCollider2D>();
            RunPolColl = this.gameObject.transform.Find("RunningCollider").GetComponent<PolygonCollider2D>();
            if (runningEnabled)
            {
                polColl = RunPolColl;
            }
            else
                polColl = StandPolColl;
            polColl.enabled = true;
        }
        else
            polColl = this.GetComponent<PolygonCollider2D>();
        dirNormalized = (endPos - transform.position).normalized;
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        IgnorePlayerAndSequenceColliders();
        if (runningEnabled)
        {
            StartCoroutine("Run");
        }
	}

    private void Update()
    {
        anim.SetFloat("speed", Mathf.Abs(rb2d.velocity.x));
        if (speed >= 0.1)
        {
            polColl = RunPolColl;
            IgnorePlayerAndSequenceColliders();
        }
        else
        {
            polColl = StandPolColl;
            IgnorePlayerAndSequenceColliders();
        }
    }

    private IEnumerator Run()
    {
        do
        {
            if (Vector2.Distance(endPos, transform.position) <= 1)
            {
                runningEnabled = false;
                Destroy(this.gameObject);
            }
            else
                ControlMaxSpeedX();
            yield return new WaitForFixedUpdate();
        } while (runningEnabled);
    }

    private void ControlMaxSpeedX()
    {
        if (rb2d.velocity.x > maxSpeed)
            rb2d.velocity = new Vector2(maxSpeed,0);
        if (rb2d.velocity.x < -maxSpeed)
            rb2d.velocity = new Vector2(-maxSpeed, 0);
        else
        {
            float xSpeed = transform.position.x + dirNormalized.x * speed * Time.deltaTime;
            rb2d.AddForce(new Vector2(xSpeed, 0));
        }
    }

    public void IgnorePlayerAndSequenceColliders()
    {
        //Player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(polColl, player.GetComponent<PolygonCollider2D>());
        Physics2D.IgnoreCollision(polColl, player.GetComponents<CircleCollider2D>()[0]);
        Physics2D.IgnoreCollision(polColl, player.GetComponents<CircleCollider2D>()[1]);

        //Sequence
        Physics2D.IgnoreCollision(polColl, GameObject.FindGameObjectsWithTag("SequenceItems").Where(g => g.name == "Villager00").FirstOrDefault().GetComponent<VillagerAI>().StandPolColl);
        Physics2D.IgnoreCollision(polColl, GameObject.FindGameObjectsWithTag("SequenceItems").Where(g => g.name == "Villager00").FirstOrDefault().GetComponent<VillagerAI>().RunPolColl);
        Physics2D.IgnoreCollision(polColl, GameObject.FindGameObjectsWithTag("SequenceItems").Where(g => g.name == "Villager01").FirstOrDefault().GetComponent<VillagerAI>().StandPolColl);
        Physics2D.IgnoreCollision(polColl, GameObject.FindGameObjectsWithTag("SequenceItems").Where(g => g.name == "Villager01").FirstOrDefault().GetComponent<VillagerAI>().RunPolColl);
        Physics2D.IgnoreCollision(polColl, GameObject.FindGameObjectsWithTag("SequenceItems").Where(g => g.name == "Villager03").FirstOrDefault().GetComponent<VillagerAI>().StandPolColl);
        Physics2D.IgnoreCollision(polColl, GameObject.FindGameObjectsWithTag("SequenceItems").Where(g => g.name == "Villager03").FirstOrDefault().GetComponent<VillagerAI>().RunPolColl);
    }
}
