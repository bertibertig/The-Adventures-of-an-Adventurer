using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_Movement : MonoBehaviour {

	private Move_To_Waypoint mtw;

	// Use this for initialization
	void Start () {
		mtw = GetComponent<Move_To_Waypoint>();

		Move ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Move()
	{
		mtw.MoveFromAToB ();
	}
}
