using UnityEngine;
using System.Collections;

public class player_grounded : MonoBehaviour {

    private Player_Movement player;

    // Use this for initialization
    void Start()
    {
        player = gameObject.GetComponentInParent<Player_Movement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
        {
            player.grounded = true;
            player.canDoubleJump = false;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
        {
            player.grounded = true;
            player.canDoubleJump = false;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
        {
            player.grounded = false;
        }
    }
}
