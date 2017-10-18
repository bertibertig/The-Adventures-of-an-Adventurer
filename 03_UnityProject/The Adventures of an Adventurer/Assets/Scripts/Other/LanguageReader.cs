using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.IO;
using System.Text;

public class LanguageReader : MonoBehaviour {

    public string Language { get; set; }
    public bool LanguageLoaded { get; set; }


    // Use this for initialization
    void Start () {
        LanguageLoaded = false;
        Language = ReadLanguage();
        LanguageLoaded = true;

    }

    public string ReadLanguage(string filePath = "options.xml")
    {
        String xmlString = new StreamReader("options.xml").ReadToEnd();
        return ReadXML(xmlString, "language");
    }

    private string ReadXML(String content, string xmlTag)
    {
        using (XmlReader reader = XmlReader.Create(new StringReader(content)))
        {
            reader.ReadToFollowing(xmlTag);
            string temp = reader.ReadElementContentAsString();
            return temp;
        }
    }
}
