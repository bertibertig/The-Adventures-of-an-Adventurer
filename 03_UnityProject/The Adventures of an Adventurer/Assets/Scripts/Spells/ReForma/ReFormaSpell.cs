using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReFormaSpell : MonoBehaviour {

    public bool selected = false;

    public bool Selected { get { return selected; } set { selected = value; } }

    // Use this for initialization
    void Start ()
    {
		
	}

    public void Transform(string element)
    {
        switch(element)
        {
            case "fire":
                GetComponent<SpriteRenderer>().color = new Color32(231, 122, 122, 255);
                break;
            case "water":
                GetComponent<SpriteRenderer>().color = new Color32(139, 125, 40, 255);
                break;
            case "earth":
                GetComponent<SpriteRenderer>().color = new Color32(160, 55, 26, 255);
                break;
            case "wind":
                GetComponent<SpriteRenderer>().color = new Color32(207, 195, 246, 255);
                break;
            default:
                GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                break;
        }
    }
}
