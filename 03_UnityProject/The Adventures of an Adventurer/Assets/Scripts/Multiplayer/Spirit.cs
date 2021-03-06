﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : Photon.MonoBehaviour {

    public float lerpStep = 0.5f;
    public bool offline = false;
    public bool StopFollowingCursor = false;

    private Rigidbody2D rb2d;
    private Vector2 newPos;
    private GameObject player;

	// Use this for initialization
	void Start () {
        rb2d = this.GetComponent<Rigidbody2D>();
        if (GameObject.FindGameObjectWithTag("EventList") != null)
        {
            SearchForGameObjects searchForPlayer = GameObject.FindGameObjectWithTag("EventList").GetComponent<SearchForGameObjects>();
            searchForPlayer.PlayerFoundEventHandler += PlayerFound;
        }
	}

    public void Update()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    public void PlayerFound(object sender, EventArgs e)
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camera_follow>().Player = player;
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
		if((this.photonView.isMine || offline) && !StopFollowingCursor)
        {
            this.rb2d.position = new Vector2(player.transform.position.x + 5, player.transform.position.y + 5);
        }
        else if(!StopFollowingCursor)
        {
            Vector2 lerp = Vector2.Lerp(this.rb2d.position, newPos, lerpStep);
            rb2d.MovePosition(lerp);
        }        
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(rb2d == null)
            rb2d = this.GetComponent<Rigidbody2D>();
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
