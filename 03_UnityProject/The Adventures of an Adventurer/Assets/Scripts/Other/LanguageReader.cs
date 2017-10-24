using UnityEngine;
using System.Collections;
using System.Xml;

public class LanguageReader : MonoBehaviour {

    public string language;

    public string Language
    {
        get { return language; }
        set { language = value; }
    }


    // Use this for initialization
    void Start () {
        XMLReader xmlReader = new XMLReader();
        Language = xmlReader.ReadLanguage();
        /*
        if (language == "german")
            language = "german";
        else
            language = "english";*/
	}
}
