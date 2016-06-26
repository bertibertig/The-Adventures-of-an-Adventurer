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
        print(usedText);
        StartCoroutine("PrintTextByChars");
    }

    public void StopPrintText()
    {
        StopCoroutine("PrintTextByChars");
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
}
