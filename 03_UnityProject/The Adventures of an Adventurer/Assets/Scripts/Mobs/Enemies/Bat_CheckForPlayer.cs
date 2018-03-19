using UnityEngine;
using System.Collections;
using System;

public class Bat_CheckForPlayer : MonoBehaviour
{
    private GameObject bat;
    private GameObject player;
    public float movementSpeed;

    private RaycastHit2D batHit;
    private RaycastHit2D playerHit;
    private float batHeightFromGround;
    private float playerHeightFromGround;

    private float xMovement;
    private float yMovement;
    private float zMovement;
    private float t;
    
    private float a;
    private float b;
    private float xMax;
    private float y;

    private bool CoRoutineStarted;
    private bool movementCoRoutineStarted = false;

    public bool HasSeenPlayer { get; set; }

    void Start()
    {
        SearchForGameObjects searchForPlayer = GameObject.FindGameObjectWithTag("EventList").GetComponent<SearchForGameObjects>();
        searchForPlayer.PlayerFoundEventHandler += PlayerFound;

        bat = transform.parent.gameObject;
        zMovement = bat.transform.position.z;

        HasSeenPlayer = false;
        CoRoutineStarted = false;

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    public void PlayerFound(object sender, EventArgs e)
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        batHit = Physics2D.Raycast(bat.transform.position, Vector2.down);
        playerHit = Physics2D.Raycast(player.transform.position, Vector2.down);

        if (batHit.collider.gameObject.CompareTag("Ground"))
            batHit.distance = batHeightFromGround;

        if (playerHit.collider.gameObject.CompareTag("Ground"))
            playerHit.distance = playerHeightFromGround;

        if (HasSeenPlayer)
        {
            bat.transform.position = new Vector3(xMovement, yMovement, zMovement);
            t += movementSpeed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !CoRoutineStarted)
        {
            CoRoutineStarted = true;
            HasSeenPlayer = true;
            StartCoroutine("AttackPlayer");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !movementCoRoutineStarted)
        {
            movementCoRoutineStarted = true;
            StopCoroutine("AttackPlayer");
            StartCoroutine("StopTriggerCoRoutine");
        }
    }

    private IEnumerator StopTriggerCoRoutine()
    {
        yield return new WaitForSeconds(2);
        movementCoRoutineStarted = false;
        HasSeenPlayer = false;
        CoRoutineStarted = false;
    }

    private IEnumerator AttackPlayer()
    {
        while (HasSeenPlayer)
        {
            StartCoroutine("Attack");
            yield return new WaitForSeconds(2);
        }
    }

    private IEnumerator Attack()
    {
        //a
        a = player.transform.position.x - bat.transform.position.x;

        //x(max)
        xMax = 2 * a;

        //b
        b = Mathf.Pow(a, 2) / (batHeightFromGround - playerHeightFromGround);

        //y

        yield return null;
    }
}