using UnityEngine;
using System.Collections;

public class fallDownOnButtionPress_Script : MonoBehaviour {

	public GameObject gameobj;
    public GameObject pl;
    private Player_Movement player;
    private EdgeCollider2D collider;

	void Start()
	{
		collider = gameobj.GetComponent<EdgeCollider2D>();
        player = pl.GetComponent<Player_Movement>();
    }

	void OnTriggerEnter2D(Collider2D col)
	{
        if (col.CompareTag ("Player") && !(player.GetGrounded())) {
            Debug.Log(player.grounded);
			collider.enabled = false;
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.CompareTag ("Player") && Input.GetButtonDown("Down")) {
			collider.enabled = false;
		}
        /*if(player.GetGrounded() == false)
        {
            collider.enabled = false;
        }*/
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) {
			collider.enabled = true;
		}
	}
}
