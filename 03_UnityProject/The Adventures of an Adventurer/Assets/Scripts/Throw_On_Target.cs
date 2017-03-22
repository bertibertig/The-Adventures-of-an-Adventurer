using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw_On_Target : MonoBehaviour {

    public Transform myTarget;
    public GameObject cannonBall;
    public float shootAngleFar = 45;
    public float switchDistance;
    public float shootAngleClose = 45;
    Vector3 direction;


    Vector3 BallisiticVel(Vector3 dir, float angle)
    {
        float h = dir.y;
        dir.y = 0;
        float distance = dir.magnitude;
        float a = angle * Mathf.Deg2Rad;
        dir.y = Mathf.Tan(a);
        distance += h / Mathf.Tan(a);
        float vel = Mathf.Sqrt(distance * Physics2D.gravity.magnitude / Mathf.Sin(2 * a));

        return vel * dir.normalized;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            GameObject ball = Instantiate(cannonBall, transform.position, Quaternion.identity) as GameObject;
            direction = myTarget.position - transform.position;
            if (direction.x >= switchDistance)
            {
                ball.GetComponent<Rigidbody2D>().velocity = BallisiticVel(direction, shootAngleFar);
            }
            else if (direction.x <= switchDistance)
            {
                ball.GetComponent<Rigidbody2D>().velocity = BallisiticVel(direction, shootAngleClose);
            }
            Destroy(ball, 5);
        }
    }
}
