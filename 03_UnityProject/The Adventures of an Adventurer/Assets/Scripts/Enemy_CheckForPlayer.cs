using UnityEngine;
using System.Collections;
using System;

public class Enemy_CheckForPlayer : MonoBehaviour {

    public GameObject enemy;

    private GameObject player;
    private Rigidbody2D rb2d;
    private Vector3 positionOfEnemy;
    private Enemy_Movement_AI enemy_movement;
    private Vector3 positionOfPlayer;
    private bool CoRoutineStarted;
    private Enemy_Movement_AI enemyAI;
    private Ground_Check groundCheck;

    public bool HasSeenPlayer { get; set; }

    void Start()
    {
        SearchForGameObjects searchForPlayer = GameObject.FindGameObjectWithTag("EventList").GetComponent<SearchForGameObjects>();
        searchForPlayer.PlayerFoundEventHandler += PlayerFound;
        enemy_movement = gameObject.GetComponentInParent<Enemy_Movement_AI>();
        HasSeenPlayer = false;
        rb2d = gameObject.GetComponentInParent<Rigidbody2D>();
        CoRoutineStarted = false;
        enemyAI = enemy.GetComponent<Enemy_Movement_AI>();
    }

    public void PlayerFound(object sender, EventArgs e)
    {
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (HasSeenPlayer)
        {
            positionOfPlayer = player.transform.position;
            positionOfEnemy = enemy.transform.position;
        }
    }

	void OnTriggerEnter2D(Collider2D col)
    {
        groundCheck = enemy_movement.GroundTrigger;
        if (col.CompareTag("Player") && !CoRoutineStarted)
        {
            CoRoutineStarted = true;
            HasSeenPlayer = true;
            enemy_movement.StopMoveCoroutine();
            StartCoroutine(MoveToPlayer());
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine("StopTriggerCoRoutine");
        }
    }

    private IEnumerator StopTriggerCoRoutine()
    {
        yield return new WaitForSeconds(2);
        HasSeenPlayer = false;
        StopCoroutine(MoveToPlayer());
        enemy_movement.StartMoveCoroutine();
        CoRoutineStarted = false;
    }

    private IEnumerator MoveToPlayer()
    {
        yield return new WaitForSeconds(2);
		while (HasSeenPlayer)
        {
            if (groundCheck.Gronded)
            {
                yield return new WaitForSeconds(2);
                if ((positionOfPlayer.x - positionOfEnemy.x) > 0)
                {
                    rb2d.AddForce(Vector2.up * enemyAI.jumpPower);
                    rb2d.AddForce((Vector2.right * enemyAI.speed));
                }

                if ((positionOfPlayer.x - positionOfEnemy.x) < 0 && groundCheck.Gronded)
                {
                    rb2d.AddForce(Vector2.up * enemyAI.jumpPower);
                    rb2d.AddForce((Vector2.left * enemyAI.speed));
                }
                print(groundCheck.Gronded);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
