using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro_Hut : MonoBehaviour {

    public string id = "NewGameIntro";
    public GameObject player;
    public GameObject children;
    public GameObject levelSelection;
    public GameObject rockingChair;

    private DialogeHandler dHandler;
    private Textfield textfield;

	// Use this for initialization
	void Start () {
        //SdialogeHandler = new XMLReader().LoadDialouge(xmlTag,filepath);

        SearchForGameObjects searchForDialogeDB = GameObject.FindGameObjectWithTag("EventList").GetComponent<SearchForGameObjects>();
        searchForDialogeDB.DialogeDBFoundEventHandler += DialogeDBFound;

        GameObject.FindGameObjectWithTag("MultiplayerGUI").SetActive(false);

        StartCoroutine("FadeIn");
    }

    void DialogeDBFound(object sender, EventArgs e)
    {
        dHandler = GameObject.FindGameObjectWithTag("DialogesDB").GetComponent<XMLReader>().GetDialougeHandlerByName(id).GetComponent<DialogeHandler>();
        StartCoroutine("Intro_Sequence");
    }

    IEnumerator FadeIn()
    {
        SpriteRenderer blackFadeIn = gameObject.GetComponentInChildren<SpriteRenderer>();
        AudioSource fireplace = gameObject.GetComponentInChildren<AudioSource>();
        while(blackFadeIn.color.a > 0)
        {
            fireplace.volume += 0.01f;
            blackFadeIn.color = new Color(blackFadeIn.color.r, blackFadeIn.color.g, blackFadeIn.color.b, (blackFadeIn.color.a-0.01f));
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(2);
        dHandler.StartConversation();
    }
	
	IEnumerator Intro_Sequence()
    {
        while (!dHandler.ConversationFinishedOnce)
        {
            yield return null;
        }

        dHandler.Movement.MovementDisabled = true;
        rockingChair.GetComponent<Animator>().SetBool("IsUsed", false);
        player.transform.position = rockingChair.transform.position;
        player.SetActive(true);
        player.transform.rotation = Quaternion.Euler(0, 180, 0);

        while(true)
        {
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-10f, 0));
            yield return null;
        }
    }
}
