using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Indefinitely : MonoBehaviour {

    public float speed = 5f;
    public Transform target;

    private Vector3 direction;

    // Use this for initialization
    void Start () {
        if(target != null)
            direction = (target.position - transform.position).normalized;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void Move_To_Direction(Vector3 dir)
    {
        direction = dir;
    }
}
