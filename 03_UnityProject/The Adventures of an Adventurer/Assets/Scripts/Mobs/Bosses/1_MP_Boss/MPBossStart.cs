using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPBossStart : MonoBehaviour {

    public string id;
    public GameObject MainBoss;

    private DialogeHandler dHandler;
    private bool fightStarted = false;

    private void Start()
    {
        SearchForGameObjects searchForDialogeDB = GameObject.FindGameObjectWithTag("EventList").GetComponent<SearchForGameObjects>();
        searchForDialogeDB.DialogeDBFoundEventHandler += DialogeDBFound;
    }

    void DialogeDBFound(object sender, EventArgs e)
    {
        dHandler = GameObject.FindGameObjectWithTag("DialogesDB").GetComponent<XMLReader>().GetDialougeHandlerByName(id).GetComponent<DialogeHandler>();
        dHandler.DialogeEndedEventHandler += DialogeEnded;
    }

    void DialogeEnded(object sender, EventArgs e)
    {
        MainBoss.GetComponent<MPBossFight>().StartFight();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player") && !fightStarted)
        {
            fightStarted = true;
            dHandler.StartConversation();
        }
    }
}
