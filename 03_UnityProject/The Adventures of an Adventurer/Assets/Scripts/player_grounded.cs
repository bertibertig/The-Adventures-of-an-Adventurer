using UnityEngine;
using System.Collections;
using System;

public class player_grounded : MonoBehaviour {

    private Player_Movement playerMv;
    private Player_Movement_MP playerMvMp;

    // Use this for initialization
    void Start()
    {
        playerMv = gameObject.GetComponentInParent<Player_Movement>();
        if(playerMv == null)
            playerMvMp = gameObject.GetComponentInParent<Player_Movement_MP>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ground") && playerMv != null)
        {
            playerMv.grounded = true;
            playerMv.canDoubleJump = false;
        }
        else if(col.CompareTag("Ground") && playerMvMp != null)
        {
            playerMvMp.grounded = true;
            playerMvMp.canDoubleJump = false;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Ground") && playerMv != null)
        {
            playerMv.grounded = true;
            playerMv.canDoubleJump = false;
        }
        else if (col.CompareTag("Ground") && playerMvMp != null)
        {
            playerMvMp.grounded = true;
            playerMvMp.canDoubleJump = false;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Ground") && playerMv != null)
        {
            playerMv.grounded = false;
        }
        else if (col.CompareTag("Ground") && playerMvMp != null)
        {
            playerMvMp.grounded = false;
        }

    }
}
