using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_In_Seconds : MonoBehaviour {

    public float time = 5.0f;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, time);
    }
}
