using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : Photon.MonoBehaviour {

    public float lerpStep = 0.5f;

    private Rigidbody2D rb2d;
    private Vector2 newPos;

	// Use this for initialization
	void Start () {
        rb2d = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(this.photonView.isMine)
        {
            Vector2 mouse = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            //NOTE: Only for Debugging has to be changed to Touch controlls. 
            if (Input.mousePosition.x >= 0 && Input.mousePosition.y >= 0)
                this.rb2d.position = mouse;
        }
        else
        {
            Vector2 lerp = Vector2.Lerp(this.rb2d.position, newPos, lerpStep);
            rb2d.MovePosition(lerp);
        }        
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(this.rb2d.position);
        }
        else if (stream.isReading)
        {
            newPos = (Vector2)stream.ReceiveNext();
        }
    }
}
