using UnityEngine;
using System.Collections;
using System;

public class Enemy_CheckForPlayer : MonoBehaviour {

    public GameObject enemy;

    public GameObject player;
    private Rigidbody2D rb2d;
    private Vector3 positionOfEnemy;
    private Enemy_Movement_AI enemy_movement;
    private Vector3 positionOfPlayer;
    private bool CoRoutineStarted;
    private Enemy_Movement_AI enemyAI;
    private Ground_Check groundCheck;
    private bool movementCoRoutineStarted = false;

    public bool HasSeenPlayer { get; set; }

    void Start()
    {
        SearchForGameObjects searchForPlayer = GameObject.FindGameObjectWithTag("EventList").GetComponent<SearchForGameObjects>();
        searchForPlayer.PlayerFoundEventHandler += PlayerFound;

        if (enemy == null)
            enemy = this.transform.parent.gameObject;

        enemy_movement = gameObject.GetComponentInParent<Enemy_Movement_AI>();
        HasSeenPlayer = false;
        rb2d = gameObject.GetComponentInParent<Rigidbody2D>();
        CoRoutineStarted = false;
        enemyAI = enemy.GetComponent<Enemy_Movement_AI>();
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player");
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
        if (col.CompareTag("Player") && !movementCoRoutineStarted)
        {
            movementCoRoutineStarted = true;
            StopCoroutine(MoveToPlayer());
            StartCoroutine("StopTriggerCoRoutine");
        }
    }

    private IEnumerator StopTriggerCoRoutine()
    {
        yield return new WaitForSeconds(2);
        movementCoRoutineStarted = false;
        HasSeenPlayer = false;
        enemy_movement.StartMoveCoroutine();
        CoRoutineStarted = false;
    }

    private IEnumerator MoveToPlayer()
    {
		while (HasSeenPlayer)
        {
            if (groundCheck.Gronded)
            {
                yield return new WaitForSeconds(1);
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
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
