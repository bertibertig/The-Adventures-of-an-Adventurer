using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_Movement : MonoBehaviour {

	private Move_To_Waypoint mtw;
    private Move_Indefinitely mdir;
    private Destroy_OnContact doc;

	// Use this for initialization
	void Start () {
		mtw = GetComponent<Move_To_Waypoint>();
        mdir = GetComponent<Move_Indefinitely>();
        doc = GetComponentInChildren<Destroy_OnContact>();

        doc.playerIsObstacle = true;
        doc.enemyIsObstacle = false;
		StartCoroutine("Move");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Move()
	{
		mtw.MoveFromAToB ();
        yield return new WaitForSeconds(1);
        mdir.speed = 8f;
        mdir.Move_To_Target(GameObject.FindGameObjectWithTag("Player"));
	}
}
