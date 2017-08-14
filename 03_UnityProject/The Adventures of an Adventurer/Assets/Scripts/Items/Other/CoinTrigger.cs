using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class CoinTrigger : MonoBehaviour {

    public float speed;
    public float maxSpeed;
    public GameObject GoldText;

    private Rigidbody2D rb2d;
    private Vector3 positionOfPlayer;
    private Vector3 positionOfCoin;
    private GameObject parrentCoin;
    private GameObject player;
    private Text coinCounter;

    void Start()
    {
        if (speed <= 0)
            speed = 10;
        rb2d = gameObject.GetComponentInParent<Rigidbody2D>();
        parrentCoin = this.transform.parent.gameObject;
        //coinCounter = GoldText.GetComponent<Text>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player = col.gameObject;
            StartCoroutine("MoveToPlayer");
        }
    }

    private IEnumerator MoveToPlayer()
    {
        do
        {
            positionOfPlayer = player.transform.position;
            positionOfCoin = parrentCoin.transform.position;

            if (positionOfCoin.x - positionOfPlayer.x < 0)
                rb2d.AddForce(Vector2.right * speed);
            else if (positionOfCoin.x - positionOfPlayer.x > 0)
                rb2d.AddForce(Vector2.left * speed);
            //if (positionOfCoin.y - positionOfPlayer.y > 0)
            //    rb2d.AddForce(Vector2.down * speed);
            if (positionOfCoin.y - positionOfPlayer.y < 0)
                rb2d.AddForce(Vector2.up * speed);

            if (rb2d.velocity.x < -maxSpeed)
            {
                rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
            }

            yield return null;
        } while (Math.Round(positionOfCoin.x) != Math.Round(positionOfPlayer.x) && positionOfCoin.y != positionOfPlayer.y);
        //int counter = Convert.ToInt32(coinCounter.text);
        //counter++;
        //coinCounter.text = counter.ToString();
        Destroy(parrentCoin);
    }
}
