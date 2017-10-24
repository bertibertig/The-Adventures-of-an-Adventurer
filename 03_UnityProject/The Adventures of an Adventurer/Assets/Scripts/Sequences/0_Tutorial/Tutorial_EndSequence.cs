using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial_EndSequence : MonoBehaviour {

    public float textSpeed;
    public string levelToLoad;
    public GameObject blackFade;
    public string[] germanDialoge;
    public string[] englishDialoge;

    private Dialoge dialoge;
    private EventList eventList;
    private Textfield textfield;
    private GameObject player;
    private bool dialogeStarted = false;


    void Start () {
        blackFade.GetComponent<SpriteRenderer>().color = new Color(blackFade.GetComponent<SpriteRenderer>().color.r, blackFade.GetComponent<SpriteRenderer>().color.g, blackFade.GetComponent<SpriteRenderer>().color.b, 0);
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
        if (!dialogeStarted)
        {
            dialogeStarted = true;
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
            player.GetComponent<Player_Movement>().MovementDisabled = true;

            StartCoroutine("FadeOut");
        }
    }

    IEnumerator FadeOut()
    {
        SpriteRenderer bFade = blackFade.GetComponent<SpriteRenderer>();
        do
        {
            print(bFade.color.a);
            bFade.color = new Color(bFade.color.r, bFade.color.g, bFade.color.b, (bFade.color.a + 0.01f));
            yield return new WaitForSeconds(0.03f);
        } while (bFade.color.a <= 1);

        yield return new WaitForSeconds(2);

        print("Loading Level: " + levelToLoad);
        player.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(GameObject.FindGameObjectWithTag("UI_OnlyOnce"));
        dialogeStarted = false;
        gameObject.GetComponent<ChangeLevel>().LoadLevel();
    }
}
