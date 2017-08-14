using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial_EndSequence : MonoBehaviour {

    public float textSpeed;
    public string levelToLoad;
    public string[] germanDialoge;
    public string[] englishDialoge;

    private Dialoge dialoge;
    private EventList eventList;
    private Textfield textfield;
    private GameObject player;


    void Start () {
        dialoge = new Dialoge(textSpeed, germanDialoge, englishDialoge);
        eventList = GameObject.FindGameObjectWithTag("EventList").GetComponent<EventList>();
        player = GameObject.FindGameObjectWithTag("Player");
        textfield = GameObject.FindGameObjectWithTag("TextFieldUI").GetComponent<Textfield>();
    }
	
	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            dialoge.StartDialoge();
            if (eventList.GetEvent("Boss_01_Defeated") == null)
                StartCoroutine("DialogeBeforeBoss");
            else if (eventList.GetEvent("Boss_01_Defeated").HasHappened)
                StartCoroutine("DialogeAfterBoss");
        }
    }

    private IEnumerator DialogeBeforeBoss()
    {
        dialoge.StartDialoge();
        textfield.ChangeTalker(dialoge.PlayerSprite);
        textfield.ChangeTalkerName("Adventurer");
        textfield.PrintText(dialoge.UsedDialoge[0], textSpeed, dialoge.Player_Talking);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.StopPrintText();

        dialoge.EndDialoge();
    }

    private IEnumerator DialogeAfterBoss()
    {
        dialoge.StartDialoge();
        textfield.ChangeTalker(dialoge.PlayerSprite);
        textfield.ChangeTalkerName("Adventurer");
        textfield.PrintText(dialoge.UsedDialoge[1], textSpeed, dialoge.Player_Talking);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.StopPrintText();

        textfield.PrintText(dialoge.UsedDialoge[2], textSpeed, dialoge.Player_Talking);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.StopPrintText();

        textfield.PrintText(dialoge.UsedDialoge[3], textSpeed, dialoge.Player_Talking);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.StopPrintText();

        dialoge.EndDialoge();

        print("Loading Level: " + levelToLoad);
        SceneManager.LoadScene(levelToLoad);
    }
}
