using UnityEngine;
using System.Collections;

public class Appear_After_Event : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponentInChildren<BoxCollider2D>().enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(GameObject.FindGameObjectWithTag("Trigger") == null)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponentInChildren<BoxCollider2D>().enabled = true;
        }
	}
}
