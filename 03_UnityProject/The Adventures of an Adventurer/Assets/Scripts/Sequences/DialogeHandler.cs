using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogeHandler : MonoBehaviour {

    private bool loadedTalkerSprites;
    private bool loadedMainComponents;
    private List<string> tempTalkerRessources;

    public bool Talking { get; set; }
    public float TextSpeed { get; set; }
    public List<string> Dialoge { get; set; }
    public string Language { get; set; }
    public List<GameObject> Talkers { get; set; }
    public List<string> ResourcesAsString { get; set; }
    public List<string> TalkerNames { get; set; }
    public List<Sprite> Sprites { get; set; }
    public List<AudioSource> TalkersSounds { get; set; }
    public GameObject Player { get; set; }
    public Player_Movement Movement { get; set; }
    public Textfield Textfield { get; set; }
    public string DialougeName { get; set; }
    public bool Ready { get; set; }

    public void StartConversation(int printUntil = -1)
    {
        if(!Talking)
        {
            Talking = true;
            StartCoroutine("Conversation", printUntil);
        }
    }

    public void PrintOneLine(int line)
    {
        if (!Talking)
        {
            Talking = true;
            StartCoroutine("PrintOneLineCoRoutine", line);
        }
    }

    private IEnumerator Conversation(int printUntil)
    {
        if (printUntil == -1)
            printUntil = Dialoge.Count;
        Textfield.EnableText();

        for(int i = 0; i < printUntil; i++)
        {
            Textfield.ChangeTalker(Talkers[i].GetComponent<SpriteRenderer>().sprite);
            Textfield.ChangeTalkerName(TalkerNames[i]);
            Textfield.PrintText(Dialoge[i], TextSpeed);
            yield return new WaitForSeconds(0.1f);
            while (!Input.GetButtonDown("Interact"))
            {
                yield return null;
            }
            Textfield.StopPrintText();
            Textfield.PrintWholeText();
            if (!Textfield.FinishedPrintingText)
            {
                yield return new WaitForSeconds(0.1f);
                while (!Input.GetButtonDown("Interact"))
                {
                    yield return null;
                }
                Textfield.StopPrintText();
            }
            yield return null;
        }

        Textfield.DisableText();
        Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        Movement.MovementDisabled = false;
        Player.GetComponent<Player_Attack>().enabled = true;
        Talking = false;
    }

    private IEnumerator PrintOneLineCoRoutine(int line)
    {
        Textfield.EnableText();
        Textfield.ChangeTalker(Talkers[line].GetComponent<SpriteRenderer>().sprite);
        Textfield.ChangeTalkerName(TalkerNames[line]);
        Textfield.PrintText(Dialoge[line], TextSpeed);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        Textfield.StopPrintText();
        Textfield.PrintWholeText();
        if (!Textfield.FinishedPrintingText)
        {
            yield return new WaitForSeconds(0.1f);
            while (!Input.GetButtonDown("Interact"))
            {
                yield return null;
            }
            Textfield.StopPrintText();
        }

        /*Textfield.DisableText();
        Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        Movement.MovementDisabled = false;
        Player.GetComponent<Player_Attack>().enabled = true;*/
        Talking = false;
    }
}
