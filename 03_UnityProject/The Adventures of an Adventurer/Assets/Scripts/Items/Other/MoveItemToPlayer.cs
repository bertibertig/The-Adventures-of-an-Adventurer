using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveItemToPlayer : MonoBehaviour {

    public float speed = 10;
    public float maxSpeed = 10;
    public int itemID = 0;

    private Rigidbody2D rb2d;
    private Vector3 positionOfPlayer;
    private Vector3 positionOfItem;
    private GameObject parrentItem;
    private GameObject player;
    private Inventory_Main inventory;
    private bool moveToPlayerStarted = false;

    void Start()
    {
        if (speed <= 0)
            speed = 10;
        inventory = GameObject.FindGameObjectWithTag("InventoryUI").GetComponentInChildren<Inventory_Main>();
        rb2d = gameObject.GetComponentInParent<Rigidbody2D>();
        parrentItem = this.transform.parent.gameObject;
        //coinCounter = GoldText.GetComponent<Text>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player = col.gameObject;
            Physics2D.IgnoreCollision(gameObject.GetComponentInParent<PolygonCollider2D>(), player.GetComponent<PolygonCollider2D>());
            if (!moveToPlayerStarted)
            {
                moveToPlayerStarted = true;
                StartCoroutine("MoveToPlayer");
            }
        }
    }

    private IEnumerator MoveToPlayer()
    {
        do
        {
            positionOfPlayer = player.transform.position;
            positionOfItem = parrentItem.transform.position;

            if (positionOfItem.x - positionOfPlayer.x < 0)
                rb2d.AddForce(Vector2.right * speed);
            else if (positionOfItem.x - positionOfPlayer.x > 0)
                rb2d.AddForce(Vector2.left * speed);
            //if (positionOfCoin.y - positionOfPlayer.y > 0)
            //    rb2d.AddForce(Vector2.down * speed);
            if (positionOfItem.y - positionOfPlayer.y < 0)
                rb2d.AddForce(Vector2.up * speed);

            if (rb2d.velocity.x < -maxSpeed)
            {
                rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
            }

            yield return null;
        } while (Math.Round(positionOfItem.x) != Math.Round(positionOfPlayer.x) && positionOfItem.y != positionOfPlayer.y);
        //int counter = Convert.ToInt32(coinCounter.text);
        //counter++;
        //coinCounter.text = counter.ToString();
        inventory.AddItem(itemID);
        Destroy(parrentItem);
    }
}
