using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using UnityEngine;

public class XMLReader : MonoBehaviour {

    private StringBuilder output;

    public XMLReader()
    {
        output = new StringBuilder();
    }

	public string ReadLanguage(string filePath = "options.xml")
    {
        String xmlString = new StreamReader("options.xml").ReadToEnd();
        string tmp = ReadXML(xmlString, "language");
        return tmp;
    }

    private string ReadXML(String content, string xmlTag)
    {
        using (XmlReader reader = XmlReader.Create(new StringReader(content)))
        {
            
            reader.ReadToFollowing(xmlTag);
            output.AppendLine(reader.ReadElementContentAsString());
        }

        return output.ToString();
    }
}
