using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayTextAfterButtonpress : MonoBehaviour {

    [Header("Id Tag of the Dialouge in the XML")]
    public string id = "default";
    public bool dialogeShouldOnlyHappenOnce = true;
    [Header("The Name of the Entry in the Event List (Default = id)")]
    public string eventListName;
    [Header("Name of the Button which sould be displayed")]
    public string buttonName = "E";
    [Header("Use this to declare a Text which sould be Displayed when the dialoge is executet a second time. This text will be displayed every time the player interacts whith the talker.")]
    public bool playAnotherTextAfterFirst = false;
    public string secondId = "default";
    
    private DialogeHandler dHandler;
    private EventList eList;
    private bool playerInTrigger = false;
    private GameObject button;
    private GameObject player;
    private bool displayedOnce = false;
    private bool currentlySpeaking = false;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject.FindGameObjectWithTag("EventList").GetComponent<SearchForGameObjects>().DialogeDBFoundEventHandler += DialogeDBFound;
        if (GameObject.FindGameObjectsWithTag("PlayerButtons").Where(g => g.name == buttonName).FirstOrDefault() != null)
            button = GameObject.FindGameObjectsWithTag("PlayerButtons").Where(g => g.name == buttonName).FirstOrDefault();
        if (eventListName == "")
            eventListName = id;
        eList = GameObject.FindGameObjectWithTag("EventList").GetComponent<EventList>();
        if (eList.GetEvent(eventListName) != null)
            displayedOnce = true;
    }

    private void DialogeDBFound(object sender, EventArgs e)
    {
        dHandler = GameObject.FindGameObjectWithTag("DialogesDB").GetComponent<XMLReader>().GetDialougeHandlerByName(id).GetComponent<DialogeHandler>();
        dHandler.DialogeEndedEventHandler += DHandler_DialogeEndedEventHandler;
    }

    private void DHandler_DialogeEndedEventHandler(object sender, EventArgs e)
    {
        if(dialogeShouldOnlyHappenOnce)
        {
            eList.AddEvent("Dialoge__ForestVillagers_02", true, "Player has talked with the Villagers ant the Forest");
            if(!playAnotherTextAfterFirst)
                button.GetComponent<SpriteRenderer>().enabled = false;
        }
        if(playAnotherTextAfterFirst)
        {
            dHandler = GameObject.FindGameObjectWithTag("DialogesDB").GetComponent<XMLReader>().GetDialougeHandlerByName(secondId).GetComponent<DialogeHandler>();
            dHandler.DialogeEndedEventHandler += DHandler_DialogeEndedEventHandler;
        }
        displayedOnce = true;
        currentlySpeaking = false;
    }

    // Update is called once per frame
    void Update () {
		if(playerInTrigger && button != null && (!displayedOnce || playAnotherTextAfterFirst))
        {
            Vector2 playerPosition = player.GetComponent<Rigidbody2D>().position;
            button.transform.position = new Vector3(playerPosition.x, playerPosition.y + 2);
        }
        if(Input.GetButtonDown("Interact") && !currentlySpeaking && playerInTrigger && (!displayedOnce || playAnotherTextAfterFirst))
        {
            currentlySpeaking = true;
            dHandler.StartConversation();
        }
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && (!displayedOnce || playAnotherTextAfterFirst))
        {
            button.GetComponent<SpriteRenderer>().enabled = true;
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            button.GetComponent<SpriteRenderer>().enabled = false;
            playerInTrigger = false;
        }
    }
}
