using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogeHandler : MonoBehaviour {

    private bool loadedTalkerSprites;
    private bool loadedMainComponents;
    private List<string> tempTalkerRessources;
    public event EventHandler DialogeEndedEventHandler;

    public bool Talking { get; set; }
    public float DefaultTextSpeed { get; set; }
    public List<string> Dialoge { get; set; }
    public string Language { get; set; }
    public List<GameObject> Talkers { get; set; }
    public List<string> ResourcesAsString { get; set; }
    public List<string> TalkerNames { get; set; }
    public List<Sprite> Sprites { get; set; }
    public List<float> TextSpeeds { get; set; }
    public List<AudioSource> TalkersSounds { get; set; }
    public List<string> MethodesAfterEnd { get; set; }
    public GameObject Player { get; set; }
    public Player_Movement Movement { get; set; }
    public Textfield Textfield { get; set; }
    public string DialougeName { get; set; }
    public bool Ready { get; set; }
    public bool ConversationFinishedOnce { get; set; }

    public void StartConversation(int printUntil = -1)
    {
        if(!Talking)
        {
            Talking = true;
            Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            Movement.MovementDisabled = true;
            if (Player.GetComponent<Player_Attack>().isActiveAndEnabled)
            {
                Player.GetComponent<Player_Attack>().enabled = false;
            }
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
            Textfield.PrintText(Dialoge[i], TextSpeeds[i], TalkersSounds[i]);
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
        DialogeEnded();
        ConversationFinishedOnce = true;
    }

    private IEnumerator PrintOneLineCoRoutine(int line)
    {
        Textfield.EnableText();
        Textfield.ChangeTalker(Talkers[line].GetComponent<SpriteRenderer>().sprite);
        Textfield.ChangeTalkerName(TalkerNames[line]);
        Textfield.PrintText(Dialoge[line], TextSpeeds[line],TalkersSounds[line]);
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
        DialogeEnded();
        ConversationFinishedOnce = true;
    }

    private void DialogeEnded()
    {
        print("Notifieing Subscribers (DilogeEnded)");
        if (DialogeEndedEventHandler != null)
            DialogeEndedEventHandler(this, null);
    }
}
