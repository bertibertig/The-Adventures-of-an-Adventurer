using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_WitchtreeDefeated : MonoBehaviour {

    public float textSpeed;
    public string language;
    public string[] germanDialoge;
    public string[] englishDialoge;

    Dialoge dialogeHandler;

	// Use this for initialization
	void Start () {
        dialogeHandler = new Dialoge(textSpeed, germanDialoge, englishDialoge);
    }
}
