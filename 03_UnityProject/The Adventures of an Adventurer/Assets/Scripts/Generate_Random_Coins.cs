using UnityEngine;
using System.Collections;

public class Generate_Random_Coins : MonoBehaviour {

    public GameObject spriteToDuplicate;

    // Use this for initialization
    void Start () {
        Vector3 currentPosition = spriteToDuplicate.transform.position;
        for (int i = 0; i < 7; i++)
        {
            GameObject tmpObj = GameObject.Instantiate(spriteToDuplicate, currentPosition, Quaternion.identity) as GameObject;
            currentPosition += new Vector3(Random.value *1.8f, Mathf.Abs(1f * Random.value), 0f);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
