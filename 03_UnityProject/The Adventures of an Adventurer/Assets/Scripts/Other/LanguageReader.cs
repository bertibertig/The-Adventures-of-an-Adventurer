using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.IO;
using System.Text;

public class LanguageReader : MonoBehaviour {

    public event EventHandler LanguageLoadedEventHandler;

    public string Language { get; set; }


    // Use this for initialization
    void Start () {
        Language = ReadLanguage();
        LanguageLoaded();

    }

    private void LanguageLoaded()
    {
        print("Notifieing Subscribers (LanguageLoaded)");
        if (LanguageLoadedEventHandler != null)
            LanguageLoadedEventHandler(this, null);
    }

    public string ReadLanguage(string filePath = @"xml\options")
    {
        String xmlString = (Resources.Load(filePath) as TextAsset).text;
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
