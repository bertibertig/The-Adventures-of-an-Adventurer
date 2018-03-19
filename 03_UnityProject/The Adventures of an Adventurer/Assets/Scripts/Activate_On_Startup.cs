using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate_On_Startup : MonoBehaviour {

    public event EventHandler UIActivatedHandler;

    public bool shouldBeDeavtivated = false;

	// Use this for initialization
	void Start () {
        if (!gameObject.active && !shouldBeDeavtivated)
        {
            this.gameObject.SetActive(true);
            UIActivated();
        }
	}

    private void UIActivated()
    {
        print("Notifieing Subscribers (UI Activated)");
        if (UIActivatedHandler != null)
            UIActivatedHandler(this, null);
    }
}
