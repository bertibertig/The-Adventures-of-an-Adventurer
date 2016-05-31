﻿using UnityEngine;
using System.Collections;

public class Inventory_Main : MonoBehaviour {

    public GameObject inventarUI;

    private bool inventoryOpen;

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(inventarUI);
        DontDestroyOnLoad(this);
        inventoryOpen = false;
        //inventarUI.SetActive(false);   
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    { 
        if(Input.GetButtonDown("Tab"))
        {
            inventoryOpen = !inventoryOpen;
            inventarUI.SetActive(inventoryOpen);
        }
	}
}
