using UnityEngine;
using System.Collections;

public class WitchTreeAcorn : MonoBehaviour {

    public GameObject witchTreeLeaveCollider;

	// Use this for initialization
	void Start () {
        Physics2D.IgnoreCollision(witchTreeLeaveCollider.GetComponent<EdgeCollider2D>(), GetComponent<PolygonCollider2D>());
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 0, -(this.gameObject.transform.rotation.z + 3)));
	}
}
