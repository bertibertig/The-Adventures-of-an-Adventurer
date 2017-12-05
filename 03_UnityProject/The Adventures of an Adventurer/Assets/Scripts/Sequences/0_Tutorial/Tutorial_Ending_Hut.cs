using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tutorial_Ending_Hut : MonoBehaviour {

    public string id;
    public GameObject levelSelection;
    public GameObject rockingChair;

    private DialogeHandler dHandler;
    private GameObject eventList;

    // Use this for initialization
    void Start () {
        if (GameObject.FindGameObjectWithTag("EventList") == null)
        {
            eventList = GameObject.Instantiate(Resources.Load("Other/EventList"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }
        else
            eventList = GameObject.FindGameObjectWithTag("EventList");

        SearchForGameObjects searchForDialogeDB = GameObject.FindGameObjectWithTag("EventList").GetComponent<SearchForGameObjects>();
        searchForDialogeDB.DialogeDBFoundEventHandler += DialogeDBFound;

        levelSelection.SetActive(false);
        StartCoroutine("FadeIn");

        if(GameObject.FindGameObjectWithTag("DialogesDB") != null)
        {
            dHandler = GameObject.FindGameObjectWithTag("DialogesDB").GetComponent<XMLReader>().GetDialougeHandlerByName(id).GetComponent<DialogeHandler>();
            dHandler.DialogeEndedEventHandler += DialogeEnded;
        }
    }

    void DialogeDBFound(object sender, EventArgs e)
    {
        dHandler = GameObject.FindGameObjectWithTag("DialogesDB").GetComponent<XMLReader>().GetDialougeHandlerByName(id).GetComponent<DialogeHandler>();
        dHandler.DialogeEndedEventHandler += DialogeEnded; 
    }

    private void DialogeEnded(object sender, EventArgs e)
    {
        levelSelection.SetActive(true);
        levelSelection.GetComponent<LevelSelection>().enabled = true;
    }

    IEnumerator FadeIn()
    {
        SpriteRenderer blackFadeIn = gameObject.GetComponentInChildren<SpriteRenderer>();
        AudioSource fireplace = gameObject.GetComponentInChildren<AudioSource>();
        while (blackFadeIn.color.a > 0)
        {
            fireplace.volume += 0.01f;
            blackFadeIn.color = new Color(blackFadeIn.color.r, blackFadeIn.color.g, blackFadeIn.color.b, (blackFadeIn.color.a - 0.01f));
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(0.5f);
        levelSelection.GetComponent<LevelSelection>().LevelSelectionDisabled = true;
        dHandler.StartConversation();
    }
}
