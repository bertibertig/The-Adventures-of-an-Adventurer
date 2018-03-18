using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDialougeAtPosition : MonoBehaviour {

    [Header("The id Tag of the Dialoge")]
    public string id = "default";
    public bool sceneEnabled = true;
    public bool dialogeShouldOnlyHappenOnce = true;
    [Header("The Name of the Entry in the Event List (Default = id)")]
    public string eventListName;

    private DialogeHandler dHandler;
    private EventList eList;
    private bool displayedOnce = false;

    // Use this for initialization
    void Start () {
        GameObject.FindGameObjectWithTag("EventList").GetComponent<SearchForGameObjects>().DialogeDBFoundEventHandler += DialogeDBFound;
        if(eventListName == "")
            eventListName = id;
        eList = GameObject.FindGameObjectWithTag("EventList").GetComponent<EventList>();
        if (eList.GetEvent(eventListName) != null && !dialogeShouldOnlyHappenOnce)
            displayedOnce = true;
    }

    private void DialogeDBFound(object sender, EventArgs e)
    {
        dHandler = GameObject.FindGameObjectWithTag("DialogesDB").GetComponent<XMLReader>().GetDialougeHandlerByName(id).GetComponent<DialogeHandler>();
        dHandler.DialogeEndedEventHandler += DHandler_DialogeEndedEventHandler;
    }

    private void DHandler_DialogeEndedEventHandler(object sender, EventArgs e)
    {
        if (dialogeShouldOnlyHappenOnce)
        {
            eList.AddEvent("Dialoge__ForestVillagers_01", true, "Player has heard the Dialouge of the villagers by the forest.");
            displayedOnce = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player") && sceneEnabled && !displayedOnce)
        {
            dHandler.StartConversation();
        }
    }
}
