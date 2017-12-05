using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw_On_Target : MonoBehaviour {

    public Transform myTarget;
    public GameObject cannonBall;
    public float shootAngleFar = 45;
	public float shootAngleMid = 45;
    public float shootAngleClose = 45;
	public float switchDistanceFM;
	public float switchDistanceMC;

	private bool throwIsActive = false;
    Vector3 direction;

    private void Start()
    {
        SearchForGameObjects searchForPlayer = GameObject.FindGameObjectWithTag("EventList").GetComponent<SearchForGameObjects>();
        searchForPlayer.PlayerFoundEventHandler += PlayerFound;

        /*MULTIPLAYER_OWN
        if (myTarget == null)
            myTarget = GameObject.FindGameObjectWithTag("Player").transform;*/
    }

    public void PlayerFound(object sender, EventArgs e)
    {
        if (myTarget == null)
            myTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public bool ThrowIsActive {
		get
		{
			return throwIsActive;
		}
		set
		{
			throwIsActive = value;
		}
	}

    Vector3 BallisiticVel(Vector3 dir, float angle)
    {
		float a = 0;
		float distance = 0;
		float h = dir.y;
		dir.y = 0;
		float dirMag = dir.magnitude;
		do {
			dir.y = 0;
			distance = dirMag;
			a = angle * Mathf.Deg2Rad;
			dir.y = Mathf.Tan (a);
			distance += h / Mathf.Tan (a);
			angle += 1;
		} while(Mathf.Max(0, distance) == 0);
		float vel = Mathf.Sqrt(distance * Physics2D.gravity.magnitude / Mathf.Sin (2 * a));
		if (float.IsNaN (vel))
			vel = 0;
        return vel * dir.normalized;
    }

	public void ThrowProjectile()
	{
		if (throwIsActive)
		{
			GameObject ball = Instantiate(cannonBall, transform.position, Quaternion.identity) as GameObject;
			ball.SetActive(true);
			direction = myTarget.position - transform.position;
			if (direction.x >= switchDistanceFM) {
				ball.GetComponent<Rigidbody2D> ().velocity = BallisiticVel (direction, shootAngleFar);
			} else if (direction.x < switchDistanceMC) {
				ball.GetComponent<Rigidbody2D> ().velocity = BallisiticVel (direction, shootAngleClose);
			} else {
				ball.GetComponent<Rigidbody2D> ().velocity = BallisiticVel (direction, shootAngleMid);
			}
			Destroy(ball, 5);
		}
	}
}
