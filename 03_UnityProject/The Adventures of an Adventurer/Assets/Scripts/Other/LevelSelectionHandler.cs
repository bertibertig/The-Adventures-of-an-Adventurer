using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionHandler : MonoBehaviour {

    public string xmlTag;
    public string filepath;
    public float textSpeed;
    public string[] germanDialoge;
    public string[] englishDialoge;
    public GameObject player;
    public GameObject children;
    public GameObject levelSelection;
    public Textfield textfield;
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
        
        if (player == null)
            player = Resources.Load("Player") as GameObject;
        //levelSelection.SetActive(false);
    }

    public void HandleLevel(int levelID)
    {
        levelSelection.GetComponent<LevelSelection>().LevelSelectionDisabled = true;
        if (levelID == 0)
            StartCoroutine("Level1CoRoutine");
    }

    IEnumerator Level1CoRoutine()
    {
        if (!sequenceRunning)
        {
            sequenceRunning = true;
            while (dHandler.Talking)
            {
                yield return null;
            }
            StartCoroutine("FadeOut");
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
