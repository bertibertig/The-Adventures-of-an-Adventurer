using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Move_To_Waypoint : MonoBehaviour {

	public float movementSpeed = 1.0f;

	private GameObject movingObject;
	private GameObject startPoint;
	private GameObject endPoint;

	private float xMovement;
	private float yMovement;
	private float zMovement;

	private float t = 0.0f;
	private bool movementStarted;

	// Use this for initialization
	void Start () {
		if (movingObject == null)
			movingObject = this.gameObject;

		if (startPoint == null)
			startPoint = GameObject.FindGameObjectsWithTag ("Waypoint").Where (x => x.name == this.gameObject.name).FirstOrDefault ().transform.Find ("WP_A").gameObject;
		
		if (endPoint == null)
			endPoint = GameObject.FindGameObjectsWithTag ("Waypoint").Where (x => x.name == this.gameObject.name).FirstOrDefault ().transform.Find ("WP_B").gameObject;

		zMovement = movingObject.transform.position.z;
	}

	// Update is called once per frame
	void Update () {
		movingObject.transform.position = new Vector3(xMovement, yMovement, zMovement);

		if(movementStarted == true)
			t += movementSpeed * Time.deltaTime;
	}

	public void MoveFromAToB()
	{
		StartCoroutine ("Coroutine_MoveFromAToB");
	}

	private IEnumerator Coroutine_MoveFromAToB()
	{
		movementStarted = true;
		while (t < 1) 
		{
			xMovement = Mathf.Lerp (startPoint.transform.position.x, endPoint.transform.position.x, t);
			yMovement = Mathf.Lerp (startPoint.transform.position.y, endPoint.transform.position.y, t);
			yield return 0;
		}
		movementStarted = false;
	}

	public void MoveFromBToA()
	{
		StartCoroutine ("Coroutine_MoveFromBToA");
	}

	private IEnumerator Coroutine_MoveFromBToA()
	{
		movementStarted = true;
		while (t < 1) 
		{
			xMovement = Mathf.Lerp (endPoint.transform.position.x, startPoint.transform.position.x, t);
			yMovement = Mathf.Lerp (endPoint.transform.position.y, startPoint.transform.position.y, t);
			yield return 0;
		}
		movementStarted = false;
	}
}