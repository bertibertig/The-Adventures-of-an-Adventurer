using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSequence : MonoBehaviour {

    [Header("Enabled if Dialouge should be started at Scene loading")]
    public bool sceneEnabled = true;
    [Header("Nametag of the Dialouge object")]
    public string id;
    [Header("Gameobject: HalfTent for cutscene")]
    public GameObject halfTent;

    private DialogeHandler dHandler;

	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("EventList").GetComponent<SearchForGameObjects>().DialogeDBFoundEventHandler += DialogeDBFound;
        
	}

    private void DialogeDBFound(object sender, EventArgs e)
    {
        dHandler = GameObject.FindGameObjectWithTag("DialogesDB").GetComponent<XMLReader>().GetDialougeHandlerByName(id).GetComponent<DialogeHandler>();
        if (sceneEnabled)
        {
            dHandler.StartConversation();
        }
    }
}
