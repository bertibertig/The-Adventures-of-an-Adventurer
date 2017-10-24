using UnityEngine;
using System.Collections;

public class DontDestroyOnLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (this.gameObject.tag != "untagged" && GameObject.FindGameObjectsWithTag(this.tag).Length > 1)
        {
            print(this.gameObject.tag);
            GameObject.Destroy(GameObject.FindGameObjectsWithTag(this.tag)[1]);
        }
        DontDestroyOnLoad(this);
	}
}
