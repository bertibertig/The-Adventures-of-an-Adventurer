using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porky_AI : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponentInChildren<PlayerChecker>().PlayerEnter += Porky_AI_PlayerEnter;
        GetComponentInChildren<PlayerChecker>().PlayerExiting += Porky_AI_PlayerExiting;
	}

    private void Porky_AI_PlayerExiting(object sender, System.EventArgs e)
    {
        print("Following Player");
    }

    private void Porky_AI_PlayerEnter(object sender, System.EventArgs e)
    {
        print("Leave Player");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
