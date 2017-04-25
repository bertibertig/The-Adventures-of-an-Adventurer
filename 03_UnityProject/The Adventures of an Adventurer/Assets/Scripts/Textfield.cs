using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Reflection;

public class Textfield : MonoBehaviour {

    public Text text;
    public Text talkerName;
    public Image field;
    public Image talker;
    public Text choiceTitle;
    public Text choice01;
    public Text choice02;
    public Text choice03;

    private string usedText;
    private float textSpeed;
    private AudioSource speachSFX;
    private bool choicesEnabled;
    private int currentChoicePosition;

    public int GetCurrentChoicePosition { get { return currentChoicePosition; } }

    void Start()
    {
        //Disable();
        choicesEnabled = false;
    }
    public void EnableText()
    {
        text.enabled = true;
        field.enabled = true;
        talker.enabled = true;
        talkerName.enabled = true;
    }

    public void DisableText()
    {
        text.enabled = false;
        field.enabled = false;
        talker.enabled = false;
        talkerName.enabled = false;
    }

    public void EnableChoise()
    {
        text.enabled = false;
        if(field.enabled != true || talker.enabled != true || talkerName.enabled != true)
        {
            field.enabled = true;
            talker.enabled = true;
            talkerName.enabled = true;
        }
        choiceTitle.enabled = true;
        choice01.enabled = true;
        choice02.enabled = true;
        choice03.enabled = true;
        choicesEnabled = true;
        StartCoroutine("Choose");
    }

    public void DisableChoise()
    {
        choiceTitle.enabled = false;
        choice01.enabled = false;
        choice02.enabled = false;
        choice03.enabled = false;
        choicesEnabled = false;
        StopCoroutine("Choose");
    }

    public void PrintText(string text, float speed)
    {
        usedText = text;
        textSpeed = speed;
        //print(usedText);
        StartCoroutine("PrintTextByChars");
    }

    public void PrintText(string text, float speed, AudioSource speSFX)
    {
        usedText = text;
        textSpeed = speed;
        speachSFX = speSFX;
        //print(usedText);
        StartCoroutine("PrintTextByCharsWithAudio");
    }

    public void StopPrintText()
    {
        StopCoroutine("PrintTextByChars");
        StopCoroutine("PrintTextByCharsWithAudio");
        if(speachSFX != null)
            speachSFX.Stop();
        text.text = "";
    }

    public void ChangeTalker(Sprite sprite)
    {
        talker.sprite = sprite;
    }

    public void ChangeTalkerName(string name)
    {
        talkerName.text = name;
    }

    public void SetChoices(string choiceTitle, string choice01, string choice02, string choice03 = " ")
    {
        this.choiceTitle.text = choiceTitle;
        this.choice01.text = "- " + choice01;
        this.choice02.text = choice02;
        this.choice03.text = choice03;
        currentChoicePosition = 1;
    }

    IEnumerator Choose()
    {
        do
        {
            if (Input.GetButtonDown("Up"))
            {
                yield return new WaitForSeconds(0.1f);
                ChoiceUp();
            }
            else if (Input.GetButtonDown("Down"))
            {
                yield return new WaitForSeconds(0.1f);
                ChoiceDown();
            }
            yield return null;
        } while (choicesEnabled);
    }

    public void ChoiceUp()
    {
        if(currentChoicePosition == 2)
        {
            choice02.text = choice02.text.Substring(2, choice02.text.Length - 2);
            choice01.text = "- " + choice01.text;
            currentChoicePosition = 1;
        }
        else if(currentChoicePosition == 3)
        {
            choice03.text = choice03.text.Substring(2, choice03.text.Length - 2);
            choice02.text = "- " + choice02.text;
            currentChoicePosition = 2;
        }
    }

    public void ChoiceDown()
    {
        if (currentChoicePosition == 1)
        {
            choice01.text = choice01.text.Substring(2, choice01.text.Length - 2);
            choice02.text = "- " + choice02.text;
            currentChoicePosition = 2;
        }
        else if (currentChoicePosition == 2 && choice03.text != " ")
        {
            choice02.text = choice02.text.Substring(2, choice02.text.Length - 2);
            choice03.text = "- " + choice03.text;
            currentChoicePosition = 3;
        }
    }

    private IEnumerator PrintTextByChars()
    {
        char[] letters = usedText.ToCharArray();
        text.text = "";
        for (int i = 0; i < usedText.Length; i++)
        {
            if ((i + 3) < letters.Length && letters[i + 1] == ' ' && letters[i + 2] == ' ')
            {
                print("less Space");
                do
                {
                    text.text += letters[i];
                    i++;
                    yield return new WaitForSeconds(0.00000001f);
                } while (i >= letters.Length || (i + 3) < letters.Length && letters[i + 1] != ' ' && letters[i + 2] != ' ');

            }
            else
            {
                text.text += letters[i];
                yield return new WaitForSeconds(textSpeed);
            }
        }
    }

    private IEnumerator PrintTextByCharsWithAudio()
    {
        char[] letters = usedText.ToCharArray();
        text.text = "";
        
        for (int i = 0; i < letters.Length; i++)
        {
            if(!speachSFX.isPlaying)
                speachSFX.Play();
            if ((i + 3) < letters.Length && letters[i + 1] == ' ' && letters[i + 2] == ' ' && letters[i + 3] == ' ')
            {
                print("less Space");
                do
                {
                    text.text += letters[i];
                    i++;
                    yield return null;
                } while (i >= letters.Length || (i + 3) < letters.Length && letters[i + 1] != ' ' && letters[i + 2] != ' ');

            }
            else
            {
                print((i + 3) < letters.Length);
                text.text += letters[i];
                yield return new WaitForSeconds(textSpeed);
            }
        }
    }
}
