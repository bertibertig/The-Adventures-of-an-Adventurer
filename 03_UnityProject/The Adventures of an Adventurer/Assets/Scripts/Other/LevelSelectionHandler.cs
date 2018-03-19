using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionHandler : MonoBehaviour {

    public string id;
    public GameObject levelSelection;
    public GameObject blackFade;
    public Text continuedText;
    public string toBeContinued = "To be continued ...";

    private DialogeHandler dHandler;
    private GameObject eventList;
    private bool sequenceRunning = false;
    // Use this for initialization
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("EventList") == null)
        {
            eventList = GameObject.Instantiate(Resources.Load("Other/EventList"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }
        else
            eventList = GameObject.FindGameObjectWithTag("EventList");

        //dHandler = new XMLReader().LoadDialouge(xmlTag, filepath);

        //levelSelection.SetActive(false);
    }

    private void DialogeEnded(object sender, EventArgs e)
    {
        StartCoroutine("FadeOut");
    }

    public void HandleLevel(int levelID)
    {
        if (!sequenceRunning)
        {
            sequenceRunning = true;
            levelSelection.GetComponent<LevelSelection>().LevelSelectionDisabled = true;
            if (levelID == 0)
            {
                dHandler = GameObject.FindGameObjectWithTag("DialogesDB").GetComponent<XMLReader>().GetDialougeHandlerByName(id+(levelID+1).ToString()).GetComponent<DialogeHandler>();
                dHandler.DialogeEndedEventHandler += DialogeEnded;
                dHandler.StartConversation();
            }
        }
    }

    IEnumerator FadeOut()
    {
        SpriteRenderer bFade = blackFade.GetComponent<SpriteRenderer>();
        do
        {
            bFade.color = new Color(bFade.color.r, bFade.color.g, bFade.color.b, (bFade.color.a + 0.01f));
            yield return new WaitForSeconds(0.03f);
        } while (bFade.color.a <= 1);

        yield return new WaitForSeconds(3);
        char[] continuedChars = toBeContinued.ToCharArray();

        for(int i = 0; i < toBeContinued.Length; i++)
        {
            print(i);
            continuedText.GetComponent<Text>().text += continuedChars[i];
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2);
        sequenceRunning = false;
        gameObject.GetComponent<ChangeLevel>().LoadLevel();
    }
}
