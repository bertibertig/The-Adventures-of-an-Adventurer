using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialoge : MonoBehaviour {

    private float textSpeed;
    private string[] germanDialoge;
    private string[] englishDialoge;
    private string language;
    private Textfield dialoge;
    private GameObject player;
    private bool coRoutineStarted;
    private Player_Movement movement;

    public AudioSource Player_Talking { get; set; }

    public string[] UsedDialoge { get; set; }

    public Sprite PlayerSprite { get; set; }

    public Dialoge(float _textspeed, string[] _geramDialoge, string[] _englishDialoge)
    {
        this.textSpeed = _textspeed;
        this.germanDialoge = _geramDialoge;
        this.englishDialoge = _englishDialoge;
        dialoge = GameObject.FindGameObjectWithTag("TextFieldUI").GetComponent<Textfield>();
        player = GameObject.FindGameObjectWithTag("Player");
        Player_Talking = player.GetComponentInChildren<AudioSource>();
        PlayerSprite = player.GetComponent<SpriteRenderer>().sprite;
        movement = player.GetComponent<Player_Movement>();
        if (textSpeed <= 0)
            textSpeed = 0.05f;
        coRoutineStarted = false;
        if (GameObject.FindGameObjectsWithTag("EventList").Length <= 0)
            language = "english";
        else
            language = GameObject.FindGameObjectWithTag("EventList").GetComponentInChildren<LanguageReader>().Language;
        if (language == "german")
            UsedDialoge = germanDialoge;
        else
            UsedDialoge = englishDialoge;
    }

    public void StartDialoge()
    {
        dialoge.EnableText();
        coRoutineStarted = true;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        player.GetComponent<Player_Movement>().MovementDisabled = true;
        player.GetComponent<Player_Attack>().enabled = false;
    }

    public void EndDialoge()
    {
        dialoge.StopPrintText();
        dialoge.DisableText();
        player.transform.localRotation = Quaternion.Euler(0, 0, 0);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        player.GetComponent<Player_Movement>().MovementDisabled = false;
        player.GetComponent<Player_Attack>().enabled = true;
        coRoutineStarted = false;
    }

    public void Conversation(int id, AudioSource talkerSound)
    {
        dialoge.PrintText(UsedDialoge[id], textSpeed, talkerSound);
    }
}
