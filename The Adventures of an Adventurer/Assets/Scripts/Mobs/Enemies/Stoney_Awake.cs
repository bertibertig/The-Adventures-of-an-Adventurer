using UnityEngine;
using System.Collections;

public class Stoney_Awake : MonoBehaviour {

    public GameObject Stony;

    private bool axeRemoved;

	// Use this for initialization
	void Start () {
        axeRemoved = false;
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void SetAxRemovedTrue()
    {
        axeRemoved = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player") && axeRemoved)
        {
            Stony.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Sprites//Mobs//Enemies//Stoney_Cracked.png");
        }
    }
}
