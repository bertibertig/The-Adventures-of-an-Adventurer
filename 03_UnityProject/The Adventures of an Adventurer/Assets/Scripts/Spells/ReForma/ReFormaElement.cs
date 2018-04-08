using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReFormaElement : MonoBehaviour {

    public string element = "fire";

	// Use this for initialization
	void Start () {
		if(element == null && element.ToLower() != "fire" || element.ToLower() != "water" || element.ToLower() != "earth" || element.ToLower() != "air")
        {
            element = "fire";
        }
	}

    private void OnMouseDown()
    {
        if(GameObject.FindGameObjectWithTag("Spirit").GetComponent< ReFormaSpell>().Selected)
            GameObject.FindGameObjectWithTag("Spirit").GetComponent<ReFormaSpell>().Transform(element);
    }
}
