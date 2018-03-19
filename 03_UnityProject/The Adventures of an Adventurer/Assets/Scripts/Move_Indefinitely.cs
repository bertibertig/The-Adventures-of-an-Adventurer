using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Indefinitely : MonoBehaviour {

    public float speed = 5f;
    public Transform target;

    private Vector3 direction;

    // Use this for initialization
    void Start () {
        Set_Target_Direction();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void Move_To_Direction(Vector3 dir)
    {
        direction = dir;
    }

    public void Move_To_Target(GameObject target)
    {
        this.target = target.transform;
        Set_Target_Direction();
    }

    private void Set_Target_Direction()
    {
        if (target != null)
            direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
    }
}
