using UnityEngine;
using System.Collections;

public class WitchTreeSquirel : MonoBehaviour {


	// Use this for initialization
	void Start () {
        StartCoroutine("ThrowNuts");
	}

    private IEnumerator ThrowNuts()
    {

        yield return null;
    }
}
