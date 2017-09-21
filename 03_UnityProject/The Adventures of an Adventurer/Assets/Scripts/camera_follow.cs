using UnityEngine;
using System.Collections;
using System;

public class camera_follow : Photon.MonoBehaviour {

    private Vector2 velocity;

    public float smoothTimeY;
    public float smoothTimeX;

    public GameObject Player { get; set; }

    public bool bounds;

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

	// Use this for initialization
	void Start () {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void FixedUpdate()
    {
        //TODO: Remove and Implement correct
        if(Player == null)
        {
            SearchForGameObjects searchForPlayer = GameObject.FindGameObjectWithTag("EventList").GetComponent<SearchForGameObjects>();
            searchForPlayer.PlayerFoundEventHandler += PlayerFound;
        }

        if (Player != null)
        {
            float posx = Mathf.SmoothDamp(transform.position.x, Player.transform.position.x, ref velocity.x, smoothTimeX);
            float posy = Mathf.SmoothDamp(transform.position.y, Player.transform.position.y, ref velocity.y, smoothTimeY);

            transform.position = new Vector3(posx, posy, transform.position.z);
        }

        if (bounds)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
            Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
            Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
        }
    }

    public void PlayerFound(object sender, EventArgs e)
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
}
