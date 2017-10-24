using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogeHandler : MonoBehaviour {

    public float TextSpeed { get; set; }
    public string[] GermanText { get; set; }
    public string[] EnglishText { get; set; }
    public string Language { get; set; }
    public AudioSource PlayerAudio { get; set; }
    public AudioSource Talker1Audio { get; set; }
    public AudioSource Talker2Audio { get; set; }
    public GameObject Talker1 { get; set; }
    public GameObject Talker2 { get; set; }
    public Sprite PlayerSprite { get; set; }
    public Sprite Talker1Sprite { get; set; }
    public Sprite Talker2Sprite { get; set; }
    public GameObject Player { get; set; }
    public string[] UsedDialoge { get; set; }
    public Player_Movement Movement { get; set; }
    public Textfield Textfield { get; set; }
    public bool Ready { get; set; }

    public DialogeHandler(float textspeed, string[] germanDialoge, string[] englishDialoge, GameObject talker1, GameObject talker2 = null, AudioSource talker1Audio = null, AudioSource talker2Audio = null)
    {
        Ready = false;
        SearchForGameObjects searchForPlayer = GameObject.FindGameObjectWithTag("EventList").GetComponent<SearchForGameObjects>();
        searchForPlayer.PlayerFoundEventHandler += PlayerFound;

        if (TextSpeed <= 0)
            TextSpeed = 0.05f;
        else
            TextSpeed = textspeed;
        GermanText = germanDialoge;
        EnglishText = englishDialoge;
        Talker1 = talker1;
        if(talker2 != null)
            Talker2 = talker2;
        Talker1Audio = talker1Audio;
        if (talker2 != null)
            Talker2Audio = talker2Audio;
        Textfield = GameObject.FindGameObjectWithTag("TextFieldUI").GetComponent<Textfield>();

        if (Talker1 != null)
            Talker1Sprite = Talker1.GetComponent<SpriteRenderer>().sprite;
        if (Talker2 != null)
            Talker2Sprite = Talker2.GetComponent<SpriteRenderer>().sprite;

        if (GameObject.FindGameObjectsWithTag("EventList").Length <= 0)
            Language = "english";
        else
            Language = "english";
            //Language = GameObject.FindGameObjectWithTag("EventList").GetComponentInChildren<LanguageReader>().Language;
        if (Language == "german")
            UsedDialoge = germanDialoge;
        else
            UsedDialoge = englishDialoge;
        PlayerFound();
    }

    public void PlayerFound(object sender, EventArgs e)
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Movement = Player.GetComponent<Player_Movement>();
        PlayerSprite = (Resources.Load("Player") as GameObject).GetComponent<SpriteRenderer>().sprite;
        PlayerAudio = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<AudioSource>();
        Ready = true;
    }

    public void PlayerFound()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Movement = Player.GetComponent<Player_Movement>();
        PlayerSprite = (Resources.Load("Player") as GameObject).GetComponent<SpriteRenderer>().sprite;
        PlayerAudio = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<AudioSource>();
        Ready = true;
    }


}
