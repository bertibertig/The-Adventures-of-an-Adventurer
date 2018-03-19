using UnityEngine;
using System.Collections;

public class Enemy_CheckForPlayer : MonoBehaviour {

    public GameObject enemy;

    private GameObject player;
    private Rigidbody2D rb2d;
    private Vector3 positionOfEnemy;
    private Slime_Movement_AI enemy_movement;
    private bool hasSeenPlayer;
    private Vector3 positionOfPlayer;
    private bool CoRoutineStarted;
    private Slime_Movement_AI enemyAI;

    void Start()
    {
        enemy_movement = gameObject.GetComponentInParent<Slime_Movement_AI>();
        hasSeenPlayer = false;
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = gameObject.GetComponentInParent<Rigidbody2D>();
        CoRoutineStarted = false;
        enemyAI = enemy.GetComponent<Slime_Movement_AI>();
    }
    void Update()
    {
        if (hasSeenPlayer)
        {
            positionOfPlayer = player.transform.position;
            positionOfEnemy = enemy.transform.position;
        }
    }

	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player") && !CoRoutineStarted)
        {
            hasSeenPlayer = true;
            enemy_movement.StopMoveCoroutine();
            StartCoroutine(MoveToPlayer());
            CoRoutineStarted = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            hasSeenPlayer = false;
            StopCoroutine(MoveToPlayer());
            enemy_movement.StartMoveCoroutine();
            CoRoutineStarted = false;
        }
    }

    public IEnumerator MoveToPlayer()
    {
        yield return new WaitForSeconds(2);
        do
        {
            if ((positionOfPlayer.x - positionOfEnemy.x) > 0)
            {
                rb2d.AddForce(Vector2.up * enemyAI.jumpPower);
                rb2d.AddForce((Vector2.right * enemyAI.speed));
            }

            if ((positionOfPlayer.x - positionOfEnemy.x) < 0)
            {
                rb2d.AddForce(Vector2.up * enemyAI.jumpPower);
                rb2d.AddForce((Vector2.left * enemyAI.speed));
            }
            yield return new WaitForSeconds(2);
        } while (hasSeenPlayer) ;
    }
}
