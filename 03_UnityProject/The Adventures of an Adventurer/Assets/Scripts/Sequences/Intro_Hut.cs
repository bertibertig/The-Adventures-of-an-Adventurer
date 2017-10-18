﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro_Hut : MonoBehaviour {

    public string xmlTag;
    public string filepath;
    public float textSpeed;
    public string[] germanDialoge;
    public string[] englishDialoge;
    public GameObject player;
    public GameObject children;
    public GameObject levelSelection;
    public GameObject rockingChair;

    private DialogeHandler dialogeHandler;
    private Textfield textfield;

	// Use this for initialization
	void Start () {
        //SdialogeHandler = new XMLReader().LoadDialouge(xmlTag,filepath);
        textfield = dialogeHandler.Textfield;

        StartCoroutine("FadeIn");
    }

    IEnumerator WaitForInisialisation()
    {
        bool conversationStarted = false;
        do
        {
            if (dialogeHandler.Ready && !conversationStarted)
            {
                conversationStarted = true;
                textfield.EnableText();
                StartCoroutine("Intro_Sequence");
            }
            yield return null;
        } while (!conversationStarted);
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
        StartCoroutine("WaitForInisialisation");
    }
	
	IEnumerator Intro_Sequence()
    {
        while (dialogeHandler.Talking)
        {
            yield return null;
        }

        dialogeHandler.Movement.MovementDisabled = true;
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
