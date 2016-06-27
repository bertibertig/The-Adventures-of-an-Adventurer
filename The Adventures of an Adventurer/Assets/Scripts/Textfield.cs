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
            text.text += letters[i];
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private IEnumerator PrintTextByCharsWithAudio()
    {
        char[] letters = usedText.ToCharArray();
        text.text = "";
        
        for (int i = 0; i < usedText.Length; i++)
        {
            if(!speachSFX.isPlaying)
                speachSFX.Play();
            text.text += letters[i];
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
