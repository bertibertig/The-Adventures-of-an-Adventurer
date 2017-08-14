using UnityEngine;
using System.Collections;

public class LanguageReader : MonoBehaviour {

    public string language;

    public string Language
    {
        get { return language; }
        set { language = value; }
    }


    // Use this for initialization
    void Start () {
        if (language == "german")
            language = "german";
        else
            language = "english";
	}
}
