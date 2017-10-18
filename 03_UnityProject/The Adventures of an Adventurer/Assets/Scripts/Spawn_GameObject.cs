using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_GameObject : MonoBehaviour {

	public GameObject objectToSpawn;
	public string objectName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// OnEnable is called when the disabled script is being enabled
	void OnEnable() {
		GameObject objectCopy = Instantiate (objectToSpawn, transform.position, Quaternion.identity);
		objectCopy.name = objectName;
		objectCopy.SetActive (true);
	}
}
