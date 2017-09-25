using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Ending_Hut : MonoBehaviour {

    public float textSpeed;
    public string[] germanDialoge;
    public string[] englishDialoge;
    public GameObject player;
    public GameObject children;
    public GameObject levelSelection;
    public GameObject rockingChair;
    public Textfield textfield;

    private DialogeHandler dialogeHandler;
    private GameObject eventList;

    // Use this for initialization
    void Start () {
        if (GameObject.FindGameObjectWithTag("EventList") == null)
        {
            eventList = GameObject.Instantiate(Resources.Load("Other/EventList"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }
        else
            eventList = GameObject.FindGameObjectWithTag("EventList");

        dialogeHandler = new DialogeHandler(textSpeed, germanDialoge, englishDialoge, children);
        if(textfield == null)
            textfield = dialogeHandler.Textfield;
        if (player == null)
            player = Resources.Load("Player") as GameObject;
        levelSelection.SetActive(false);
        StartCoroutine("FadeIn");
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
        StartCoroutine("WaitForInisialisation");
    }

    IEnumerator WaitForInisialisation()
    {
        GameObject tmpObj = GameObject.Instantiate(Resources.Load("Player"), new Vector3(-10, -4.88f, -1) , Quaternion.identity) as GameObject;
        bool conversationStarted = false;
        do
        {
            print(dialogeHandler.Ready + " " + conversationStarted);
            if (dialogeHandler.Ready && !conversationStarted)
            {
                conversationStarted = true;
                textfield.EnableText();
                StartCoroutine("Intro_Sequence");
            }
            yield return null;
        } while (!conversationStarted);
    }

    IEnumerator Intro_Sequence()
    {
        levelSelection.GetComponent<LevelSelection>().LevelSelectionDisabled = true;
        textfield.ChangeTalker(dialogeHandler.PlayerSprite);
        textfield.ChangeTalkerName("Adventurer");
        textfield.PrintText(dialogeHandler.UsedDialoge[0], textSpeed, dialogeHandler.PlayerAudio);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.StopPrintText();
        textfield.PrintWholeText();
        if (!textfield.FinishedPrintingText)
        {
            yield return new WaitForSeconds(0.1f);
            while (!Input.GetButtonDown("Interact"))
            {
                yield return null;
            }
            textfield.StopPrintText();
        }
        yield return null;

        textfield.ChangeTalker(dialogeHandler.Talker1Sprite);
        textfield.ChangeTalkerName("Grandchildren");
        textfield.PrintText(dialogeHandler.UsedDialoge[1], textSpeed, dialogeHandler.PlayerAudio);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.StopPrintText();
        textfield.PrintWholeText();
        if (!textfield.FinishedPrintingText)
        {
            yield return new WaitForSeconds(0.1f);
            while (!Input.GetButtonDown("Interact"))
            {
                yield return null;
            }
            textfield.StopPrintText();
        }
        textfield.StopPrintText();

        textfield.ChangeTalker(dialogeHandler.PlayerSprite);
        textfield.ChangeTalkerName("Adventurer");
        textfield.PrintText(dialogeHandler.UsedDialoge[2], textSpeed, dialogeHandler.PlayerAudio);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.StopPrintText();
        textfield.PrintWholeText();
        if (!textfield.FinishedPrintingText)
        {
            yield return new WaitForSeconds(0.1f);
            while (!Input.GetButtonDown("Interact"))
            {
                yield return null;
            }
            textfield.StopPrintText();
        }

        textfield.PrintText(dialogeHandler.UsedDialoge[3], textSpeed, dialogeHandler.PlayerAudio);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.StopPrintText();
        textfield.PrintWholeText();
        if (!textfield.FinishedPrintingText)
        {
            yield return new WaitForSeconds(0.1f);
            while (!Input.GetButtonDown("Interact"))
            {
                yield return null;
            }
            textfield.StopPrintText();
        }

        textfield.StopPrintText();
        textfield.DisableText();

        levelSelection.SetActive(true);
    }
}
