using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Textfield : MonoBehaviour {

    public Text text;
    public Text talkerName;
    public Image field;
    public Image talker;

    private string usedText;
    private float textSpeed;
    private AudioSource speachSFX;

    void Start()
    {
        //Disable();
    }
    public void Enable()
    {
        text.enabled = true;
        field.enabled = true;
        talker.enabled = true;
        talkerName.enabled = true;
    }

    public void Disable()
    {
        text.enabled = false;
        field.enabled = false;
        talker.enabled = false;
        talkerName.enabled = false;
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
                print((i + 3) < letters.Length);
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
