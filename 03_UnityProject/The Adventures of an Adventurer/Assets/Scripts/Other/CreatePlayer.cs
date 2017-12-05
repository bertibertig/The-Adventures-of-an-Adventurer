using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlayer : MonoBehaviour {

    public bool createPlayerSP = false;
    public Vector3 playerPosition;

	// Use this for initialization
	void Start () {
		if(createPlayerSP && GameObject.FindGameObjectWithTag("Player") == null && GameObject.FindGameObjectWithTag("MultiplayerController").GetComponent<Main>().RoomCreated == false)
        {
            if(playerPosition == new Vector3 (0,0,0))
                playerPosition = new Vector3(0, 2.3f, -1);
            GameObject tmpObj = GameObject.Instantiate(Resources.Load("Player/Player Final"), playerPosition, Quaternion.identity) as GameObject;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
